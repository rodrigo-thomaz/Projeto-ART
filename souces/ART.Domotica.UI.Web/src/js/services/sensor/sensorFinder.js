'use strict';
app.factory('sensorFinder', ['$rootScope', 'sensorContext', function ($rootScope, sensorContext) {

    var context = sensorContext;

    var serviceFactory = {};    

    var getByKey = function (sensorId, sensorDatasheetId, sensorTypeId) {
        for (var i = 0; i < context.sensor.length; i++) {
            var item = context.sensor[i];
            if (item.sensorId === sensorId && item.sensorDatasheetId === sensorDatasheetId && item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    }

    var getBySensorDatasheetKey = function (sensorDatasheetId, sensorTypeId) {
        var result = [];
        for (var i = 0; i < context.sensor.length; i++) {
            var sensor = context.sensor[i];
            if (sensor.sensorDatasheetId === sensorDatasheetId && sensor.sensorTypeId === sensorTypeId) {
                result.push(sensor);
            }
        }
        return result;
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;
    serviceFactory.getBySensorDatasheetKey = getBySensorDatasheetKey;

    return serviceFactory;

}]);