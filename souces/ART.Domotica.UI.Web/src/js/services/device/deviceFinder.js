'use strict';
app.factory('deviceFinder', ['$rootScope', 'deviceContext', function ($rootScope, deviceContext) {

    var context = deviceContext;

    var serviceFactory = {};    

    var getByKey = function (deviceTypeId, deviceDatasheetId, deviceId) {
        for (var i = 0; i < context.device.length; i++) {
            var item = context.device[i];
            if (item.deviceTypeId === deviceTypeId && item.deviceDatasheetId === deviceDatasheetId && item.deviceId === deviceId) {
                return item;
            }
        }
    };

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);