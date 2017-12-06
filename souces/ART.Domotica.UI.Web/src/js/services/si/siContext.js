﻿'use strict';
app.factory('siContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

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
        
    return context;

}]);
﻿