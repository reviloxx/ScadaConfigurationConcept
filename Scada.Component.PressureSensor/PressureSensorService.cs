using Microsoft.Extensions.Hosting;

namespace Scada.Component.PressureSensor;

public class PressureSensorService(PressureSensorConfigurationContainer configurationContainer) : IHostedService, IDisposable
{
    private readonly PressureSensorConfigurationContainer _configurationContainer = configurationContainer;
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
        if (_configurationContainer.Configuration?.PressureUnit == null)
            throw new ApplicationException("PressureUnit not set in configuration");

        Console.WriteLine("{0} Sensor measurement: {1} {2}", DateTime.Now, Random.Shared.Next(0, 20), _configurationContainer.Configuration.PressureUnit);
    }
}