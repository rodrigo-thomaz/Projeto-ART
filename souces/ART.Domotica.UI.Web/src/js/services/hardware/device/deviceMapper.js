'use strict';
app.factory('deviceMapper', [
    '$rootScope',
    'deviceContext',
    'deviceConstant',
    'globalizationContext',
    'timeZoneFinder',
    'sensorContext',
    'sensorFinder',
    function (
        $rootScope,
        deviceContext,
        deviceConstant,
        globalizationContext,
        timeZoneFinder,
        sensorContext,
        sensorFinder) {

        var serviceFactory = {};

        var addDeviceAggregates = function (device) {
            //deviceNTP
            deviceContext.deviceNTP.push(device.deviceNTP);
            //deviceSensors
            deviceContext.deviceSensors.push(device.deviceSensors);
            //sensorInDevice
            for (var i = 0; i < device.deviceSensors.sensorInDevice.length; i++) {
                deviceContext.sensorInDevice.push(device.deviceSensors.sensorInDevice[i]);
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
            for (var i = 0; i < device.deviceSensors.sensorInDevice.length; i++) {
                var sensorInDevice = device.deviceSensors.sensorInDevice[i];
                for (var j = 0; j < deviceContext.sensorInDevice.length; j++) {
                    if (sensorInDevice === deviceContext.sensorInDevice[j]) {
                        deviceContext.sensorInDevice.splice(j, 1);
                    }
                }
            }
        }

        deviceContext.$watchCollection('device', function (newValues, oldValues) {
            //inserindo
            for (var i = 0; i < newValues.length; i++) {
                addDeviceAggregates(newValues[i]);
            }
            //removendo
            for (var i = 0; i < oldValues.length; i++) {
                removeDeviceAggregates(oldValues[i]);
            }
        });

        deviceContext.$watchCollection('deviceNTP', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceNTP = newValues[i];
                deviceNTP.timeZone = function () { return timeZoneFinder.getByKey(this.timeZoneId); }
            }
        });

        deviceContext.$watchCollection('deviceSensors', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceSensors = newValues[i];

            }
        });        

        deviceContext.$watchCollection('sensorInDevice', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var sensorInDevice = newValues[i];

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