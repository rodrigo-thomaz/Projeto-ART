'use strict';
app.factory('countryFinder', ['$rootScope', 'localeContext', 'siContext', 'numericalScaleTypeFinder',
    function ($rootScope, localeContext, siContext, numericalScaleTypeFinder) {

        var context = localeContext;

        var serviceFactory = {};

        var getByKey = function (countryId) {
            for (var i = 0; i < context.country.length; i++) {
                var item = context.country[i];
                if (item.countryId === countryId) {
                    return item;
                }
            }
        }

        var getByContinentKey = function (continentId) {
            var result = [];
            for (var i = 0; i < context.country.length; i++) {
                if (context.country[i].continentId === continentId) {
                    result.push(context.country[i]);
                }
            }
            return result;
        }

        var getByNumericalScaleTypeKey = function (numericalScaleTypeId) {
            var result = [];
            for (var i = 0; i < siContext.numericalScaleTypeCountry.length; i++) {
                if (siContext.numericalScaleTypeCountry[i].numericalScaleTypeId === numericalScaleTypeId) {
                    var country = getByKey(siContext.numericalScaleTypeCountry[i].countryId);
                    result.push(country);
                }
            }
            return result;
        }

        // *** Public Methods ***

        serviceFactory.getByKey = getByKey;
        serviceFactory.getByContinentKey = getByContinentKey;
        serviceFactory.getByNumericalScaleTypeKey = getByNumericalScaleTypeKey;

        return serviceFactory;

    }]);