**How to change the configuration of the temperature sensor at runtime**

PUT request to localhost:5093 with body:

{
  "configurationKey": "TemperatureSensor",
  "configuration": "{\"TemperatureUnit\":\"Fahrenheit\",\"MeasurementIntervalMs\": 2000}"
}
