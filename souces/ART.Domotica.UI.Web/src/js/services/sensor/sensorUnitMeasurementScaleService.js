'use strict';
app.factory('sensorUnitMeasurementScaleService', ['$http', '$log', '$rootScope', 'ngAuthSettings', 'stompService', 'sensorContext', 'unitMeasurementConverter', 'sensorUnitMeasurementScaleConstant', 'sensorUnitMeasurementScaleFinder', 'sensorTempDSFamilyFinder',
    function ($http, $log, $rootScope, ngAuthSettings, stompService, sensorContext, unitMeasurementConverter, sensorUnitMeasurementScaleConstant, sensorUnitMeasurementScaleFinder, sensorTempDSFamilyFinder) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setDatasheetUnitMeasurementScaleSubscription = null;
        var setUnitMeasurementNumericalScaleTypeCountrySubscription = null;
        var setRangeSubscription = null;
        var setChartLimiterSubscription = null;

        var onConnected = function () {
            setDatasheetUnitMeasurementScaleSubscription = stompService.subscribeAllViews(sensorUnitMeasurementScaleConstant.setDatasheetUnitMeasurementScaleCompletedTopic, onSetDatasheetUnitMeasurementScaleCompleted);
            setUnitMeasurementNumericalScaleTypeCountrySubscription = stompService.subscribeAllViews(sensorUnitMeasurementScaleConstant.setUnitMeasurementNumericalScaleTypeCountryCompletedTopic, onSetUnitMeasurementNumericalScaleTypeCountryCompleted);
            setRangeSubscription = stompService.subscribeAllViews(sensorUnitMeasurementScaleConstant.setRangeCompletedTopic, onSetRangeCompleted);
            setChartLimiterSubscription = stompService.subscribeAllViews(sensorUnitMeasurementScaleConstant.setChartLimiterCompletedTopic, onSetChartLimiterCompleted);
        }

        var setDatasheetUnitMeasurementScale = function (sensorUnitMeasurementScaleId, sensorDatasheetId, sensorTypeId, unitMeasurementId, unitMeasurementTypeId, numericalScalePrefixId, numericalScaleTypeId) {
            var data = {
                sensorUnitMeasurementScaleId: sensorUnitMeasurementScaleId,
                sensorDatasheetId: sensorDatasheetId,
                sensorTypeId: sensorTypeId,
                unitMeasurementId: unitMeasurementId,
                unitMeasurementTypeId: unitMeasurementTypeId,
                numericalScalePrefixId: numericalScalePrefixId,
                numericalScaleTypeId: numericalScaleTypeId,
            }
            return $http.post(serviceBase + sensorUnitMeasurementScaleConstant.setDatasheetUnitMeasurementScaleApiUri, data).then(function (results) {
                return results;
            });
        };    

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

        var setRange = function (sensorUnitMeasurementScaleId, sensorDatasheetId, sensorTypeId, position, value) {
            var data = {
                sensorUnitMeasurementScaleId: sensorUnitMeasurementScaleId,
                sensorDatasheetId: sensorDatasheetId,
                sensorTypeId: sensorTypeId,
                value: value,
                position: position,
            }
            return $http.post(serviceBase + sensorUnitMeasurementScaleConstant.setRangeApiUri, data).then(function (results) {
                return results;
            });
        };        

        var setChartLimiter = function (sensorUnitMeasurementScaleId, sensorDatasheetId, sensorTypeId, position, value) {
            var data = {
                sensorUnitMeasurementScaleId: sensorUnitMeasurementScaleId,
                sensorDatasheetId: sensorDatasheetId,
                sensorTypeId: sensorTypeId,
                value: value,
                position: position,
            }
            return $http.post(serviceBase + sensorUnitMeasurementScaleConstant.setChartLimiterApiUri, data).then(function (results) {
                return results;
            });
        };     
        
        var onSetDatasheetUnitMeasurementScaleCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensorUnitMeasurementScale = sensorUnitMeasurementScaleFinder.getByKey(result.sensorUnitMeasurementScaleId, result.sensorDatasheetId, result.sensorTypeId);

            sensorUnitMeasurementScale.unitMeasurementId = result.unitMeasurementId;
            sensorUnitMeasurementScale.unitMeasurementTypeId = result.unitMeasurementTypeId;
            sensorUnitMeasurementScale.numericalScalePrefixId = result.numericalScalePrefixId;
            sensorUnitMeasurementScale.numericalScaleTypeId = result.numericalScaleTypeId;

            sensorContext.$digest();
            $rootScope.$emit(sensorUnitMeasurementScaleConstant.setDatasheetUnitMeasurementScaleCompletedEventName + result.sensorUnitMeasurementScaleId, result);
        }

        var onSetUnitMeasurementNumericalScaleTypeCountryCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensorUnitMeasurementScale = sensorUnitMeasurementScaleFinder.getByKey(result.sensorUnitMeasurementScaleId, result.sensorDatasheetId, result.sensorTypeId);

            sensorUnitMeasurementScale.unitMeasurementId = result.unitMeasurementId;
            sensorUnitMeasurementScale.unitMeasurementTypeId = result.unitMeasurementTypeId;
            sensorUnitMeasurementScale.numericalScalePrefixId = result.numericalScalePrefixId;
            sensorUnitMeasurementScale.numericalScaleTypeId = result.numericalScaleTypeId;
            sensorUnitMeasurementScale.countryId = result.countryId;

            sensorContext.$digest();
            $rootScope.$emit(sensorUnitMeasurementScaleConstant.setUnitMeasurementNumericalScaleTypeCountryCompletedEventName + result.sensorUnitMeasurementScaleId, result);
        }

        var onSetRangeCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensorUnitMeasurementScale = sensorUnitMeasurementScaleFinder.getByKey(result.sensorUnitMeasurementScaleId, result.sensorDatasheetId, result.sensorTypeId);
            if (result.position === 'Max') {
                sensorUnitMeasurementScale.rangeMax = result.value;
            }
            else if (result.position === 'Min') {
                sensorUnitMeasurementScale.rangeMin = result.value;
            }
            sensorContext.$digest();
            $rootScope.$emit(sensorUnitMeasurementScaleConstant.setRangeCompletedEventName + result.sensorUnitMeasurementScaleId, result);
        }

        var onSetChartLimiterCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensorUnitMeasurementScale = sensorUnitMeasurementScaleFinder.getByKey(result.sensorUnitMeasurementScaleId, result.sensorDatasheetId, result.sensorTypeId);
            if (result.position === 'Max') {
                sensorUnitMeasurementScale.chartLimiterMax = result.value;
            }
            else if (result.position === 'Min') {
                sensorUnitMeasurementScale.chartLimiterMin = result.value;
            }
            sensorContext.$digest();
            $rootScope.$emit(sensorUnitMeasurementScaleConstant.setChartLimiterCompletedEventName + result.sensorUnitMeasurementScaleId, result);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setDatasheetUnitMeasurementScaleSubscription.unsubscribe();
            setUnitMeasurementNumericalScaleTypeCountrySubscription.unsubscribe();
            setRangeSubscription.unsubscribe();
            setChartLimiterSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);       

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.setDatasheetUnitMeasurementScale = setDatasheetUnitMeasurementScale;
        serviceFactory.setUnitMeasurementNumericalScaleTypeCountry = setUnitMeasurementNumericalScaleTypeCountry;
        serviceFactory.setRange = setRange;
        serviceFactory.setChartLimiter = setChartLimiter;

        return serviceFactory;

    }]);