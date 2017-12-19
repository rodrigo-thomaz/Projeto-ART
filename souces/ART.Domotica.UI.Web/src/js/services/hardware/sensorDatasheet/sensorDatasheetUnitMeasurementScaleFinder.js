'use strict';
app.factory('sensorDatasheetUnitMeasurementScaleFinder', ['$rootScope', 'orderByFilter', 'sensorDatasheetContext', 'unitMeasurementFinder', 'numericalScaleTypeFinder', 'numericalScaleTypeCountryFinder',
    function ($rootScope, orderBy, sensorDatasheetContext, unitMeasurementFinder, numericalScaleTypeFinder, numericalScaleTypeCountryFinder) {

        var context = sensorDatasheetContext;

        var serviceFactory = {};

        var getByKey = function (sensorDatasheetId, sensorTypeId, unitMeasurementId, unitMeasurementTypeId, numericalScalePrefixId, numericalScaleTypeId) {
            for (var i = 0; i < context.sensorDatasheetUnitMeasurementScale.length; i++) {
                var item = context.sensorDatasheetUnitMeasurementScale[i];
                if (item.sensorDatasheetId === sensorDatasheetId && item.sensorTypeId === sensorTypeId && item.unitMeasurementId === unitMeasurementId && item.unitMeasurementTypeId === unitMeasurementTypeId && item.numericalScalePrefixId === numericalScalePrefixId && item.numericalScaleTypeId === numericalScaleTypeId) {
                    return item;
                }
            }
        };

        var getBySensorDatasheetKey = function (sensorDatasheetId, sensorTypeId) {
            var result = [];
            for (var i = 0; i < context.sensorDatasheetUnitMeasurementScale.length; i++) {
                var sensorDatasheetUnitMeasurementScale = context.sensorDatasheetUnitMeasurementScale[i];
                if (sensorDatasheetUnitMeasurementScale.sensorDatasheetId === sensorDatasheetId && sensorDatasheetUnitMeasurementScale.sensorTypeId === sensorTypeId) {
                    result.push(sensorDatasheetUnitMeasurementScale);
                }
            }
            return result;
        };

        var getByUnitMeasurementScaleKey = function (unitMeasurementId, unitMeasurementTypeId, numericalScalePrefixId, numericalScaleTypeId) {
            var result = [];
            for (var i = 0; i < context.sensorDatasheetUnitMeasurementScale.length; i++) {
                var sensorDatasheetUnitMeasurementScale = context.sensorDatasheetUnitMeasurementScale[i];
                if (sensorDatasheetUnitMeasurementScale.unitMeasurementId === unitMeasurementId && sensorDatasheetUnitMeasurementScale.unitMeasurementTypeId === unitMeasurementTypeId && sensorDatasheetUnitMeasurementScale.numericalScalePrefixId === numericalScalePrefixId && sensorDatasheetUnitMeasurementScale.numericalScaleTypeId === numericalScaleTypeId) {
                    result.push(sensorDatasheetUnitMeasurementScale);
                }
            }
            return result;
        };

        var containInNumericalScaleTypeCountries = function (numericalScaleTypeCountries, numericalScaleTypeId) {
            for (var i = 0; i < numericalScaleTypeCountries.length; i++) {
                if (numericalScaleTypeCountries[i].numericalScaleTypeId === numericalScaleTypeId) {
                    return true;
                }
            }
            return false;
        };

        var getNumericalScaleTypesBySensorDatasheetCountryKey = function (sensorDatasheetId, sensorTypeId, countryId) {
            var result = [];
            var numericalScaleTypeCountries = numericalScaleTypeCountryFinder.getByCountryKey(countryId);
            for (var i = 0; i < context.sensorDatasheetUnitMeasurementScale.length; i++) {
                var sensorDatasheetUnitMeasurementScale = context.sensorDatasheetUnitMeasurementScale[i];
                var containInNumericalScaleTypeCountries = serviceFactory.containInNumericalScaleTypeCountries(numericalScaleTypeCountries, sensorDatasheetUnitMeasurementScale.numericalScaleTypeId);
                if (containInNumericalScaleTypeCountries && sensorDatasheetUnitMeasurementScale.sensorDatasheetId === sensorDatasheetId && sensorDatasheetUnitMeasurementScale.sensorTypeId === sensorTypeId) {
                    var numericalScaleType = numericalScaleTypeFinder.getByKey(sensorDatasheetUnitMeasurementScale.numericalScaleTypeId);
                    var contain = false;
                    for (var j = 0; j < result.length; j++) {
                        if (result[j] === numericalScaleType) {
                            contain = true;
                            break;
                        }
                    }
                    if (!contain) result.push(numericalScaleType);
                }
            }
            return orderBy(result, 'name', false);
        }

        var getUnitMeasurementsBySensorDatasheetKey = function (sensorDatasheetId, sensorTypeId) {
            var result = [];
            for (var i = 0; i < context.sensorDatasheetUnitMeasurementScale.length; i++) {
                var sensorDatasheetUnitMeasurementScale = context.sensorDatasheetUnitMeasurementScale[i];
                if (sensorDatasheetUnitMeasurementScale.sensorDatasheetId === sensorDatasheetId && sensorDatasheetUnitMeasurementScale.sensorTypeId === sensorTypeId) {
                    var unitMeasurement = unitMeasurementFinder.getByKey(sensorDatasheetUnitMeasurementScale.unitMeasurementId, sensorDatasheetUnitMeasurementScale.unitMeasurementTypeId);
                    var contain = false;
                    for (var j = 0; j < result.length; j++) {
                        if (result[j] === unitMeasurement) {
                            contain = true;
                            break;
                        }
                    }
                    if (!contain) result.push(unitMeasurement);
                }
            }
            return orderBy(result, 'name', false);
        }

        // *** Public Methods ***

        serviceFactory.getByKey = getByKey;
        serviceFactory.getBySensorDatasheetKey = getBySensorDatasheetKey;
        serviceFactory.getByUnitMeasurementScaleKey = getByUnitMeasurementScaleKey;
        serviceFactory.getNumericalScaleTypesBySensorDatasheetCountryKey = getNumericalScaleTypesBySensorDatasheetCountryKey;
        serviceFactory.getUnitMeasurementsBySensorDatasheetKey = getUnitMeasurementsBySensorDatasheetKey;
        serviceFactory.containInNumericalScaleTypeCountries = containInNumericalScaleTypeCountries;

        return serviceFactory;

    }]);