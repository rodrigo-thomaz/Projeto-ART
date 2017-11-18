'use strict';
app.factory('temperatureScaleConverter', function () {

    var serviceFactory = {};    

    // To

    var convertToCelsius = function (temperatureScaleId, temperature) {
        switch (temperatureScaleId) {
            case 1:
                return temperature;
            case 2:
                return convertFahrenheitToCelsius(temperature);
            default:
        }
    };

    var convertFahrenheitToCelsius = function (fahrenheit) {
        return (fahrenheit - 32) * (5 / 9);
    }       

    // From

    var convertFromCelsius = function (temperatureScaleId, celsius) {
        switch (temperatureScaleId) {
            case 1:
                return celsius;
            case 2:
                return convertCelsiusToFahrenheit(celsius);
            default:
        }
    };       

    var convertCelsiusToFahrenheit = function (celsius) {
        return (celsius * 1.8) + 32;
    }

    // To

    serviceFactory.convertToCelsius = convertToCelsius;
    serviceFactory.convertFahrenheitToCelsius = convertFahrenheitToCelsius;

    // From

    serviceFactory.convertFromCelsius = convertFromCelsius;
    serviceFactory.convertCelsiusToFahrenheit = convertCelsiusToFahrenheit;
        
    return serviceFactory;

});