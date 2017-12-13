'use strict';
app.factory('numericalScaleTypeCountryFinder', ['$rootScope', 'siContext', function ($rootScope, siContext) {

    var context = siContext;

    var serviceFactory = {};    

    var getByKey = function (numericalScaleTypeId, countryId) {
        for (var i = 0; i < context.numericalScaleTypeCountry.length; i++) {
            var item = context.numericalScaleTypeCountry[i];
            if (item.numericalScaleTypeId === numericalScaleTypeId && item.countryId === countryId) {
                return item;
            }
        }
    }

    var getByNumericalScaleTypeKey = function (numericalScaleTypeId) {
        var result = [];
        for (var i = 0; i < context.numericalScaleTypeCountry.length; i++) {
            if (context.numericalScaleTypeCountry[i].numericalScaleTypeId === numericalScaleTypeId) {
                result.push(context.numericalScaleTypeCountry[i]);
            }
        }
        return result;
    } 

    var getByCountryKey = function (countryId) {
        var result = [];
        for (var i = 0; i < context.numericalScaleTypeCountry.length; i++) {
            if (context.numericalScaleTypeCountry[i].countryId === countryId) {
                result.push(context.numericalScaleTypeCountry[i]);
            }
        }
        return result;
    }    

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;
    serviceFactory.getByNumericalScaleTypeKey = getByNumericalScaleTypeKey;
    serviceFactory.getByCountryKey = getByCountryKey;

    return serviceFactory;

}]);