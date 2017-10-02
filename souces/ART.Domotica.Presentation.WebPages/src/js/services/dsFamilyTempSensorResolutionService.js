'use strict';
app.factory('dsFamilyTempSensorResolutionService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};

    var _get = function () {

        return [
            { mode: '9 bits', resolution: '0.5°C', conversionTime: '93.75 ms', value: 9 },
            { mode: '10 bits', resolution: '0.25°C', conversionTime: '187.5 ms', value: 10 },
            { mode: '11 bits', resolution: '0.125°C', conversionTime: '375 ms', value: 11 },
            { mode: '12 bits', resolution: '0.0625°C', conversionTime: '750 ms', value: 12 },
        ];

    };

    serviceFactory.get = _get;

    return serviceFactory;

}]);