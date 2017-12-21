'use strict';
app.factory('sensorUnitMeasurementScaleFinder', ['$rootScope', 'sensorContext', function ($rootScope, sensorContext) {

    var context = sensorContext;

    var serviceFactory = {};

    var getByKey = function (sensorUnitMeasurementScaleId, sensorDatasheetId, sensorTypeId) {
        for (var i = 0; i < context.sensorUnitMeasurementScale.length; i++) {
            var item = context.sensorUnitMeasurementScale[i];
            if (item.sensorUnitMeasurementScaleId === sensorUnitMeasurementScaleId && item.sensorDatasheetId === sensorDatasheetId && item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    }

    var getBySensorUnitMeasurementScaleKey = function (sensorDatasheetId, sensorTypeId, unitMeasurementId, unitMeasurementTypeId, numericalScalePrefixId, numericalScaleTypeId) {
        var result = [];
        for (var i = 0; i < context.sensorUnitMeasurementScale.length; i++) {
            var sensorUnitMeasurementScale = context.sensorUnitMeasurementScale[i];
            if (sensorUnitMeasurementScale.sensorDatasheetId === sensorDatasheetId && sensorUnitMeasurementScale.sensorTypeId === sensorTypeId && sensorUnitMeasurementScale.unitMeasurementId === unitMeasurementId && sensorUnitMeasurementScale.unitMeasurementTypeId === unitMeasurementTypeId && sensorUnitMeasurementScale.numericalScalePrefixId === numericalScalePrefixId && sensorUnitMeasurementScale.numericalScaleTypeId === numericalScaleTypeId) {
                result.push(sensorUnitMeasurementScale);
            }
        }
        return result;
    }; 

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;
    serviceFactory.getBySensorUnitMeasurementScaleKey = getBySensorUnitMeasurementScaleKey;

    return serviceFactory;

}]);