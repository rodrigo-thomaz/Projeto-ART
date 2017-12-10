'use strict';
app.factory('sensorUnitMeasurementScaleFinder', ['$rootScope', 'sensorContext', function ($rootScope, sensorContext) {

    var context = sensorContext;

    var serviceFactory = {};

    var getByKey = function (sensorUnitMeasurementScaleId) {
        for (var i = 0; i < context.sensorUnitMeasurementScale.length; i++) {
            var item = context.sensorUnitMeasurementScale[i];
            if (item.sensorUnitMeasurementScaleId === sensorUnitMeasurementScaleId) {
                return item;
            }
        }
    }

    var getByUnitMeasurementScaleKey = function (unitMeasurementId, unitMeasurementTypeId, numericalScalePrefixId, numericalScaleTypeId) {
        var result = [];
        for (var i = 0; i < context.sensorUnitMeasurementScale.length; i++) {
            var sensorUnitMeasurementScale = context.sensorUnitMeasurementScale[i];
            if (sensorUnitMeasurementScale.unitMeasurementId === unitMeasurementId && sensorUnitMeasurementScale.unitMeasurementTypeId === unitMeasurementTypeId && sensorUnitMeasurementScale.numericalScalePrefixId === numericalScalePrefixId && sensorUnitMeasurementScale.numericalScaleTypeId === numericalScaleTypeId) {
                result.push(sensorUnitMeasurementScale);
            }
        }
        return result;
    }; 

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;
    serviceFactory.getByUnitMeasurementScaleKey = getByUnitMeasurementScaleKey;

    return serviceFactory;

}]);