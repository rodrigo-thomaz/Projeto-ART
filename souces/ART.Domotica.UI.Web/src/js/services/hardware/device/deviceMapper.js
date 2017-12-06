'use strict';
app.factory('deviceMapper', ['$rootScope', 'deviceContext', 'deviceConstant', 'globalizationContext', 'globalizationFinder', 'timeZoneConstant', function ($rootScope, deviceContext, deviceConstant, globalizationContext, globalizationFinder, timeZoneConstant) {

    var serviceFactory = {};    

    // *** Navigation Properties Mappers ***

    var loadAll = function () {

        for (var i = 0; i < deviceContext.device.length; i++) {

            var deviceNTP = deviceContext.device[i].deviceNTP;
            deviceContext.deviceNTP.push(deviceNTP);

            var deviceSensors = deviceContext.device[i].deviceSensors;
            deviceContext.deviceSensors.push(deviceSensors);

            for (var j = 0; j < deviceSensors.sensorsInDevice.length; j++) {
                var sensorsInDevice = deviceSensors.sensorsInDevice[j];
                deviceContext.sensorsInDevice.push(sensorsInDevice);
            }
        }

        deviceContext.deviceLoaded = true;
        deviceContext.deviceNTPLoaded = true;
        deviceContext.deviceSensorsLoaded = true;
        deviceContext.sensorsInDeviceLoaded = true;
    }

    var mapper_DeviceNTP_TimeZone_Init = false;
    var mapper_DeviceNTP_TimeZone = function () {
        if (!mapper_DeviceNTP_TimeZone_Init && deviceContext.deviceLoaded && globalizationContext.timeZoneLoaded) {
            mapper_DeviceNTP_TimeZone_Init = true;
            for (var i = 0; i < deviceContext.deviceNTP.length; i++) {
                var deviceNTP = deviceContext.deviceNTP[i];
                var timeZone = globalizationFinder.getTimeZoneByKey(deviceNTP.timeZoneId);
                deviceNTP.timeZone = timeZone;
                delete deviceNTP.timeZoneId; // removendo a foreing key
                if (timeZone.devicesNTP === undefined) {
                    timeZone.devicesNTP = [];
                }
                timeZone.devicesNTP.push(deviceNTP);
            }
        }
    };  

    // *** Navigation Properties Mappers ***


    // *** Events Subscriptions

    var onDeviceGetAllByApplicationIdCompleted = function (event, data) {
        deviceGetAllByApplicationIdCompletedSubscription();
        loadAll(); 
        mapper_DeviceNTP_TimeZone();
    }      

    var onTimeZoneGetAllCompleted = function (event, data) {
        timeZoneGetAllCompletedSubscription();
        mapper_DeviceNTP_TimeZone();
    }  

    var deviceGetAllByApplicationIdCompletedSubscription = $rootScope.$on(deviceConstant.getAllByApplicationIdCompletedEventName, onDeviceGetAllByApplicationIdCompleted);
    var timeZoneGetAllCompletedSubscription = $rootScope.$on(timeZoneConstant.getAllCompletedEventName, onTimeZoneGetAllCompleted);
        
    $rootScope.$on('$destroy', function () {
        deviceGetAllByApplicationIdCompletedSubscription();  
        timeZoneGetAllCompletedSubscription();
    });

    // *** Events Subscriptions
    
    
    return serviceFactory;

}]);