'use strict';
app.factory('globalizationMapper', ['$rootScope', 'globalizationContext', 'timeZoneFinder', 'timeZoneConstant', 'deviceNTPFinder',
    function ($rootScope, globalizationContext, timeZoneFinder, timeZoneConstant, deviceNTPFinder) {

    var serviceFactory = {};    

    globalizationContext.$watchCollection('timeZone', function (newValues, oldValues) {
        for (var i = 0; i < newValues.length; i++) {
            var timeZone = newValues[i];
            timeZone.devicesNTP = function () { return deviceNTPFinder.getByTimeZoneKey(this.timeZoneId); }
        }
    });

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