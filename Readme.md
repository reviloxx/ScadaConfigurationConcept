**How to change the configurations at runtime**

PUT http://localhost:5093/config

    {
      "configurationKey": "TemperatureSensor",
      "configuration": "{\"TemperatureUnit\":\"Fahrenheit\",\"MeasurementIntervalMs\": 2000}"
    }

PUT http://localhost:5093/config

    {
      "configurationKey": "PressureSensor",
      "configuration": "{\"PressureUnit\":\"Psi\",\"MeasurementIntervalMs\": 3000}"
    }
