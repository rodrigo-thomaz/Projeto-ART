'use strict';
app.factory('timeZoneFinder', ['$rootScope', 'globalizationContext', function ($rootScope, globalizationContext) {

    var context = globalizationContext;

    var serviceFactory = {};    

    var getByKey = function (timeZoneId) {
        for (var i = 0; i < context.timeZone.length; i++) {
            var item = context.timeZone[i];
            if (item.timeZoneId === timeZoneId) {
                return item;
            }
        }
    } 

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);