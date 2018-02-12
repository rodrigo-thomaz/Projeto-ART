'use strict';
app.factory('deviceSerialFinder', ['$rootScope', 'deviceContext', function ($rootScope, deviceContext) {

    var context = deviceContext;

    var serviceFactory = {};      

    var getByKey = function (deviceTypeId, deviceDatasheetId, deviceId, deviceSerialId) {
        for (var i = 0; i < context.deviceSerial.length; i++) {
            var item = context.deviceSerial[i];
            if (item.deviceTypeId === deviceTypeId && item.deviceDatasheetId === deviceDatasheetId && item.deviceId === deviceId && item.deviceSerialId === deviceSerialId) {
                return item;
            }
        }
    };

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);