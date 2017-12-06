'use strict';
app.factory('deviceMapper', ['$rootScope', 'deviceContext', 'deviceConstant', 'timeZoneConstant', function ($rootScope, deviceContext, deviceConstant, timeZoneConstant) {

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
    //var mapper_DeviceNTP_TimeZone = function () {
    //    if (!mapper_DeviceNTP_TimeZone_Init && deviceContext.deviceLoaded && deviceContext.timeZoneLoaded) {
    //        mapper_DeviceNTP_TimeZone_Init = true;
    //        for (var i = 0; i < deviceContext.deviceNTP.length; i++) {
    //            applyTimeZoneInDeviceNTP(deviceContext.deviceNTP[i]);
    //        }
    //    }
    //};   

    var applyTimeZoneInDeviceNTP = function (deviceNTP) {
        var timeZone = deviceContext.getTimeZoneByKey(deviceNTP.timeZoneId);
        deviceNTP.timeZone = timeZone;
        delete deviceNTP.timeZoneId; // removendo a foreing key
    }

    // *** Navigation Properties Mappers ***


    // *** Events Subscriptions

    var onDeviceGetAllByApplicationIdCompleted = function (event, data) {
        deviceGetAllByApplicationIdCompletedSubscription();
        loadAll();        
    }      

    var onTimeZoneGetAllCompleted = function (event, data) {
        timeZoneGetAllCompletedSubscription();
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