'use strict';
app.factory('localeMapper', ['$rootScope', 'localeContext', 'localeFinder', function ($rootScope, localeContext, localeFinder) {

    var serviceFactory = {};    

    // *** Navigation Properties Mappers ***

    var mapper_Country_Continent_Init = false;
    var mapper_Country_Continent = function () {
        if (!mapper_Country_Continent_Init && localeContext.countryLoaded && localeContext.continentLoaded) {
            mapper_Country_Continent_Init = true;
            for (var i = 0; i < localeContext.countries.length; i++) {
                var country = localeContext.countries[i];
                var continent = localeFinder.getContinentByKey(country.continentId);
                country.continent = continent;
                delete country.continentId; // removendo a foreing key
                if (continent.countries === undefined) {
                    continent.countries = [];
                }
                continent.countries.push(country);
            }
        }
    };   

    // *** Watches ***

    localeContext.$watch('continentLoaded', function (newValue, oldValue) {
        mapper_Country_Continent();
    });

    localeContext.$watch('countryLoaded', function (newValue, oldValue) {
        mapper_Country_Continent();
    });        
    
    return serviceFactory;

}]);