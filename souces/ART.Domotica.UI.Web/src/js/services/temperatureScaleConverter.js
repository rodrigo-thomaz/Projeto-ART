'use strict';
app.factory('temperatureScaleConverter', function () {

    var serviceFactory = {};    

    // To

    var convertToRaw = function (temperatureScaleId, temperature) {
        switch (temperatureScaleId) {
            case 1:
                return convertCelsiusToRaw(temperature);
            case 2:
                return convertFahrenheitToRaw(temperature);
            default:
        }
    };

    var convertRawToCelsius = function (raw) {
        return raw * 0.0078125;
    }

    var convertRawToFahrenheit = function (raw) {
        return (raw * 0.0140625) + 32;
    }

    // From

    var convertFromRaw = function (temperatureScaleId, raw) {
        switch (temperatureScaleId) {
            case 1:
                return convertRawToCelsius(raw);
            case 2:
                return convertRawToFahrenheit(raw);
            default:
        }
    };

    var convertCelsiusToRaw = function (celsius) {
        return celsius / 0.0078125;
    }

    var convertFahrenheitToRaw = function (fahrenheit) {
        return (fahrenheit - 32) / 0.0140625;
    }   

    // To

    serviceFactory.convertToRaw = convertToRaw;
    serviceFactory.convertRawToCelsius = convertRawToCelsius;
    serviceFactory.convertRawToFahrenheit = convertRawToFahrenheit;

    // From

    serviceFactory.convertFromRaw = convertFromRaw;
    serviceFactory.convertCelsiusToRaw = convertCelsiusToRaw;
    serviceFactory.convertFahrenheitToRaw = convertFahrenheitToRaw;
        
    return serviceFactory;

});