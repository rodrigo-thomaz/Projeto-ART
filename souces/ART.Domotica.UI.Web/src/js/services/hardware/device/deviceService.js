'use strict';
app.factory('deviceService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceConstant', 'deviceContext',
    function ($http, $log, ngAuthSettings, $rootScope, stompService, deviceConstant, deviceContext) {

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var serviceFactory = {};

        var _initializing = false;
        var _initialized = false;

        var getAllByApplicationIdCompletedSubscription = null;
        var insertInApplicationCompletedSubscription = null;
        var deleteFromApplicationCompletedSubscription = null;
        var getByPinCompletedSubscription = null;
        var setLabelCompletedSubscription = null;

        var onConnected = function () {

            getAllByApplicationIdCompletedSubscription = stompService.subscribe(deviceConstant.getAllByApplicationIdCompletedTopic, onGetAllByApplicationIdCompleted);
            insertInApplicationCompletedSubscription = stompService.subscribeAllViews(deviceConstant.insertInApplicationCompletedTopic, onInsertInApplicationCompleted);
            deleteFromApplicationCompletedSubscription = stompService.subscribeAllViews(deviceConstant.deleteFromApplicationCompletedTopic, onDeleteFromApplicationCompleted);
            getByPinCompletedSubscription = stompService.subscribe(deviceConstant.getByPinCompletedTopic, onGetByPinCompleted);
            setLabelCompletedSubscription = stompService.subscribeAllViews(deviceConstant.setLabelCompletedTopic, onSetLabelCompleted);

            stompService.client.subscribe('/topic/ARTPUBTEMP', onReadReceived);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAllByApplicationId();
            }
        }

        var initialized = function () {
            return _initialized;
        };

        var getAllByApplicationId = function () {
            return $http.post(serviceBase + deviceConstant.getAllByApplicationIdApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var getByPin = function (pin) {
            var data = {
                pin: pin
            };
            return $http.post(serviceBase + deviceConstant.getByPinApiUri, data).then(function successCallback(response) {
                //alert('envio bem sucedido');
            });
        };

        var insertInApplication = function (pin) {
            var data = {
                pin: pin
            };
            return $http.post(serviceBase + deviceConstant.insertInApplicationApiUri, data).then(function successCallback(response) {
                //alert('envio bem sucedido');
            });
        };

        var deleteFromApplication = function (hardwareInApplicationId) {
            var data = {
                hardwareInApplicationId: hardwareInApplicationId
            };
            return $http.post(serviceBase + deviceConstant.deleteFromApplicationApiUri, data).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var setLabel = function (deviceId, label) {
            var data = {
                deviceId: deviceId,
                label: label,
            }
            return $http.post(serviceBase + deviceConstant.setLabelApiUri, data).then(function (results) {
                return results;
            });
        };

        var onReadReceived = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            for (var i = 0; i < deviceContext.devices.length; i++) {
                var device = deviceContext.devices[i];
                if (device.hardwareId === data.hardwareId) {
                    device.epochTimeUtc = data.epochTimeUtc;
                    device.wifiQuality = data.wifiQuality;
                    device.localIPAddress = data.localIPAddress;
                    updateSensors(device, data.sensorTempDSFamilies);
                    break;
                }
            }
            $rootScope.$emit('ESPDeviceService_onReadReceived');
        }

        var updateSensors = function (device, newSensors) {
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

        var onGetAllByApplicationIdCompleted = function (payload) {

            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);

            for (var i = 0; i < data.length; i++) {
                deviceContext.device.push(data[i]);
            }

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllByApplicationIdCompletedSubscription.unsubscribe();

            $rootScope.$emit(deviceConstant.getAllByApplicationIdCompletedEventName);
        }

        var onGetByPinCompleted = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            $rootScope.$emit(deviceConstant.getByPinCompletedEventName, data);
        }

        var onInsertInApplicationCompleted = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            insertDeviceInCollection(data);
            $rootScope.$emit(deviceConstant.insertInApplicationCompletedEventName);
        }

        var onDeleteFromApplicationCompleted = function (payload) {
            var dataUTF8 = decodeURIComponent(escape(payload.body));
            var data = JSON.parse(dataUTF8);
            for (var i = 0; i < deviceContext.devices.length; i++) {
                if (deviceContext.devices[i].hardwareInApplicationId === data.hardwareInApplicationId) {
                    deviceContext.devices.splice(i, 1);
                    $rootScope.$emit(deviceConstant.deleteFromApplicationCompletedEventName);
                    break;
                }
            }
        }

        var onSetLabelCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var device = deviceContext.getDeviceById(result.deviceId);
            device.label = result.label;
            $rootScope.$emit(deviceConstant.setLabelCompletedEventName + result.deviceId, result);
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

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.initialized = initialized;

        serviceFactory.getByPin = getByPin;
        serviceFactory.insertInApplication = insertInApplication;
        serviceFactory.deleteFromApplication = deleteFromApplication;
        serviceFactory.setLabel = setLabel;

        return serviceFactory;

    }]);