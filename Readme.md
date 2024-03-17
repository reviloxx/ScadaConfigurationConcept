**How to change the configurations at runtime**

PUT http://localhost:5093/config

    {
      "componentId": "d1574589-8764-4cbf-a79d-f5701f30c6df",
      "configuration": "{\"TemperatureUnit\":\"Fahrenheit\",\"MeasurementIntervalMs\": 2000}"
    }

PUT http://localhost:5093/config

    {
      "componentId": "344f923d-9cf3-4d79-8e25-565d95a4b602",
      "configuration": "{\"PressureUnit\":\"Psi\",\"MeasurementIntervalMs\": 3000}"
    }
