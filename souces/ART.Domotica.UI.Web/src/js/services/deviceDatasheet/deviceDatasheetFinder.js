'use strict';
app.factory('deviceDatasheetFinder', ['$rootScope', 'deviceDatasheetContext', function ($rootScope, deviceDatasheetContext) {

    var context = deviceDatasheetContext;

    var serviceFactory = {};       

    var getByKey = function (deviceDatasheetId, deviceTypeId) {
        for (var i = 0; i < context.deviceDatasheet.length; i++) {
            var item = context.deviceDatasheet[i];
            if (item.deviceDatasheetId === deviceDatasheetId && item.deviceTypeId === deviceTypeId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);