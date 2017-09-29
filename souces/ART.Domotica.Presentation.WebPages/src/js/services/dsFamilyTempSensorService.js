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

    serviceFactory.setResolution = _setResolution;

    return serviceFactory;

}]);