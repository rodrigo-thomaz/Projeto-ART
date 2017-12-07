'use strict';
app.factory('sensorService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'contextScope', 'sensorConstant', function ($http, ngAuthSettings, $rootScope, stompService, contextScope, sensorConstant) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};    

    var _initializing = false;
    var _initialized  = false;

    var getAllByApplicationIdCompletedSubscription = null;
    var setSensorUnitMeasurementScaleCompletedSubscription = null;
    var setLabelCompletedSubscription = null;

    var onConnected = function () {

        getAllByApplicationIdCompletedSubscription = stompService.subscribe(sensorConstant.getAllByApplicationIdCompletedTopic, onGetAllByApplicationIdCompleted);
        setSensorUnitMeasurementScaleCompletedSubscription = stompService.subscribeAllViews(sensorConstant.setSensorUnitMeasurementScaleCompletedTopic, onSetSensorUnitMeasurementScaleCompleted);
        setLabelCompletedSubscription = stompService.subscribeAllViews(sensorConstant.setLabelCompletedTopic, onSetLabelCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAllByApplicationId();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAllByApplicationId = function () {
        return $http.post(serviceBase + sensorConstant.getAllByApplicationIdApiUri).then(function (results) {
            //alert('envio bem sucedido');
        });
    };     

    var setSensorUnitMeasurementScale = function (sensorTempDSFamilyId, unitMeasurementId) {
        var data = {
            sensorTempDSFamilyId: sensorTempDSFamilyId,
            unitMeasurementId: unitMeasurementId,
        }
        return $http.post(serviceBase + sensorConstant.setUnitMeasurementApiUri, data).then(function (results) {
            return results;
        });
    };

    var setLabel = function (sensorTempDSFamilyId, label) {
        var data = {
            sensorTempDSFamilyId: sensorTempDSFamilyId,
            label: label,
        }
        return $http.post(serviceBase + sensorConstant.setLabelApiUri, data).then(function (results) {
            return results;
        });
    };  

    var onGetAllByApplicationIdCompleted = function (payload) {

        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);

        for (var i = 0; i < data.length; i++) {
            contextScope.sensors.push(data[i]);
        }

        _initializing = false;
        _initialized = true;

        contextScope.sensorsLoaded = true;
        clearOnConnected();

        getAllByApplicationIdCompletedSubscription.unsubscribe();

        $rootScope.$emit(sensorConstant.getAllByApplicationIdCompletedEventName);
    }

    var onSetSensorUnitMeasurementScaleCompleted = function (payload) {

        var result = JSON.parse(payload.body);
        var sensor = getByKey(result.deviceId, result.sensorTempDSFamilyId);

        //unitMeasurement
        sensor.unitMeasurementId = result.unitMeasurementId;
        sensor.unitMeasurement = unitMeasurementService.getByKey(sensor.unitMeasurementId);

        //temp
        sensor.tempConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.tempCelsius);

        //sensorUnitMeasurementScale
        sensor.sensorUnitMeasurementScale.maxConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.sensorUnitMeasurementScale.max);
        sensor.sensorUnitMeasurementScale.minConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.sensorUnitMeasurementScale.min);

        //alarms
        sensor.highAlarm.alarmConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.highAlarm.alarmCelsius);
        sensor.lowAlarm.alarmConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.lowAlarm.alarmCelsius);

        $rootScope.$emit(sensorConstant.setSensorUnitMeasurementScaleCompletedEventName + result.sensorTempDSFamilyId, result);
    }    

    var onSetLabelCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getByKey(result.deviceId, result.sensorTempDSFamilyId);
        sensor.label = result.label;
        $rootScope.$emit(sensorConstant.setLabelCompletedEventName + result.sensorTempDSFamilyId, result);
    }   

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);       

    // stompService
    if (stompService.connected()) onConnected();

    // serviceFactory
        
    serviceFactory.initialized = initialized;
        
    serviceFactory.setLabel = setLabel;   

    serviceFactory.setSensorUnitMeasurementScale = setSensorUnitMeasurementScale;

    return serviceFactory;

}]);