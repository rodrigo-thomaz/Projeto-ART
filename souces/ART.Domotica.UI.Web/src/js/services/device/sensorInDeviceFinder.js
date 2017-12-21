'use strict';
app.factory('sensorInDeviceFinder', ['$rootScope', 'deviceContext', function ($rootScope, deviceContext) {

    var context = deviceContext;

    var serviceFactory = {};    

    var getByKey = function (deviceSensorsId, deviceDatasheetId, sensorId, sensorDatasheetId, sensorTypeId) {
        for (var i = 0; i < context.sensorInDevice.length; i++) {
            var item = context.sensorInDevice[i];
            if (item.deviceSensorsId === deviceSensorsId && item.deviceDatasheetId === deviceDatasheetId && item.sensorId === sensorId && item.sensorDatasheetId === sensorDatasheetId && item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    };

    var getByDeviceSensorsKey = function (deviceSensorsId, deviceDatasheetId) {
        var result = [];
        for (var i = 0; i < context.sensorInDevice.length; i++) {
            var sensorInDevice = context.sensorInDevice[i];
            if (sensorInDevice.deviceSensorsId === deviceSensorsId && sensorInDevice.deviceDatasheetId === deviceDatasheetId) {
                result.push(sensorInDevice);
            }
        }
        return result;
    };

    var getBySensorKey = function (sensorId, sensorDatasheetId, sensorTypeId) {
        for (var i = 0; i < context.sensorInDevice.length; i++) {
            var item = context.sensorInDevice[i];
            if (item.sensorId === sensorId && item.sensorDatasheetId === sensorDatasheetId && item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    };

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;
    serviceFactory.getByDeviceSensorsKey = getByDeviceSensorsKey;
    serviceFactory.getBySensorKey = getBySensorKey;

    return serviceFactory;

}]);