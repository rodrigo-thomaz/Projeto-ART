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

    var getBySensorKey = function (sensorId) {
        var result = [];
        for (var i = 0; i < context.sensorTrigger.length; i++) {
            var sensorTrigger = context.sensorTrigger[i];
            if (sensorTrigger.sensorId === sensorId) {
                result.push(sensorTrigger);
            }
        }
        return result;
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;
    serviceFactory.getBySensorKey = getBySensorKey;

    return serviceFactory;

}]);