**How to change the configuration of the temperature sensor at runtime**

POST request to localhost:5093 with body:

{
  "configurationKey": "TemperatureSensor",
  "configuration": "{\"TemperatureUnit\":\"Fahrenheit\",\"MeasurementIntervalMs\": 2000}"
}
