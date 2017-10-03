'use strict';
app.factory('dsFamilyTempSensorService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    //var serviceBase = ngAuthSettings.distributedServicesUri;
    var serviceBase = "http://localhost:47039/";

    var serviceFactory = {};

    var _getResolutions = function () {
        serviceFactory.resolutions.push({ dsFamilyTempSensorResolutionId: 9, mode: '9 bits', resolution: '0.5°C', conversionTime: '93.75 ms', value: 9 });
        serviceFactory.resolutions.push({ dsFamilyTempSensorResolutionId: 10, mode: '10 bits', resolution: '0.25°C', conversionTime: '187.5 ms', value: 10 });
        serviceFactory.resolutions.push({ dsFamilyTempSensorResolutionId: 11, mode: '11 bits', resolution: '0.125°C', conversionTime: '375 ms', value: 11 });
        serviceFactory.resolutions.push({ dsFamilyTempSensorResolutionId: 12, mode: '12 bits', resolution: '0.0625°C', conversionTime: '750 ms', value: 12 });        
    };

    var _setResolution = function (dsFamilyTempSensorId, dsFamilyTempSensorResolutionId) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            dsFamilyTempSensorResolutionId: dsFamilyTempSensorResolutionId,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setResolution', data).then(function (results) {
            return results;
        });
    };

    var _setHighAlarm = function (dsFamilyTempSensorId, highAlarm) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            highAlarm: highAlarm,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setHighAlarm', data).then(function (results) {
            return results;
        });
    };

    var _setLowAlarm = function (dsFamilyTempSensorId, lowAlarm) {
        var data = {
            dsFamilyTempSensorId: dsFamilyTempSensorId,
            lowAlarm: lowAlarm,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setLowAlarm', data).then(function (results) {
            return results;
        });
    };

    serviceFactory.resolutions = [];
    serviceFactory.setResolution = _setResolution;
    serviceFactory.setHighAlarm = _setHighAlarm;
    serviceFactory.setLowAlarm = _setLowAlarm;

    _getResolutions();

    return serviceFactory;

}]);