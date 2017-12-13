'use strict';
app.factory('unitMeasurementScaleFinder', ['$rootScope', 'siContext', function ($rootScope, siContext) {

    var context = siContext;

    var serviceFactory = {};    
    
    var getByKey = function (unitMeasurementId, unitMeasurementTypeId, numericalScalePrefixId, numericalScaleTypeId) {
        for (var i = 0; i < context.unitMeasurementScale.length; i++) {
            var item = context.unitMeasurementScale[i];
            if (item.unitMeasurementId === unitMeasurementId && item.unitMeasurementTypeId === unitMeasurementTypeId && item.numericalScalePrefixId === numericalScalePrefixId && item.numericalScaleTypeId === numericalScaleTypeId) {
                return item;
            }
        }
    }      

    var getByNumericalScaleKey = function (numericalScalePrefixId, numericalScaleTypeId) {
        var result = [];
        for (var i = 0; i < context.unitMeasurementScale.length; i++) {
            var unitMeasurementScale = context.unitMeasurementScale[i];
            if (unitMeasurementScale.numericalScalePrefixId === numericalScalePrefixId && unitMeasurementScale.numericalScaleTypeId === numericalScaleTypeId) {
                result.push(context.unitMeasurementScale[i]);
            }
        }
        return result;
    }  

    var getByUnitMeasurementKey = function (unitMeasurementId, unitMeasurementTypeId) {
        var result = [];
        for (var i = 0; i < context.unitMeasurementScale.length; i++) {
            var unitMeasurementScale = context.unitMeasurementScale[i];
            if (unitMeasurementScale.unitMeasurementId === unitMeasurementId && unitMeasurementScale.unitMeasurementTypeId === unitMeasurementTypeId) {
                result.push(context.unitMeasurementScale[i]);
            }
        }
        return result;
    }  

    var getUnitMeasurementScalePrefixes = function (unitMeasurementId, unitMeasurementTypeId, numericalScaleTypeId) {
        var result = [];
        for (var i = 0; i < context.unitMeasurementScale.length; i++) {
            var unitMeasurementScale = context.unitMeasurementScale[i];
            if (unitMeasurementScale.unitMeasurementId === unitMeasurementId && unitMeasurementScale.unitMeasurementTypeId === unitMeasurementTypeId && unitMeasurementScale.numericalScaleTypeId === numericalScaleTypeId) {
                result.push(context.unitMeasurementScale[i]);
            }
        }
        return result;
    } 


    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;
    serviceFactory.getByNumericalScaleKey = getByNumericalScaleKey;
    serviceFactory.getByUnitMeasurementKey = getByUnitMeasurementKey;
    serviceFactory.getUnitMeasurementScalePrefixes = getUnitMeasurementScalePrefixes;

    return serviceFactory;

}]);