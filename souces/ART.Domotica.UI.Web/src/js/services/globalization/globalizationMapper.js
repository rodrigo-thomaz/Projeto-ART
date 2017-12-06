'use strict';
app.factory('globalizationMapper', ['$rootScope', 'globalizationContext', 'globalizationFinder', 'timeZoneConstant', function ($rootScope, globalizationContext, globalizationFinder, timeZoneConstant) {

    var serviceFactory = {};    

    // *** Navigation Properties Mappers ***

    

    // *** Navigation Properties Mappers ***


    // *** Events Subscriptions
        
    var onTimeZoneGetAllCompleted = function (event, data) {
        timeZoneGetAllCompletedSubscription();
        globalizationContext.timeZoneLoaded = true;
    }

    var timeZoneGetAllCompletedSubscription = $rootScope.$on(timeZoneConstant.getAllCompletedEventName, onTimeZoneGetAllCompleted);

    $rootScope.$on('$destroy', function () {
        timeZoneGetAllCompletedSubscription();
    });

    // *** Events Subscriptions


    return serviceFactory;

}]);