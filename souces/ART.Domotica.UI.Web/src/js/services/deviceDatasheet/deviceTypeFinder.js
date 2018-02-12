'use strict';
app.factory('deviceTypeFinder', ['$rootScope', 'deviceDatasheetContext', function ($rootScope, deviceDatasheetContext) {

    var context = deviceDatasheetContext;

    var serviceFactory = {};    

    var getByKey = function (deviceTypeId) {
        for (var i = 0; i < context.deviceType.length; i++) {
            var item = context.deviceType[i];
            if (item.deviceTypeId === deviceTypeId) {
                return item;
            }
        }
    }   

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);