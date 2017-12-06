'use strict';
app.factory('deviceFinder', ['$rootScope', 'deviceContext', function ($rootScope, deviceContext) {

    var context = deviceContext;

    var serviceFactory = {};    

    var getDeviceByKey = function (deviceId) {
        for (var i = 0; i < context.devices.length; i++) {
            var item = context.devices[i];
            if (item.deviceId === deviceId) {
                return item;
            }
        }
    };

    var getDeviceNTPByKey = function (deviceNTPId) {
        for (var i = 0; i < context.deviceNTPs.length; i++) {
            var item = context.deviceNTPs[i];
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

    var getDeviceSensorsByKey = function (sensorsInDeviceId) {
        for (var i = 0; i < context.sensorsInDevices.length; i++) {
            var item = context.sensorsInDevices[i];
            if (item.sensorsInDeviceId === sensorsInDeviceId) {
                return item;
            }
        }
    };

    // *** Public Methods ***

    serviceFactory.getDeviceByKey = getDeviceByKey;
    serviceFactory.getDeviceNTPByKey = getDeviceNTPByKey;
    serviceFactory.getDeviceSensorsByKey = getDeviceSensorsByKey;
    serviceFactory.getDeviceSensorsByKey = getDeviceSensorsByKey;

    return serviceFactory;

}]);