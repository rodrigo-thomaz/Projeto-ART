'use strict';
app.factory('deviceSensorService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', 'deviceSensorConstant', 'deviceSensorFinder', 'sensorConstant',
    function ($http, ngAuthSettings, $rootScope, stompService, deviceContext, deviceSensorConstant, deviceSensorFinder, sensorConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var messageIoTReceivedSubscription = null;
        var setReadIntervalInMilliSecondsCompletedSubscription = null;
        var setPublishIntervalInMilliSecondsCompletedSubscription = null;

        var setReadIntervalInMilliSeconds = function (deviceTypeId, deviceDatasheetId, deviceId, readIntervalInMilliSeconds) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                intervalInMilliSeconds: readIntervalInMilliSeconds,
            }
            return $http.post(serviceBase + deviceSensorConstant.setReadIntervalInMilliSecondsApiUri, data).then(function (results) {
                return results;
            });
        };

        var setPublishIntervalInMilliSeconds = function (deviceTypeId, deviceDatasheetId, deviceId, publishIntervalInMilliSeconds) {
            var data = {
                deviceTypeId: deviceTypeId,
                deviceDatasheetId: deviceDatasheetId,
                deviceId: deviceId,
                intervalInMilliSeconds: publishIntervalInMilliSeconds,
            }
            return $http.post(serviceBase + deviceSensorConstant.setPublishIntervalInMilliSecondsApiUri, data).then(function (results) {
                return results;
            });
        };

        var onConnected = function () {
            messageIoTReceivedSubscription = stompService.subscribeDevice(deviceSensorConstant.messageIoTTopic, onMessageIoTReceived);            
            setReadIntervalInMilliSecondsCompletedSubscription = stompService.subscribeAllViews(deviceSensorConstant.setReadIntervalInMilliSecondsCompletedTopic, onSetReadIntervalInMilliSecondsCompleted);
            setPublishIntervalInMilliSecondsCompletedSubscription = stompService.subscribeAllViews(deviceSensorConstant.setPublishIntervalInMilliSecondsCompletedTopic, onSetPublishIntervalInMilliSecondsCompleted);
        }

        var onMessageIoTReceived = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            var deviceSensor = deviceSensorFinder.getByKey(data.deviceTypeId, data.deviceDatasheetId, data.deviceId);
            if(angular.isUndefined(deviceSensor)) return;
            deviceSensor.epochTimeUtc = data.epochTimeUtc;   
            updateSensorsInDevice(deviceSensor.sensorInDevice, data.sensorsInDevice);
            deviceContext.$digest();
            $rootScope.$emit(deviceSensorConstant.messageIoTEventName + data.deviceId, data);
        }

        var updateSensorsInDevice = function (oldValues, newValues) {
            for (var i = 0; i < oldValues.length; i++) {
                for (var j = 0; j < newValues.length; j++) {
                    if (oldValues[i].sensorTypeId === newValues[j].sensorTypeId && oldValues[i].sensorDatasheetId === newValues[j].sensorDatasheetId && oldValues[i].sensorId === newValues[j].sensorId) {
                        var sensor = oldValues[i].sensor();
                        sensor.isConnected = newValues[j].isConnected;
                        sensor.value = newValues[j].value;
                        $rootScope.$emit(sensorConstant.messageIoTEventName + sensor.sensorId);
                        break;
                    }
                }
            }
        }

        var onSetReadIntervalInMilliSecondsCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceSensor = deviceSensorFinder.getByKey(result.deviceTypeId, result.deviceDatasheetId, result.deviceId);
            deviceSensor.readIntervalInMilliSeconds = result.readIntervalInMilliSeconds;
            deviceContext.$digest();
            $rootScope.$emit(deviceSensorConstant.setReadIntervalInMilliSecondsCompletedEventName + result.deviceId, result);
        };

        var onSetPublishIntervalInMilliSecondsCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var deviceSensor = deviceSensorFinder.getByKey(result.deviceTypeId, result.deviceDatasheetId, result.deviceId);
            deviceSensor.publishIntervalInMilliSeconds = result.publishIntervalInMilliSeconds;
            deviceContext.$digest();
            $rootScope.$emit(deviceSensorConstant.setPublishIntervalInMilliSecondsCompletedEventName + result.deviceId, result);
        };

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            messageIoTReceivedSubscription.unsubscribe();
            setReadIntervalInMilliSecondsCompletedSubscription.unsubscribe();
            setPublishIntervalInMilliSecondsCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.setReadIntervalInMilliSeconds = setReadIntervalInMilliSeconds;
        serviceFactory.setPublishIntervalInMilliSeconds = setPublishIntervalInMilliSeconds;

        return serviceFactory;

        var updateSensors1 = function (device, newSensors) {
            var oldSensors = device.sensors;
            for (var i = 0; i < oldSensors.length; i++) {
                for (var j = 0; j < newSensors.length; j++) {
                    if (oldSensors[i].sensorTempDSFamilyId === newSensors[j].sensorTempDSFamilyId) {

                        oldSensors[i].isConnected = newSensors[j].isConnected;

                        //Temp
                        oldSensors[i].tempCelsius = newSensors[j].tempCelsius;
                        //oldSensors[i].tempConverted = unitMeasurementConverter.convertFromCelsius(oldSensors[i].unitMeasurementId, oldSensors[i].tempCelsius);

                        //Chart

                        oldSensors[i].chart[1].key = 'Temperatura ' + oldSensors[i].tempCelsius + ' °C';

                        oldSensors[i].chart[0].values.push({
                            epochTime: device.epochTimeUtc,
                            temperature: oldSensors[i].highAlarm.alarmCelsius,
                        });

                        oldSensors[i].chart[1].values.push({
                            epochTime: device.epochTimeUtc,
                            temperature: oldSensors[i].tempCelsius,
                        });

                        oldSensors[i].chart[2].values.push({
                            epochTime: device.epochTimeUtc,
                            temperature: oldSensors[i].lowAlarm.alarmCelsius,
                        });

                        if (oldSensors[i].chart[0].values.length > 60)
                            oldSensors[i].chart[0].values.shift();

                        if (oldSensors[i].chart[1].values.length > 60)
                            oldSensors[i].chart[1].values.shift();

                        if (oldSensors[i].chart[2].values.length > 60)
                            oldSensors[i].chart[2].values.shift();

                        break;
                    }
                }
            }
        }

        var chartLine = function (key) {
            this.key = key;
            this.values = [];
        }

        var insertDeviceInCollection = function (device) {
            //device.createDate = new Date(device.createDate * 1000).toLocaleString();
            //deviceContext.device.push(device);
            //for (var i = 0; i < device.sensors.length; i++) {

            //var sensor = device.sensors[i];

            //temp
            //sensor.tempConverted = null;

            //unitMeasurement

            // Arrumar aqui !!!
            //sensor.unitMeasurement = siContext.getUnitMeasurementScaleByKey(sensor.unitMeasurementId);

            //sensorUnitMeasurementScale
            //sensor.sensorUnitMeasurementScale.maxConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.sensorUnitMeasurementScale.max);
            //sensor.sensorUnitMeasurementScale.minConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.sensorUnitMeasurementScale.min);

            //alarms
            //sensor.highAlarm.alarmConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.highAlarm.alarmCelsius);
            //sensor.lowAlarm.alarmConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.lowAlarm.alarmCelsius);

            //Chart
            //sensor.chart = [];
            //sensor.chart.push(new chartLine("Máximo"));
            //sensor.chart.push(new chartLine("Temperatura"));
            //sensor.chart.push(new chartLine("Mínimo"));
            //}
        }

    }]);