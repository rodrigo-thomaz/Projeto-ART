'use strict';
app.factory('localeMapper', ['$rootScope', 'localeContext', 'localeFinder', 'continentConstant', 'countryConstant',
    function ($rootScope, localeContext, localeFinder, continentConstant, countryConstant) {

        var serviceFactory = {};

        localeContext.$watchCollection('continent', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var continent = newValues[i];
                //countries
                continent.countries = function () {
                    var result = [];
                    for (var i = 0; i < localeContext.country.length; i++) {
                        if (localeContext.country[i].continentId === this.continentId) {
                            result.push(localeContext.country[i]);
                        }
                    }
                    return result;
                }
            }
        });

        localeContext.$watchCollection('country', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var country = newValues[i];
                //continent
                country.continent = function () {
                    return localeFinder.getContinentByKey(this.continentId);
                }
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