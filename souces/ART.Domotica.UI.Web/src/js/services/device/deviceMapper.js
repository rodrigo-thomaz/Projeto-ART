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
    'deviceSerialFinder',
    'sensorInDeviceFinder',
    'deviceSensorFinder',
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
        deviceSerialFinder,
        sensorInDeviceFinder,
        deviceSensorFinder,
        deviceDatasheetFinder) {

        var serviceFactory = {};

        var addDeviceAggregates = function (device) {
            //deviceWiFi
            var deviceWiFi = device.deviceWiFi;
            deviceContext.deviceWiFi.push(deviceWiFi);
            //deviceNTP
            var deviceNTP = device.deviceNTP;
            deviceContext.deviceNTP.push(deviceNTP);
            //deviceSerial
            var deviceSerial = device.deviceSerial;
            for (var i = 0; i < deviceSerial.length; i++) {
                deviceContext.deviceSerial.push(deviceSerial[i]);
            }
            deviceContext.deviceSerial.push(deviceSerial);
            //deviceDebug
            var deviceDebug = device.deviceDebug;
            deviceContext.deviceDebug.push(deviceDebug);
            //deviceSensor
            var deviceSensor = device.deviceSensor;
            deviceContext.deviceSensor.push(deviceSensor);
            //sensorInDevice
            var sensorInDevice = deviceSensor.sensorInDevice;            
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
            //deviceSerial
            var deviceSerial = device.deviceSerial;
            for (var i = 0; i < deviceSerial.length; i++) {
                var item = deviceSerial[i];
                for (var j = 0; j < deviceContext.deviceSerial.length; j++) {
                    if (item === deviceContext.deviceSerial[j]) {
                        deviceContext.deviceSerial.splice(j, 1);
                        break;
                    }
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
            //deviceSensor
            var deviceSensor = device.deviceSensor;
            for (var i = 0; i < deviceContext.deviceSensor.length; i++) {
                if (deviceSensor === deviceContext.deviceSensor[i]) {
                    deviceContext.deviceSensor.splice(i, 1);
                    break;
                }
            }
            //sensorInDevice
            var sensorsInDevices = deviceSensor.sensorInDevice;
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
                device.deviceDatasheet = function () { return deviceDatasheetFinder.getByKey(this.deviceTypeId, this.deviceDatasheetId); }
                addDeviceAggregates(device);
            }            
        });

        deviceContext.$watchCollection('deviceWiFi', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceWiFi = newValues[i];
                deviceWiFi.device = function () { return deviceFinder.getByKey(this.deviceTypeId, this.deviceDatasheetId, this.deviceId); }
            }
        });

        deviceContext.$watchCollection('deviceNTP', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceNTP = newValues[i];
                deviceNTP.timeZone = function () { return timeZoneFinder.getByKey(this.timeZoneId); }
                deviceNTP.device = function () { return deviceFinder.getByKey(this.deviceTypeId, this.deviceDatasheetId, this.deviceId); }
            }
        });

        deviceContext.$watchCollection('deviceSerial', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceSerial = newValues[i];
                deviceSerial.device = function () { return deviceFinder.getByKey(this.deviceTypeId, this.deviceDatasheetId, this.deviceId); }
            }
        });

        deviceContext.$watchCollection('deviceDebug', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceDebug = newValues[i];
                deviceDebug.device = function () { return deviceFinder.getByKey(this.deviceTypeId, this.deviceDatasheetId, this.deviceId); }
            }
        });

        deviceContext.$watchCollection('deviceSensor', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceSensor = newValues[i];
                deviceSensor.device = function () { return deviceFinder.getByKey(this.deviceTypeId, this.deviceDatasheetId, this.deviceId); }
            }
        });        

        deviceContext.$watchCollection('sensorInDevice', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var sensorInDevice = newValues[i];
                sensorInDevice.deviceSensor = function () { return deviceSensorFinder.getByKey(this.deviceTypeId, this.deviceDatasheetId, this.deviceId); }
                sensorInDevice.sensor = function () { return sensorFinder.getByKey(this.sensorId, this.sensorDatasheetId, this.sensorTypeId); }
            }
        });


        // *** Events Subscriptions

        var onDeviceGetAllByApplicationIdCompleted = function (event, data) {

            deviceGetAllByApplicationIdCompletedSubscription();

            deviceContext.deviceLoaded = true;
            deviceContext.deviceWiFiLoaded = true;
            deviceContext.deviceNTPLoaded = true;
            deviceContext.deviceSerialLoaded = true;
            deviceContext.deviceDebugLoaded = true;
            deviceContext.deviceSensorLoaded = true;
            deviceContext.sensorInDeviceLoaded = true;
        }

        var deviceGetAllByApplicationIdCompletedSubscription = $rootScope.$on(deviceConstant.getAllByApplicationIdCompletedEventName, onDeviceGetAllByApplicationIdCompleted);

        $rootScope.$on('$destroy', function () {
            deviceGetAllByApplicationIdCompletedSubscription();
        });

        // *** Events Subscriptions
       

        return serviceFactory;

    }]);