**How to change the configuration of the temperature sensor at runtime**

PUT http://localhost:5093/config

    {
      "configurationKey": "TemperatureSensor",
      "configuration": "{\"TemperatureUnit\":\"Fahrenheit\",\"MeasurementIntervalMs\": 2000}"
    }
