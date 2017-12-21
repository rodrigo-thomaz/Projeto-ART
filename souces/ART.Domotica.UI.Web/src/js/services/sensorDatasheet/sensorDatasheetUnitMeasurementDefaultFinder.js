'use strict';
app.factory('sensorDatasheetUnitMeasurementDefaultFinder', ['$rootScope', 'sensorDatasheetContext', function ($rootScope, sensorDatasheetContext) {

    var context = sensorDatasheetContext;

    var serviceFactory = {};        

    var getByKey = function (sensorDatasheetUnitMeasurementDefaultId, sensorTypeId) {
        for (var i = 0; i < context.sensorDatasheetUnitMeasurementDefault.length; i++) {
            var item = context.sensorDatasheetUnitMeasurementDefault[i];
            if (item.sensorDatasheetUnitMeasurementDefaultId === sensorDatasheetUnitMeasurementDefaultId && item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    };   

    var getByUnitMeasurementScaleKey = function (unitMeasurementId, unitMeasurementTypeId, numericalScalePrefixId, numericalScaleTypeId) {
        var result = [];
        for (var i = 0; i < context.sensorDatasheetUnitMeasurementDefault.length; i++) {
            var sensorDatasheetUnitMeasurementDefault = context.sensorDatasheetUnitMeasurementDefault[i];
            if (sensorDatasheetUnitMeasurementDefault.unitMeasurementId === unitMeasurementId && sensorDatasheetUnitMeasurementDefault.unitMeasurementTypeId === unitMeasurementTypeId && sensorDatasheetUnitMeasurementDefault.numericalScalePrefixId === numericalScalePrefixId && sensorDatasheetUnitMeasurementDefault.numericalScaleTypeId === numericalScaleTypeId) {                
                result.push(sensorDatasheetUnitMeasurementDefault);
            }
        }
        return result;
    }; 

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;
    serviceFactory.getByUnitMeasurementScaleKey = getByUnitMeasurementScaleKey;

    return serviceFactory;

}]);