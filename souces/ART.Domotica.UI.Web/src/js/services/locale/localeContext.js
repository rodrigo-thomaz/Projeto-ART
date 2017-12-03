'use strict';
app.factory('localeContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    // *** Public Properties ***

    context.continents = [];    
    context.continentLoaded = false;    

    context.countries = [];    
    context.countryLoaded = false;        

    // *** Finders ***

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

    context.getContinentByKey = getContinentByKey;
    context.getCountryByKey = getCountryByKey;

    return context;

}]);