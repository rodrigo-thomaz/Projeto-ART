'use strict';
app.factory('dsFamilyTempSensorService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    //var serviceBase = ngAuthSettings.distributedServicesUri;
    var serviceBase = "http://localhost:47039/";

    var serviceFactory = {};

    var _setResolution = function (deviceAddress, value) {
        var data = {
            deviceAddress: deviceAddress,
            value: value,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setResolution', data).then(function (results) {
            return results;
        });
    };

    var _setHighAlarm = function (deviceAddress, value) {
        var data = {
            deviceAddress: deviceAddress,
            value: value,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setHighAlarm', data).then(function (results) {
            return results;
        });
    };

    var _setLowAlarm = function (deviceAddress, value) {
        var data = {
            deviceAddress: deviceAddress,
            value: value,
        }
        return $http.post(serviceBase + 'api/dsFamilyTempSensor/setLowAlarm', data).then(function (results) {
            return results;
        });
    };

    serviceFactory.setResolution = _setResolution;
    serviceFactory.setHighAlarm = _setHighAlarm;
    serviceFactory.setLowAlarm = _setLowAlarm;

    return serviceFactory;

}]);