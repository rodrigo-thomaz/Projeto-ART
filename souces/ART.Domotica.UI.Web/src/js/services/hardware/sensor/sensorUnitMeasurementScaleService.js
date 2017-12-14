'use strict';
app.factory('sensorUnitMeasurementScaleService', ['$http', '$log', '$rootScope', 'ngAuthSettings', 'stompService', 'unitMeasurementConverter', 'sensorUnitMeasurementScaleConstant', 'sensorTempDSFamilyFinder',
    function ($http, $log, $rootScope, ngAuthSettings, stompService, unitMeasurementConverter, sensorUnitMeasurementScaleConstant, sensorTempDSFamilyFinder) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setUnitMeasurementNumericalScaleTypeCountrySubscription = null;
        var setValueSubscription = null;

        var onConnected = function () {
            setUnitMeasurementNumericalScaleTypeCountrySubscription = stompService.subscribeAllViews(sensorUnitMeasurementScaleConstant.setUnitMeasurementNumericalScaleTypeCountryCompletedTopic, onSetUnitMeasurementNumericalScaleTypeCountryCompleted);
            setValueSubscription = stompService.subscribeAllViews(sensorUnitMeasurementScaleConstant.setValueCompletedTopic, onSetValueCompleted);
        }

        var setUnitMeasurementNumericalScaleTypeCountry = function (sensorUnitMeasurementScaleId, sensorDatasheetId, sensorTypeId, unitMeasurementId, unitMeasurementTypeId, numericalScalePrefixId, numericalScaleTypeId, countryId) {
            var data = {
                sensorUnitMeasurementScaleId: sensorUnitMeasurementScaleId,
                sensorDatasheetId: sensorDatasheetId,
                sensorTypeId: sensorTypeId,
                unitMeasurementId: unitMeasurementId,
                unitMeasurementTypeId: unitMeasurementTypeId,
                numericalScalePrefixId: numericalScalePrefixId,
                numericalScaleTypeId: numericalScaleTypeId,
                countryId: countryId,
            }
            return $http.post(serviceBase + sensorUnitMeasurementScaleConstant.setUnitMeasurementNumericalScaleTypeCountryApiUri, data).then(function (results) {
                return results;
            });
        };    

        var setValue = function (sensorUnitMeasurementScaleId, value, position) {
            var data = {
                sensorUnitMeasurementScaleId: sensorUnitMeasurementScaleId,
                value: value,
                position: position,
            }
            return $http.post(serviceBase + sensorUnitMeasurementScaleConstant.setValueApiUri, data).then(function (results) {
                return results;
            });
        };        

        var onSetUnitMeasurementNumericalScaleTypeCountryCompleted = function (payload) {
            //var result = JSON.parse(payload.body);
            //var sensor = sensorTempDSFamilyFinder.getByKey(result.sensorUnitMeasurementScaleId, result.sensorDatasheetId, result.sensorTypeId);
            //if (result.position === 'Max') {
            //    sensor.sensorUnitMeasurementScale.max = result.value;
            //    sensor.sensorUnitMeasurementScale.maxConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.sensorUnitMeasurementScale.max);
            //}
            //else if (result.position === 'Min') {
            //    sensor.sensorUnitMeasurementScale.min = result.value;
            //    sensor.sensorUnitMeasurementScale.minConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.sensorUnitMeasurementScale.min);
            //}
            //$rootScope.$emit(sensorUnitMeasurementScaleConstant.setValueCompletedEventName + result.sensorUnitMeasurementScaleId, result);
        }

        var onSetValueCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensor = sensorTempDSFamilyFinder.getByKey(result.sensorUnitMeasurementScaleId, result.sensorDatasheetId, result.sensorTypeId);
            if (result.position === 'Max') {
                sensor.sensorUnitMeasurementScale.max = result.value;
                sensor.sensorUnitMeasurementScale.maxConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.sensorUnitMeasurementScale.max);
            }
            else if (result.position === 'Min') {
                sensor.sensorUnitMeasurementScale.min = result.value;
                sensor.sensorUnitMeasurementScale.minConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.sensorUnitMeasurementScale.min);
            }
            $rootScope.$emit(sensorUnitMeasurementScaleConstant.setValueCompletedEventName + result.sensorUnitMeasurementScaleId, result);
        }

        //var onSetSensorUnitMeasurementScaleCompleted = function (payload) {

        //    var result = JSON.parse(payload.body);
        //    var sensor = getByKey(result.deviceId, result.sensorTempDSFamilyId);

        //    //unitMeasurement
        //    sensor.unitMeasurementId = result.unitMeasurementId;
        //    sensor.unitMeasurement = unitMeasurementService.getByKey(sensor.unitMeasurementId);

        //    //temp
        //    sensor.tempConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.tempCelsius);

        //    //sensorUnitMeasurementScale
        //    sensor.sensorUnitMeasurementScale.maxConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.sensorUnitMeasurementScale.max);
        //    sensor.sensorUnitMeasurementScale.minConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.sensorUnitMeasurementScale.min);

        //    //alarms
        //    sensor.highAlarm.alarmConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.highAlarm.alarmCelsius);
        //    sensor.lowAlarm.alarmConverted = unitMeasurementConverter.convertFromCelsius(sensor.unitMeasurementId, sensor.lowAlarm.alarmCelsius);

        //    $rootScope.$emit(sensorConstant.setSensorUnitMeasurementScaleCompletedEventName + result.sensorTempDSFamilyId, result);
        //}    

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setUnitMeasurementNumericalScaleTypeCountrySubscription.unsubscribe();
            setValueSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);       

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.setUnitMeasurementNumericalScaleTypeCountry = setUnitMeasurementNumericalScaleTypeCountry;
        serviceFactory.setValue = setValue;

        return serviceFactory;

    }]);