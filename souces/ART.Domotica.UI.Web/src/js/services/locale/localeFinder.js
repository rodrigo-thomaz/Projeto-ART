'use strict';
app.factory('localeFinder', ['$rootScope', 'localeContext', function ($rootScope, localeContext) {

    var context = localeContext;

    var serviceFactory = {};    

    var getContinentByKey = function (continentId) {
        for (var i = 0; i < context.continents.length; i++) {
            var item = context.continents[i];
            if (item.continentId === continentId) {
                return item;
            }
        }
    }

    var getCountryByKey = function (countryId) {
        for (var i = 0; i < context.countries.length; i++) {
            var item = context.countries[i];
            if (item.countryId === countryId) {
                return item;
            }
        }
    }       

    // *** Public Methods ***

    serviceFactory.getContinentByKey = getContinentByKey;
    serviceFactory.getCountryByKey = getCountryByKey;

    return serviceFactory;

}]);