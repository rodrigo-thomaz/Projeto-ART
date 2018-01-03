'use strict';
app.factory('deviceMapper', [
    '$rootScope',
    'deviceContext',
    'deviceConstant',
    'globalizationContext',
    'timeZoneFinder',
    'sensorContext',
    'deviceFinder',
    'sensorFinder',
    'deviceWiFiFinder',
    'deviceDebugFinder',
    'deviceNTPFinder',
    'sensorInDeviceFinder',
    'deviceSensorsFinder',
    'deviceDatasheetFinder',
    function (
        $rootScope,
        deviceContext,
        deviceConstant,
        globalizationContext,
        timeZoneFinder,
        sensorContext,
        deviceFinder,
        sensorFinder,
        deviceWiFiFinder,
        deviceDebugFinder,
        deviceNTPFinder,
        sensorInDeviceFinder,
        deviceSensorsFinder,
        deviceDatasheetFinder) {

        var serviceFactory = {};

        var addDeviceAggregates = function (device) {
            //deviceWiFi
            var deviceWiFi = device.deviceWiFi;
            deviceContext.deviceWiFi.push(deviceWiFi);
            //deviceNTP
            var deviceNTP = device.deviceNTP;
            deviceContext.deviceNTP.push(deviceNTP);
            //deviceDebug
            var deviceDebug = device.deviceDebug;
            deviceContext.deviceDebug.push(deviceDebug);
            //deviceSensors
            var deviceSensors = device.deviceSensors;
            deviceContext.deviceSensors.push(deviceSensors);
            //sensorInDevice
            var sensorInDevice = deviceSensors.sensorInDevice;            
            for (var i = 0; i < sensorInDevice.length; i++) {
                deviceContext.sensorInDevice.push(sensorInDevice[i]);
            }
        }

        var removeDeviceAggregates = function (device) {
            //deviceWiFi
            var deviceWiFi = device.deviceWiFi;
            for (var i = 0; i < deviceContext.deviceWiFi.length; i++) {
                if (deviceWiFi === deviceContext.deviceWiFi[i]) {
                    deviceContext.deviceWiFi.splice(i, 1);
                    break;
                }
            }
            //deviceNTP
            var deviceNTP = device.deviceNTP;
            for (var i = 0; i < deviceContext.deviceNTP.length; i++) {
                if (deviceNTP === deviceContext.deviceNTP[i]) {
                    deviceContext.deviceNTP.splice(i, 1);
                    break;
                }
            }
            //deviceDebug
            var deviceDebug = device.deviceDebug;
            for (var i = 0; i < deviceContext.deviceDebug.length; i++) {
                if (deviceDebug === deviceContext.deviceDebug[i]) {
                    deviceContext.deviceDebug.splice(i, 1);
                    break;
                }
            }
            //deviceSensors
            var deviceSensors = device.deviceSensors;
            for (var i = 0; i < deviceContext.deviceSensors.length; i++) {
                if (deviceSensors === deviceContext.deviceSensors[i]) {
                    deviceContext.deviceSensors.splice(i, 1);
                    break;
                }
            }
            //sensorInDevice
            var sensorsInDevices = deviceSensors.sensorInDevice;
            for (var i = 0; i < sensorsInDevices.length; i++) {
                var sensorInDevice = sensorsInDevices[i];
                for (var j = 0; j < deviceContext.sensorInDevice.length; j++) {
                    if (sensorInDevice === deviceContext.sensorInDevice[j]) {
                        deviceContext.sensorInDevice.splice(j, 1);
                        break;
                    }
                }
            }
        }

        deviceContext.$watchCollection('device', function (newValues, oldValues) {
            //removendo
            for (var i = 0; i < oldValues.length; i++) {
                removeDeviceAggregates(oldValues[i]);
            }
            //inserindo
            for (var i = 0; i < newValues.length; i++) {
                var device = newValues[i];
                device.deviceDatasheet = function () { return deviceDatasheetFinder.getByKey(this.deviceDatasheetId); }
                addDeviceAggregates(device);
            }            
        });

        deviceContext.$watchCollection('deviceWiFi', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceWiFi = newValues[i];
                deviceWiFi.device = function () { return deviceFinder.getByKey(this.deviceWiFiId, this.deviceDatasheetId); }
            }
        });

        deviceContext.$watchCollection('deviceNTP', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceNTP = newValues[i];
                deviceNTP.timeZone = function () { return timeZoneFinder.getByKey(this.timeZoneId); }
                deviceNTP.device = function () { return deviceFinder.getByKey(this.deviceNTPId, this.deviceDatasheetId); }
            }
        });

        deviceContext.$watchCollection('deviceDebug', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceDebug = newValues[i];
                deviceDebug.device = function () { return deviceFinder.getByKey(this.deviceDebugId, this.deviceDatasheetId); }
            }
        });

        deviceContext.$watchCollection('deviceSensors', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceSensors = newValues[i];
                deviceSensors.device = function () { return deviceFinder.getByKey(this.deviceSensorsId, this.deviceDatasheetId); }
            }
        });        

        deviceContext.$watchCollection('sensorInDevice', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var sensorInDevice = newValues[i];
                sensorInDevice.deviceSensors = function () { return deviceSensorFinder.getByKey(this.deviceSensorsId); }
                sensorInDevice.sensor = function () { return sensorFinder.getByKey(this.sensorId, this.sensorDatasheetId, this.sensorTypeId); }
            }
        });


        // *** Events Subscriptions

        var onDeviceGetAllByApplicationIdCompleted = function (event, data) {

            deviceGetAllByApplicationIdCompletedSubscription();

            deviceContext.deviceLoaded = true;
            deviceContext.deviceWiFiLoaded = true;
            deviceContext.deviceNTPLoaded = true;
            deviceContext.deviceDebugLoaded = true;
            deviceContext.deviceSensorsLoaded = true;
            deviceContext.sensorInDeviceLoaded = true;
        }

        var deviceGetAllByApplicationIdCompletedSubscription = $rootScope.$on(deviceConstant.getAllByApplicationIdCompletedEventName, onDeviceGetAllByApplicationIdCompleted);

        $rootScope.$on('$destroy', function () {
            deviceGetAllByApplicationIdCompletedSubscription();
        });

        // *** Events Subscriptions
       

        return serviceFactory;

    }]);