﻿'use strict';
app.factory('siContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    context.numericalScale = [];
    context.numericalScaleLoaded = false;

    context.numericalScalePrefix = [];
    context.numericalScalePrefixLoaded = false;

    context.numericalScaleType = [];
    context.numericalScaleTypeLoaded = false;

    context.numericalScaleTypeCountry = [];
    context.numericalScaleTypeCountryLoaded = false;

    context.unitMeasurementScale = [];
    context.unitMeasurementScaleLoaded = false;

    context.unitMeasurementType = [];
    context.unitMeasurementTypeLoaded = false;

    context.unitMeasurement = [];
    context.unitMeasurementLoaded = false;
        
    return context;

}]);
﻿