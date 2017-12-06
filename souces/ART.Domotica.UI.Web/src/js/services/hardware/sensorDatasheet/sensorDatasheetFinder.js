'use strict';
app.factory('sensorDatasheetFinder', ['$rootScope', 'sensorDatasheetContext', function ($rootScope, sensorDatasheetContext) {

    var context = sensorDatasheetContext;

    var serviceFactory = {};    

    var getSensorTypeByKey = function (sensorTypeId) {
        for (var i = 0; i < context.sensorType.length; i++) {
            var item = context.sensorType[i];
            if (item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    }

    var getSensorDatasheetByKey = function (sensorDatasheetId, sensorTypeId) {
        for (var i = 0; i < context.sensorDatasheet.length; i++) {
            var item = context.sensorDatasheet[i];
            if (item.sensorDatasheetId === sensorDatasheetId && item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    }

    var getSensorUnitMeasurementDefaultByKey = function (sensorUnitMeasurementDefaultId, sensorTypeId) {
        for (var i = 0; i < context.sensorUnitMeasurementDefault.length; i++) {
            var item = context.sensorUnitMeasurementDefault[i];
            if (item.sensorUnitMeasurementDefaultId === sensorUnitMeasurementDefaultId && item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    };    

    var getSensorDatasheetUnitMeasurementScaleByKey = function (sensorDatasheetId, sensorTypeId, unitMeasurementId, unitMeasurementTypeId, numericalScalePrefixId, numericalScaleTypeId) {
        for (var i = 0; i < context.sensorDatasheetUnitMeasurementScale.length; i++) {
            var item = context.sensorDatasheetUnitMeasurementScale[i];
            if (item.sensorDatasheetId === sensorDatasheetId && item.sensorTypeId === sensorTypeId && item.unitMeasurementId === unitMeasurementId && item.unitMeasurementTypeId === unitMeasurementTypeId && item.numericalScalePrefixId === numericalScalePrefixId && item.numericalScaleTypeId === numericalScaleTypeId) {
                return item;
            }
        }
    };  

    // *** Public Methods ***

    serviceFactory.getSensorTypeByKey = getSensorTypeByKey;
    serviceFactory.getSensorDatasheetByKey = getSensorDatasheetByKey;
    serviceFactory.getSensorUnitMeasurementDefaultByKey = getSensorUnitMeasurementDefaultByKey;
    serviceFactory.getSensorDatasheetUnitMeasurementScaleByKey = getSensorDatasheetUnitMeasurementScaleByKey;

    return serviceFactory;

}]);