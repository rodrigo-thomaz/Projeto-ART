'use strict';
app.factory('sensorDatasheetFinder', ['$rootScope', 'sensorDatasheetContext', function ($rootScope, sensorDatasheetContext) {

    var context = sensorDatasheetContext;

    var serviceFactory = {};       

    var getByKey = function (sensorDatasheetId, sensorTypeId) {
        for (var i = 0; i < context.sensorDatasheet.length; i++) {
            var item = context.sensorDatasheet[i];
            if (item.sensorDatasheetId === sensorDatasheetId && item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);