'use strict';
app.factory('sensorDatasheetUnitMeasurementScaleFinder', ['$rootScope', 'sensorDatasheetContext', 'numericalScalePrefixFinder', function ($rootScope, sensorDatasheetContext, numericalScalePrefixFinder) {

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

    var getNumericalScalePrefixes = function (sensorDatasheetId, sensorTypeId, numericalScaleTypeId) {
        var result = [];
        for (var i = 0; i < context.sensorDatasheetUnitMeasurementScale.length; i++) {
            var sensorDatasheetUnitMeasurementScale = context.sensorDatasheetUnitMeasurementScale[i];
            if (sensorDatasheetUnitMeasurementScale.sensorDatasheetId === sensorDatasheetId && sensorDatasheetUnitMeasurementScale.sensorTypeId === sensorTypeId && sensorDatasheetUnitMeasurementScale.numericalScaleTypeId === numericalScaleTypeId) {
                var numericalScalePrefix = numericalScalePrefixFinder.getByKey(sensorDatasheetUnitMeasurementScale.numericalScalePrefixId);
                result.push(numericalScalePrefix);
            }
        }
        return result;
    }

    // *** Public Methods ***
        
    serviceFactory.getByKey = getByKey;
    serviceFactory.getBySensorDatasheetKey = getBySensorDatasheetKey;
    serviceFactory.getByUnitMeasurementScaleKey = getByUnitMeasurementScaleKey;
    serviceFactory.getNumericalScalePrefixes = getNumericalScalePrefixes;

    return serviceFactory;

}]);