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
    'deviceNTPFinder',
    'sensorInDeviceFinder',
    'deviceSensorsFinder',
    function (
        $rootScope,
        deviceContext,
        deviceConstant,
        globalizationContext,
        timeZoneFinder,
        sensorContext,
        deviceFinder,
        sensorFinder,
        deviceNTPFinder,
        sensorInDeviceFinder,
        deviceSensorsFinder) {

        var serviceFactory = {};

        var addDeviceAggregates = function (device) {
            //deviceNTP
            var deviceNTP = null;
            if (angular.isObject(device.deviceNTP))
                deviceNTP = device.deviceNTP;
            else if (angular.isFunction(device.deviceNTP)) 
                deviceNTP = device.deviceNTP();
            deviceContext.deviceNTP.push(deviceNTP);
            //deviceSensors
            var deviceSensors = null;
            if (angular.isObject(device.deviceSensors))
                deviceSensors = device.deviceSensors;
            else if (angular.isFunction(device.deviceSensors))
                deviceSensors = device.deviceSensors();
            deviceContext.deviceSensors.push(deviceSensors);
            //sensorInDevice
            var sensorInDevice = null;
            if (angular.isObject(deviceSensors.sensorInDevice))
                sensorInDevice = deviceSensors.sensorInDevice;
            else if (angular.isFunction(deviceSensors.sensorInDevice))
                sensorInDevice = deviceSensors.sensorInDevice();
            for (var i = 0; i < sensorInDevice.length; i++) {
                deviceContext.sensorInDevice.push(sensorInDevice[i]);
            }
        }

        var removeDeviceAggregates = function (device) {
            //deviceNTP
            for (var i = 0; i < deviceContext.deviceNTP.length; i++) {
                if (device.deviceNTP === deviceContext.deviceNTP[i]) {
                    deviceContext.deviceNTP.splice(i, 1);
                    break;
                }
            }            
            //deviceSensors
            for (var i = 0; i < deviceContext.deviceSensors.length; i++) {
                if (device.deviceSensors === deviceContext.deviceSensors[i]) {
                    deviceContext.deviceSensors.splice(i, 1);
                    break;
                }
            }
            //sensorInDevice
            var sensorsInDevices = device.deviceSensors().sensorInDevice();
            for (var i = 0; i < sensorsInDevices.length; i++) {
                var sensorInDevice = sensorsInDevices[i];
                for (var j = 0; j < deviceContext.sensorInDevice.length; j++) {
                    if (sensorInDevice === deviceContext.sensorInDevice[j]) {
                        deviceContext.sensorInDevice.splice(j, 1);
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
                addDeviceAggregates(device);
                device.deviceNTP = function () { return deviceNTPFinder.getByKey(this.deviceId); }
                device.deviceSensors = function () { return deviceSensorsFinder.getByKey(this.deviceId); }
            }            
        });

        deviceContext.$watchCollection('deviceNTP', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceNTP = newValues[i];
                deviceNTP.timeZone = function () { return timeZoneFinder.getByKey(this.timeZoneId); }
                deviceNTP.device = function () { return deviceFinder.getByKey(this.deviceNTPId); }
            }
        });

        deviceContext.$watchCollection('deviceSensors', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceSensors = newValues[i];
                deviceSensors.device = function () { return deviceFinder.getByKey(this.deviceSensorsId); }
                deviceSensors.sensorInDevice = function () { return sensorInDeviceFinder.getByDeviceSensorsKey(this.deviceSensorsId); }
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
            deviceContext.deviceNTPLoaded = true;
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