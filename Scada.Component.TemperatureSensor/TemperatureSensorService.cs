using Microsoft.Extensions.Hosting;

namespace Scada.Components.TemperatureSensor;
public class TemperatureSensorService(ConfigurationContainer configurationContainer) : IHostedService, IDisposable
{
    private readonly ConfigurationContainer _configurationContainer = configurationContainer;
    private Timer? _timer = null;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        if (_configurationContainer.Configuration?.MeasurementIntervalMs == null)
            throw new ApplicationException("MeasurementIntervalMs not set in configuration");

        _timer = new Timer(DoWork, null, TimeSpan.Zero,
            TimeSpan.FromMilliseconds(_configurationContainer.Configuration.MeasurementIntervalMs));

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }

    private void DoWork(object? state)
    {
        if (_configurationContainer.Configuration?.TemperatureUnit == null)
            throw new ApplicationException("TemperatureUnit not set in configuration");

        Console.WriteLine("{0} Sensor measurement: {1} degree {2}", DateTime.Now, Random.Shared.Next(5,99), _configurationContainer.Configuration.TemperatureUnit);
    }
}
