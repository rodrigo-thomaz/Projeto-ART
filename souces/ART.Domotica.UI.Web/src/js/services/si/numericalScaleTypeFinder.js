'use strict';
app.factory('numericalScaleTypeFinder', ['$rootScope', 'siContext', function ($rootScope, siContext) {

    var context = siContext;

    var serviceFactory = {};    

    var getByKey = function (numericalScaleTypeId) {
        for (var i = 0; i < context.numericalScaleType.length; i++) {
            var item = context.numericalScaleType[i];
            if (item.numericalScaleTypeId === numericalScaleTypeId) {
                return item;
            }
        }
    }

    var getByCountryKey = function (countryId) {
        var result = [];
        for (var i = 0; i < context.numericalScaleTypeCountry.length; i++) {
            if (context.numericalScaleTypeCountry[i].countryId === countryId) {
                var numericalScaleType = getByKey(context.numericalScaleTypeCountry[i].numericalScaleTypeId);
                result.push(numericalScaleType);
            }
        }
        return result;
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;
    serviceFactory.getByCountryKey = getByCountryKey;

    return serviceFactory;

}]);