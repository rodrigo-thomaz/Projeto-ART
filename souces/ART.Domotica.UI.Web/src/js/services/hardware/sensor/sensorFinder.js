'use strict';
app.factory('sensorFinder', ['$rootScope', 'sensorContext', function ($rootScope, sensorContext) {

    var context = sensorContext;

    var serviceFactory = {};    

    var getSensorByKey = function (sensorId) {
        for (var i = 0; i < context.sensor.length; i++) {
            var item = context.sensor[i];
            if (item.sensorId === sensorId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getSensorByKey = getSensorByKey;

    return serviceFactory;

}]);