'use strict';
app.factory('localeMapper', ['$rootScope', 'localeContext', 'continentFinder', 'countryFinder', 'numericalScaleTypeCountryFinder', 'continentConstant', 'countryConstant',
    function ($rootScope, localeContext, continentFinder, countryFinder, numericalScaleTypeCountryFinder, continentConstant, countryConstant) {

        var serviceFactory = {};

        localeContext.$watchCollection('continent', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var continent = newValues[i];
                continent.countries = function () { return countryFinder.getByContinentKey(this.continentId); }
            }
        });

        localeContext.$watchCollection('country', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var country = newValues[i];
                country.continent = function () { return continentFinder.getByKey(this.continentId); }
                country.numericalScaleTypeCountries = function () { return numericalScaleTypeCountryFinder.getByCountryKey(this.countryId); }
            }
        });

        // *** Events Subscriptions

        var onContinentGetAllCompleted = function (event, data) {
            continentGetAllCompletedSubscription();
            localeContext.continentLoaded = true;
        }

        var onCountryGetAllCompleted = function (event, data) {
            countryGetAllCompletedSubscription();
            localeContext.countryLoaded = true;
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