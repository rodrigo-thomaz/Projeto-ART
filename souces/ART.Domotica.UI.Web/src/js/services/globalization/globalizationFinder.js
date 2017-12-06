'use strict';
app.factory('globalizationFinder', ['$rootScope', 'globalizationContext', function ($rootScope, globalizationContext) {

    var context = globalizationContext;

    var serviceFactory = {};    

    var getTimeZoneByKey = function (timeZoneId) {
        for (var i = 0; i < context.timeZone.length; i++) {
            var item = context.timeZone[i];
            if (item.timeZoneId === timeZoneId) {
                return item;
            }
        }
    } 

    // *** Public Methods ***

    serviceFactory.getTimeZoneByKey = getTimeZoneByKey;

    return serviceFactory;

}]);