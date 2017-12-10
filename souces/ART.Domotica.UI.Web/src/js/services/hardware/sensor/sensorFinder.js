'use strict';
app.factory('sensorFinder', ['$rootScope', 'sensorContext', function ($rootScope, sensorContext) {

    var context = sensorContext;

    var serviceFactory = {};    

    var getByKey = function (sensorId) {
        for (var i = 0; i < context.sensor.length; i++) {
            var item = context.sensor[i];
            if (item.sensorId === sensorId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);