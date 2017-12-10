'use strict';
app.factory('sensorTypeFinder', ['$rootScope', 'sensorDatasheetContext', function ($rootScope, sensorDatasheetContext) {

    var context = sensorDatasheetContext;

    var serviceFactory = {};    

    var getByKey = function (sensorTypeId) {
        for (var i = 0; i < context.sensorType.length; i++) {
            var item = context.sensorType[i];
            if (item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    }   

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);