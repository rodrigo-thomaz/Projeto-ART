'use strict';
app.factory('continentFinder', ['$rootScope', 'localeContext', function ($rootScope, localeContext) {

    var context = localeContext;

    var serviceFactory = {};    

    var getByKey = function (continentId) {
        for (var i = 0; i < context.continent.length; i++) {
            var item = context.continent[i];
            if (item.continentId === continentId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);