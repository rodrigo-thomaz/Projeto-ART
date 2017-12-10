'use strict';
app.factory('deviceFinder', ['$rootScope', 'deviceContext', function ($rootScope, deviceContext) {

    var context = deviceContext;

    var serviceFactory = {};    

    var getDeviceByKey = function (deviceId) {
        for (var i = 0; i < context.device.length; i++) {
            var item = context.device[i];
            if (item.deviceId === deviceId) {
                return item;
            }
        }
    };

    var getDeviceNTPByKey = function (deviceNTPId) {
        for (var i = 0; i < context.deviceNTP.length; i++) {
            var item = context.deviceNTP[i];
            if (item.deviceNTPId === deviceNTPId) {
                return item;
            }
        }
    };

    var getDeviceSensorsByKey = function (deviceSensorsId) {
        for (var i = 0; i < context.deviceSensors.length; i++) {
            var item = context.deviceSensors[i];
            if (item.deviceSensorsId === deviceSensorsId) {
                return item;
            }
        }
    };

    // *** Public Methods ***

    serviceFactory.getDeviceByKey = getDeviceByKey;
    serviceFactory.getDeviceNTPByKey = getDeviceNTPByKey;
    serviceFactory.getDeviceSensorsByKey = getDeviceSensorsByKey;

    return serviceFactory;

}]);