'use strict';
app.factory('sensorInDeviceFinder', ['$rootScope', 'deviceContext', function ($rootScope, deviceContext) {

    var context = deviceContext;

    var serviceFactory = {};    

    var getByKey = function (deviceTypeId, deviceDatasheetId, deviceId, sensorId, sensorDatasheetId, sensorTypeId) {
        for (var i = 0; i < context.sensorInDevice.length; i++) {
            var item = context.sensorInDevice[i];
            if (item.deviceTypeId === deviceTypeId && item.deviceDatasheetId === deviceDatasheetId && item.deviceId === deviceId && item.sensorId === sensorId && item.sensorDatasheetId === sensorDatasheetId && item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    };

    var getByDeviceSensorKey = function (deviceTypeId, deviceDatasheetId, deviceId) {
        var result = [];
        for (var i = 0; i < context.sensorInDevice.length; i++) {
            var item = context.sensorInDevice[i];
            if (item.deviceTypeId === deviceTypeId && item.deviceDatasheetId === deviceDatasheetId && item.deviceId === deviceId) {
                result.push(item);
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
    serviceFactory.getByDeviceSensorKey = getByDeviceSensorKey;
    serviceFactory.getBySensorKey = getBySensorKey;

    return serviceFactory;

}]);