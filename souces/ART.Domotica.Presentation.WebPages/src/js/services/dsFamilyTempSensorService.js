'use strict';
app.factory('dsFamilyTempSensorService', ['$http', 'ngAuthSettings', function ($http, ngAuthSettings) {

    //var serviceBase = ngAuthSettings.distributedServicesUri;
    var serviceBase = "http://localhost:47039/";

    var serviceFactory = {};

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

    serviceFactory.setResolution = _setResolution;
    serviceFactory.setHighAlarm = _setHighAlarm;
    serviceFactory.setLowAlarm = _setLowAlarm;

    return serviceFactory;

}]);