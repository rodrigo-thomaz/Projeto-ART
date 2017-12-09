'use strict';
app.factory('deviceMapper', ['$rootScope', 'deviceContext', 'deviceConstant', 'globalizationContext', 'globalizationFinder', 'sensorContext', 'sensorFinder',
    function ($rootScope, deviceContext, deviceConstant, globalizationContext, globalizationFinder, sensorContext, sensorFinder) {

        var serviceFactory = {};

        deviceContext.$watchCollection('device', function (newValues, oldValues) {
            //inserindo
            for (var i = 0; i < newValues.length; i++) {
                var device = newValues[i];
                //deviceNTP
                var deviceNTP = device.deviceNTP;
                deviceNTP.device = device;
                deviceContext.deviceNTP.push(deviceNTP);
                //deviceSensors
                var deviceSensors = device.deviceSensors;
                deviceSensors.device = device;
                deviceContext.deviceSensors.push(deviceSensors);
            }
            //removendo
            for (var i = 0; i < oldValues.length; i++) {
                var device = oldValues[i];
                //deviceNTP
                for (var j = 0; j < deviceContext.deviceNTP.length; j++) {
                    if (device.deviceNTP === deviceContext.deviceNTP[j]) {
                        deviceContext.deviceNTP.splice(j, 1);
                    }
                }
                //deviceSensors
                for (var j = 0; j < deviceContext.deviceSensors.length; j++) {
                    if (device.deviceSensors === deviceContext.deviceSensors[j]) {
                        deviceContext.deviceSensors.splice(j, 1);
                    }
                }
            }
        });

        deviceContext.$watchCollection('deviceSensors', function (newValues, oldValues) {
            //inserindo
            for (var i = 0; i < newValues.length; i++) {
                var deviceSensors = newValues[i];
                //sensorInDevice
                for (var j = 0; j < deviceSensors.sensorInDevice.length; j++) {
                    var sensorInDevice = deviceSensors.sensorInDevice[j];
                    sensorInDevice.deviceSensors = deviceSensors;
                    deviceContext.sensorInDevice.push(sensorInDevice);
                }
            }
            //removendo
            for (var i = 0; i < oldValues.length; i++) {
                var deviceSensors = oldValues[i];
                //sensorInDevice
                for (var j = 0; j < deviceSensors.sensorInDevice.length; j++) {
                    var sensorInDevice = deviceSensors.sensorInDevice[j];
                    for (var k = 0; k < deviceContext.sensorInDevice.length; k++) {
                        if (sensorInDevice === deviceContext.sensorInDevice[k]) {
                            deviceContext.sensorInDevice.splice(k, 1);
                        }
                    }
                }
            }
        });

        deviceContext.$watchCollection('deviceNTP', function (newValues, oldValues) {
            //inserindo
            for (var i = 0; i < newValues.length; i++) {
                var deviceNTP = newValues[i];
                //timeZone
                if (globalizationContext.timeZoneLoaded) {
                    setTimeZoneInDeviceNTP(deviceNTP);
                }
            }
            //removendo
            for (var i = 0; i < oldValues.length; i++) {
                var deviceNTP = oldValues[i];
                //timeZone
                var timeZone = globalizationFinder.getTimeZoneByKey(deviceNTP.timeZone.timeZoneId);
                for (var j = 0; j < timeZone.devicesNTP.length; j++) {
                    if (deviceNTP === timeZone.devicesNTP[j]) {
                        timeZone.devicesNTP.splice(j, 1);
                    }
                }
            }
        });

        deviceContext.$watchCollection('sensorInDevice', function (newValues, oldValues) {
            //inserindo
            for (var i = 0; i < newValues.length; i++) {
                var sensorInDevice = newValues[i];
                //sensor
                setSensorInDeviceInSensor(sensorInDevice);
            }
            //removendo
            for (var i = 0; i < oldValues.length; i++) {
                var sensorInDevice = oldValues[i];
                //sensor
                if (sensorInDevice.sensor) {
                    var sensor = sensorInDevice.sensor;
                    delete sensorInDevice.sensor;
                    delete sensor.sensorInDevice;
                }
            }
        });

        sensorContext.$watchCollection('sensor', function (newValues, oldValues) {
            //inserindo
            for (var i = 0; i < newValues.length; i++) {
                var sensor = newValues[i];
                for (var j = 0; j < deviceContext.sensorInDevice.length; j++) {
                    var sensorInDevice = deviceContext.sensorInDevice[j];
                    if (sensor.sensorId === sensorInDevice.sensorId) {
                        if (!sensor.sensorInDevice) sensor.sensorInDevice = sensorInDevice;
                        if (!sensorInDevice.sensor) sensorInDevice.sensor = sensor;
                        break;
                    }
                }
            }
            //removendo
            for (var i = 0; i < oldValues.length; i++) {
                var sensor = oldValues[i];
                if (sensor.sensorInDevice) {
                    var sensorInDevice = sensor.sensorInDevice;
                    delete sensorInDevice.sensor;
                    delete sensor.sensorInDevice;
                }
            }
        });

        var setTimeZoneInDeviceNTP = function (deviceNTP) {
            if (deviceNTP.timeZone) return;
            var timeZone = globalizationFinder.getTimeZoneByKey(deviceNTP.timeZoneId);
            deviceNTP.timeZone = timeZone;
            delete deviceNTP.timeZoneId; // removendo a foreing key
            if (timeZone.devicesNTP === undefined) {
                timeZone.devicesNTP = [];
            }
            timeZone.devicesNTP.push(deviceNTP);
        }

        var setSensorInDeviceInSensor = function (sensorInDevice) {
            if (sensorInDevice.sensor || !sensorContext.sensorLoaded) return;
            var sensor = sensorFinder.getSensorByKey(sensorInDevice.sensorId);
            if (sensor) {
                if (!sensor.sensorInDevice) sensor.sensorInDevice = sensorInDevice;
                if (!sensorInDevice.sensor) sensorInDevice.sensor = sensor;
            }
        }

        // *** Navigation Properties Mappers ***        

        var mapper_DeviceNTP_TimeZone_Init = false;
        var mapper_DeviceNTP_TimeZone = function () {
            if (!mapper_DeviceNTP_TimeZone_Init && deviceContext.deviceLoaded && globalizationContext.timeZoneLoaded) {
                mapper_DeviceNTP_TimeZone_Init = true;
                timeZoneLoadedUnbinding();
                for (var i = 0; i < deviceContext.deviceNTP.length; i++) {
                    setTimeZoneInDeviceNTP(deviceContext.deviceNTP[i]);
                }
            }
        };

        var mapper_SensorInDevice_Sensor_Init = false;
        var mapper_SensorInDevice_Sensor = function () {
            if (!mapper_SensorInDevice_Sensor_Init && deviceContext.sensorInDeviceLoaded && sensorContext.sensorLoaded) {
                mapper_SensorInDevice_Sensor_Init = true;
                sensorLoadedUnbinding();
                for (var i = 0; i < deviceContext.sensorInDevice.length; i++) {
                    setSensorInDeviceInSensor(deviceContext.sensorInDevice[i]);
                }
            }
        };

        // *** Navigation Properties Mappers ***


        // *** Events Subscriptions

        var onDeviceGetAllByApplicationIdCompleted = function (event, data) {

            deviceGetAllByApplicationIdCompletedSubscription();

            deviceContext.deviceLoaded = true;
            deviceContext.deviceNTPLoaded = true;
            deviceContext.deviceSensorsLoaded = true;
            deviceContext.sensorInDeviceLoaded = true;
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