'use strict';
app.factory('deviceContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    // *** Public Properties ***       
    
    context.devicesLoaded = false;
    context.devices = [];   

    context.deviceNTPsLoaded = false;
    context.deviceNTPs = [];   

    context.deviceSensorsLoaded = false;
    context.deviceSensors = [];   

    context.sensorsInDevicesLoaded = false;
    context.sensorsInDevices = [];   

    // *** Finders ***        

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

    context.getDeviceByKey = getDeviceByKey;
    context.getDeviceNTPByKey = getDeviceNTPByKey;
    context.getDeviceSensorsByKey = getDeviceSensorsByKey;
    context.getDeviceSensorsByKey = getDeviceSensorsByKey;

    return context;

}]);