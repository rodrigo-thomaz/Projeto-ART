'use strict';
app.factory('siFinder', ['$rootScope', 'siContext', function ($rootScope, siContext) {

    var context = siContext;

    var serviceFactory = {};    

    var getNumericalScaleTypeByKey = function (numericalScaleTypeId) {
        for (var i = 0; i < context.numericalScaleType.length; i++) {
            var item = context.numericalScaleType[i];
            if (item.numericalScaleTypeId === numericalScaleTypeId) {
                return item;
            }
        }
    }

    var getNumericalScalePrefixByKey = function (numericalScalePrefixId) {
        for (var i = 0; i < context.numericalScalePrefix.length; i++) {
            var item = context.numericalScalePrefix[i];
            if (item.numericalScalePrefixId === numericalScalePrefixId) {
                return item;
            }
        }
    }

    var getNumericalScaleByKey = function (numericalScalePrefixId, numericalScaleTypeId) {
        for (var i = 0; i < context.numericalScale.length; i++) {
            var item = context.numericalScale[i];
            if (item.numericalScalePrefixId === numericalScalePrefixId && item.numericalScaleTypeId === numericalScaleTypeId) {
                return item;
            }
        }
    }

    var getUnitMeasurementTypeByKey = function (unitMeasurementTypeId) {
        for (var i = 0; i < context.unitMeasurementType.length; i++) {
            var item = context.unitMeasurementType[i];
            if (item.unitMeasurementTypeId === unitMeasurementTypeId) {
                return item;
            }
        }
    }

    var getUnitMeasurementByKey = function (unitMeasurementId, unitMeasurementTypeId) {
        for (var i = 0; i < context.unitMeasurement.length; i++) {
            var item = context.unitMeasurement[i];
            if (item.unitMeasurementId === unitMeasurementId && item.unitMeasurementTypeId === unitMeasurementTypeId) {
                return item;
            }
        }
    }

    var getUnitMeasurementScaleByKey = function (unitMeasurementId, unitMeasurementTypeId, numericalScalePrefixId, numericalScaleTypeId) {
        for (var i = 0; i < context.unitMeasurementScale.length; i++) {
            var item = context.unitMeasurementScale[i];
            if (item.unitMeasurementId === unitMeasurementId && item.unitMeasurementTypeId === unitMeasurementTypeId && item.numericalScalePrefixId === numericalScalePrefixId && item.numericalScaleTypeId === numericalScaleTypeId) {
                return item;
            }
        }
    }      

    // *** Public Methods ***

    serviceFactory.getNumericalScaleTypeByKey = getNumericalScaleTypeByKey;
    serviceFactory.getNumericalScalePrefixByKey = getNumericalScalePrefixByKey;
    serviceFactory.getNumericalScaleByKey = getNumericalScaleByKey;
    serviceFactory.getUnitMeasurementTypeByKey = getUnitMeasurementTypeByKey;
    serviceFactory.getUnitMeasurementByKey = getUnitMeasurementByKey;
    serviceFactory.getUnitMeasurementScaleByKey = getUnitMeasurementScaleByKey;

    return serviceFactory;

}]);