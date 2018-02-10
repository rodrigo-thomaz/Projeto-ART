'use strict';
app.factory('deviceSerialFinder', ['$rootScope', 'deviceContext', function ($rootScope, deviceContext) {

    var context = deviceContext;

    var serviceFactory = {};      

    var getByKey = function (deviceSerialId, deviceId, deviceDatasheetId) {
        for (var i = 0; i < context.deviceSerial.length; i++) {
            var item = context.deviceSerial[i];
            if (item.deviceSerialId === deviceSerialId && item.deviceId === deviceId && item.deviceDatasheetId === deviceDatasheetId) {
                return item;
            }
        }
    };

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);