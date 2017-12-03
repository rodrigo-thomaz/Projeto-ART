'use strict';
app.factory('siContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    // *** Public Properties ***

    context.numericalScales = [];
    context.numericalScaleLoaded = false;

    context.numericalScalePrefixes = [];
    context.numericalScalePrefixLoaded = false;

    context.numericalScaleTypes = [];
    context.numericalScaleTypeLoaded = false;

    context.numericalScaleTypeCountries = [];
    context.numericalScaleTypeCountryLoaded = false;

    context.unitMeasurementScales = [];
    context.unitMeasurementScaleLoaded = false;

    context.unitMeasurementTypes = [];
    context.unitMeasurementTypeLoaded = false;

    context.unitMeasurements = [];
    context.unitMeasurementLoaded = false;

    // *** Finders ***

    var getNumericalScaleTypeByKey = function (numericalScaleTypeId) {
        for (var i = 0; i < context.numericalScaleTypes.length; i++) {
            var item = context.numericalScaleTypes[i];
            if (item.numericalScaleTypeId === numericalScaleTypeId) {
                return item;
            }
        }
    }

    var getNumericalScalePrefixByKey = function (numericalScalePrefixId) {
        for (var i = 0; i < context.numericalScalePrefixes.length; i++) {
            var item = context.numericalScalePrefixes[i];
            if (item.numericalScalePrefixId === numericalScalePrefixId) {
                return item;
            }
        }
    }

    var getNumericalScaleByKey = function (numericalScalePrefixId, numericalScaleTypeId) {
        for (var i = 0; i < context.numericalScales.length; i++) {
            var item = context.numericalScales[i];
            if (item.numericalScalePrefixId === numericalScalePrefixId && item.numericalScaleTypeId === numericalScaleTypeId) {
                return item;
            }
        }
    }

    var getUnitMeasurementTypeByKey = function (unitMeasurementTypeId) {
        for (var i = 0; i < context.unitMeasurementTypes.length; i++) {
            var item = context.unitMeasurementTypes[i];
            if (item.unitMeasurementTypeId === unitMeasurementTypeId) {
                return item;
            }
        }
    }

    var getUnitMeasurementByKey = function (unitMeasurementId, unitMeasurementTypeId) {
        for (var i = 0; i < context.unitMeasurements.length; i++) {
            var item = context.unitMeasurements[i];
            if (item.unitMeasurementId === unitMeasurementId && item.unitMeasurementTypeId === unitMeasurementTypeId) {
                return item;
            }
        }
    }

    var getUnitMeasurementScaleByKey = function (unitMeasurementId, unitMeasurementTypeId, numericalScalePrefixId, numericalScaleTypeId) {
        for (var i = 0; i < context.unitMeasurementScales.length; i++) {
            var item = context.unitMeasurementScales[i];
            if (item.unitMeasurementId === unitMeasurementId && item.unitMeasurementTypeId === unitMeasurementTypeId && item.numericalScalePrefixId === numericalScalePrefixId && item.numericalScaleTypeId === numericalScaleTypeId) {
                return item;
            }
        }
    }      

    // *** Public Methods ***

    context.getNumericalScaleTypeByKey = getNumericalScaleTypeByKey;
    context.getNumericalScalePrefixByKey = getNumericalScalePrefixByKey;
    context.getNumericalScaleByKey = getNumericalScaleByKey;
    context.getUnitMeasurementTypeByKey = getUnitMeasurementTypeByKey;
    context.getUnitMeasurementByKey = getUnitMeasurementByKey;
    context.getUnitMeasurementScaleByKey = getUnitMeasurementScaleByKey;

    return context;

}]);