'use strict';
app.factory('sensorTriggerFinder', ['$rootScope', 'sensorContext', function ($rootScope, sensorContext) {

    var context = sensorContext;

    var serviceFactory = {};

    var getByKey = function (sensorTriggerId) {
        for (var i = 0; i < context.sensorTrigger.length; i++) {
            var item = context.sensorTrigger[i];
            if (item.sensorTriggerId === sensorTriggerId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);