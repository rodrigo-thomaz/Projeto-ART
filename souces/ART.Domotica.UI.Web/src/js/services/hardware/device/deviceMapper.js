'use strict';
app.factory('deviceMapper', ['$rootScope', 'deviceContext', 'deviceConstant', 'globalizationContext', 'globalizationFinder', 'timeZoneConstant', 'sensorConstant',
    function ($rootScope, deviceContext, deviceConstant, globalizationContext, globalizationFinder, timeZoneConstant, sensorConstant) {

        var serviceFactory = {};

        // *** Navigation Properties Mappers ***

        var loadAll = function () {

            for (var i = 0; i < deviceContext.device.length; i++) {

                var device = deviceContext.device[i];

                var deviceNTP = device.deviceNTP;
                deviceNTP.device = device;
                deviceContext.deviceNTP.push(deviceNTP);

                var deviceSensors = device.deviceSensors;
                deviceSensors.device = device;
                deviceContext.deviceSensors.push(deviceSensors);

                for (var j = 0; j < deviceSensors.sensorInDevice.length; j++) {
                    var sensorInDevice = deviceSensors.sensorInDevice[j];
                    sensorInDevice.deviceSensors = deviceSensors;
                    deviceContext.sensorInDevice.push(sensorInDevice);
                }
            }

            deviceContext.deviceLoaded = true;
            deviceContext.deviceNTPLoaded = true;
            deviceContext.deviceSensorsLoaded = true;
            deviceContext.sensorInDeviceLoaded = true;
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

        var onSensorGetAllByApplicationIdCompleted = function (event, data) {

        }

        var deviceGetAllByApplicationIdCompletedSubscription = $rootScope.$on(deviceConstant.getAllByApplicationIdCompletedEventName, onDeviceGetAllByApplicationIdCompleted);
        var timeZoneGetAllCompletedSubscription = $rootScope.$on(timeZoneConstant.getAllCompletedEventName, onTimeZoneGetAllCompleted);
        var sensorGetAllByApplicationIdCompletedSubscription = $rootScope.$on(sensorConstant.getAllByApplicationIdCompletedEventName, onSensorGetAllByApplicationIdCompleted);

        $rootScope.$on('$destroy', function () {
            deviceGetAllByApplicationIdCompletedSubscription();
            timeZoneGetAllCompletedSubscription();
            sensorGetAllByApplicationIdCompletedSubscription();
        });

        // *** Events Subscriptions


        return serviceFactory;

    }]);