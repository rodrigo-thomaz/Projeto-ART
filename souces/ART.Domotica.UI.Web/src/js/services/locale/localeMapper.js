'use strict';
app.factory('localeMapper', ['$rootScope', 'localeContext', 'localeFinder', 'continentConstant', 'countryConstant', function ($rootScope, localeContext, localeFinder, continentConstant, countryConstant) {

    var serviceFactory = {};    

    // *** Navigation Properties Mappers ***

    var mapper_Country_Continent_Init = false;
    var mapper_Country_Continent = function () {
        if (!mapper_Country_Continent_Init && localeContext.countryLoaded && localeContext.continentLoaded) {
            mapper_Country_Continent_Init = true;
            for (var i = 0; i < localeContext.country.length; i++) {
                var country = localeContext.country[i];
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

    // *** Navigation Properties Mappers ***


    // *** Events Subscriptions

    var onContinentGetAllCompleted = function (event, data) {
        continentGetAllCompletedSubscription();
        localeContext.continentLoaded = true;
        mapper_Country_Continent();
    }

    var onCountryGetAllCompleted = function (event, data) {
        countryGetAllCompletedSubscription();
        localeContext.countryLoaded = true;
        mapper_Country_Continent();
    }

    var continentGetAllCompletedSubscription = $rootScope.$on(continentConstant.getAllCompletedEventName, onContinentGetAllCompleted);
    var countryGetAllCompletedSubscription = $rootScope.$on(countryConstant.getAllCompletedEventName, onCountryGetAllCompleted);

    $rootScope.$on('$destroy', function () {
        continentGetAllCompletedSubscription();
        countryGetAllCompletedSubscription();
    });

    // *** Events Subscriptions

    return serviceFactory;

}]);