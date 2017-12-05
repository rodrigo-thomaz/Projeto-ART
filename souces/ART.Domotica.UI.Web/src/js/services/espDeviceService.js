'use strict';
app.factory('espDeviceService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', 'contextScope', function ($http, $log, ngAuthSettings, $rootScope, stompService, contextScope) {
    
    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};    

    var _initializing = false;
    var _initialized = false;
    
    var getAllByApplicationIdApiUri = 'api/espDevice/getAllByApplicationId';
    var getAllByApplicationIdCompletedTopic = 'ESPDevice.GetAllByApplicationIdViewCompleted';
    var getAllByApplicationIdCompletedSubscription = null;
    var getAllByApplicationIdCompletedEventName = 'espDeviceService.onGetAllByApplicationIdCompleted_Id_';

    var insertInApplicationApiUri = 'api/espDevice/insertInApplication';
    var insertInApplicationCompletedTopic = 'ESPDevice.InsertInApplicationViewCompleted';
    var insertInApplicationCompletedSubscription = null;
    var insertInApplicationCompletedEventName = 'espDeviceService.onInsertInApplicationCompleted';

    var deleteFromApplicationApiUri = 'api/espDevice/deleteFromApplication';
    var deleteFromApplicationCompletedTopic = 'ESPDevice.DeleteFromApplicationViewCompleted';
    var deleteFromApplicationCompletedSubscription = null;
    var deleteFromApplicationCompletedEventName = 'espDeviceService.onDeleteFromApplicationCompleted';

    var getByPinApiUri = 'api/espDevice/getByPin';
    var getByPinCompletedTopic = 'ESPDevice.GetByPinViewCompleted';
    var getByPinCompletedSubscription = null;
    var getByPinCompletedEventName = 'espDeviceService.onGetByPinCompleted';

    var setLabelApiUri = 'api/espDevice/setLabel';
    var setLabelCompletedTopic = 'ESPDevice.SetLabelViewCompleted';
    var setLabelCompletedSubscription = null;
    var setLabelCompletedEventName = 'espDeviceService.onSetLabelCompleted_Id_';

    var initializedEventName = 'espDeviceService.onInitialized';

    var onConnected = function () {

        getAllByApplicationIdCompletedSubscription = stompService.subscribe(getAllByApplicationIdCompletedTopic, onGetAllByApplicationIdCompleted);
        insertInApplicationCompletedSubscription = stompService.subscribeAllViews(insertInApplicationCompletedTopic, onInsertInApplicationCompleted);
        deleteFromApplicationCompletedSubscription = stompService.subscribeAllViews(deleteFromApplicationCompletedTopic, onDeleteFromApplicationCompleted);
        getByPinCompletedSubscription = stompService.subscribe(getByPinCompletedTopic, onGetByPinCompleted);
        setLabelCompletedSubscription = stompService.subscribeAllViews(setLabelCompletedTopic, onSetLabelCompleted);

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
        return $http.post(serviceBase + getAllByApplicationIdApiUri).then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    var getByPin = function (pin) {
        var data = {
            pin: pin
        };
        return $http.post(serviceBase + getByPinApiUri, data).then(function successCallback(response) {
            //alert('envio bem sucedido');
        });
    };  

    var insertInApplication = function (pin) {
        var data = {
            pin: pin
        };
        return $http.post(serviceBase + insertInApplicationApiUri, data).then(function successCallback(response) {
            //alert('envio bem sucedido');
        });
    };     

    var deleteFromApplication = function (hardwareInApplicationId) {
        var data = {
            hardwareInApplicationId: hardwareInApplicationId
        };
        return $http.post(serviceBase + deleteFromApplicationApiUri, data).then(function (results) {
            //alert('envio bem sucedido');
        });
    };
    
    var setLabel = function (deviceId, label) {
        var data = {
            deviceId: deviceId,
            label: label,
        }
        return $http.post(serviceBase + setLabelApiUri, data).then(function (results) {
            return results;
        });
    };

    var onReadReceived = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < serviceFactory.devices.length; i++) {
            var device = serviceFactory.devices[i];
            if (device.hardwareInApplicationId === data.hardwareInApplicationId) {
                device.epochTimeUtc = data.epochTimeUtc;
                device.wifiQuality = data.wifiQuality;
                device.localIPAddress = data.localIPAddress;
                updateSensors(device, data.sensorTempDSFamilys);
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
            insertDeviceInCollection(data[i]);
        }

        _initializing = false;
        _initialized = true;

        contextScope.deviceLoaded = true;
        clearOnConnected();

        getAllByApplicationIdCompletedSubscription.unsubscribe();

        $rootScope.$emit(initializedEventName);
    }

    var onGetByPinCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        $rootScope.$emit(getByPinCompletedEventName, data);
    }

    var onInsertInApplicationCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        insertDeviceInCollection(data);
        $rootScope.$emit(insertInApplicationCompletedEventName);
    }  

    var onDeleteFromApplicationCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < contextScope.devices.length; i++) {
            if (contextScope.devices[i].hardwareInApplicationId === data.hardwareInApplicationId) {
                contextScope.devices.splice(i, 1);
                $rootScope.$emit(deleteFromApplicationCompletedEventName);
                break;
            }
        }       
    }
       
    var onSetLabelCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var device = contextScope.getDeviceById(result.deviceId);
        device.label = result.label;
        $rootScope.$emit(setLabelCompletedEventName + result.deviceId, result);
    }

    var insertDeviceInCollection = function (device) {
        device.createDate = new Date(device.createDate * 1000).toLocaleString();
        contextScope.devices.push(device);
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
    serviceFactory.initializedEventName = initializedEventName;

    serviceFactory.getByPin = getByPin;
    serviceFactory.insertInApplication = insertInApplication;    
    serviceFactory.deleteFromApplication = deleteFromApplication;
    serviceFactory.setLabel = setLabel;       

    serviceFactory.getAllByApplicationIdCompletedEventName = getAllByApplicationIdCompletedEventName;
    serviceFactory.insertInApplicationCompletedEventName = insertInApplicationCompletedEventName;
    serviceFactory.deleteFromApplicationCompletedEventName = deleteFromApplicationCompletedEventName;
    serviceFactory.getByPinCompletedEventName = getByPinCompletedEventName;
    serviceFactory.setLabelCompletedEventName = setLabelCompletedEventName;
    
    return serviceFactory;

}]);