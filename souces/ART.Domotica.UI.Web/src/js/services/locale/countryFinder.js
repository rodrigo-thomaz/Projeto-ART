'use strict';
app.factory('countryFinder', ['$rootScope', 'localeContext', function ($rootScope, localeContext) {

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
            if (context.country[i].continentId === this.continentId) {
                result.push(context.country[i]);
            }
        }
        return result;
    }  

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;
    serviceFactory.getByContinentKey = getByContinentKey;

    return serviceFactory;

}]);