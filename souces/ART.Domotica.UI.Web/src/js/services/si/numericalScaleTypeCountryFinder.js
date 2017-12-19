'use strict';
app.factory('numericalScaleTypeCountryFinder', ['$rootScope', 'siContext', 'orderByFilter',
    function ($rootScope, siContext, orderBy) {

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

        var getCountries = function () {
            var result = [];
            for (var i = 0; i < context.numericalScaleTypeCountry.length; i++) {
                var country = context.numericalScaleTypeCountry[i].country();
                var contains = false;
                for (var j = 0; j < result.length; j++) {
                    if (result[j] === country) {
                        contains = true;
                        break;
                    }   
                }
                if (!contains) result.push(country);
            }
            return orderBy(result, 'name', false);
        }

        // *** Public Methods ***

        serviceFactory.getByKey = getByKey;
        serviceFactory.getByNumericalScaleTypeKey = getByNumericalScaleTypeKey;
        serviceFactory.getByCountryKey = getByCountryKey;
        serviceFactory.getCountries = getCountries;

        return serviceFactory;

    }]);