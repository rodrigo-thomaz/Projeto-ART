'use strict';
app.factory('localeFinder', ['$rootScope', 'localeContext', function ($rootScope, localeContext) {

    var context = localeContext;

    var serviceFactory = {};    

    var getContinentByKey = function (continentId) {
        for (var i = 0; i < context.continent.length; i++) {
            var item = context.continent[i];
            if (item.continentId === continentId) {
                return item;
            }
        }
    }

    var getCountryByKey = function (countryId) {
        for (var i = 0; i < context.country.length; i++) {
            var item = context.country[i];
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