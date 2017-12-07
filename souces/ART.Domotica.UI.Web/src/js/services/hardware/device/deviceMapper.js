'use strict';
app.factory('deviceMapper', ['$rootScope', 'deviceContext', 'deviceConstant', 'globalizationContext', 'globalizationFinder', 'sensorContext', 'sensorFinder',
    function ($rootScope, deviceContext, deviceConstant, globalizationContext, globalizationFinder, sensorContext, sensorFinder) {

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
                timeZoneLoadedUnbinding();
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

        var mapper_SensorInDevice_Sensor_Init = false;
        var mapper_SensorInDevice_Sensor = function () {
            if (!mapper_SensorInDevice_Sensor_Init && deviceContext.sensorInDeviceLoaded && sensorContext.sensorLoaded) {
                mapper_SensorInDevice_Sensor_Init = true;
                sensorLoadedUnbinding();
                for (var i = 0; i < deviceContext.sensorInDevice.length; i++) {
                    var sensorInDevice = deviceContext.sensorInDevice[i];
                    var sensor = sensorFinder.getSensorByKey(sensorInDevice.sensorId);
                    sensorInDevice.sensor = sensor;
                    sensor.sensorInDevice = sensorInDevice;
                }
            }
        };

        // *** Navigation Properties Mappers ***


        // *** Events Subscriptions

        var onDeviceGetAllByApplicationIdCompleted = function (event, data) {
            deviceGetAllByApplicationIdCompletedSubscription();
            loadAll();
            mapper_DeviceNTP_TimeZone();
            mapper_SensorInDevice_Sensor();
        }

        var deviceGetAllByApplicationIdCompletedSubscription = $rootScope.$on(deviceConstant.getAllByApplicationIdCompletedEventName, onDeviceGetAllByApplicationIdCompleted);
        
        $rootScope.$on('$destroy', function () {
            deviceGetAllByApplicationIdCompletedSubscription();
        });

        // *** Events Subscriptions


        // *** Watches

        var timeZoneLoadedUnbinding = globalizationContext.$watch('timeZoneLoaded', function (newValue, oldValue) {
            mapper_DeviceNTP_TimeZone();
        })

        var sensorLoadedUnbinding = sensorContext.$watch('sensorLoaded', function (newValue, oldValue) {
            mapper_SensorInDevice_Sensor();
        })

        // *** Watches

        return serviceFactory;

    }]);