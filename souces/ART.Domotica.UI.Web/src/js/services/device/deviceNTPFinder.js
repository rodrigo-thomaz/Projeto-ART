'use strict';
app.factory('deviceNTPFinder', ['$rootScope', 'deviceContext', function ($rootScope, deviceContext) {

    var context = deviceContext;

    var serviceFactory = {};      

    var getByKey = function (deviceTypeId, deviceDatasheetId, deviceId) {
        for (var i = 0; i < context.deviceNTP.length; i++) {
            var item = context.deviceNTP[i];
            if (item.deviceTypeId === deviceTypeId && item.deviceDatasheetId === deviceDatasheetId && item.deviceId === deviceId) {
                return item;
            }
        }
    };

    var getByTimeZoneKey = function (timeZoneId) {
        var result = [];
        for (var i = 0; i < context.deviceNTP.length; i++) {
            var deviceNTP = context.deviceNTP[i];
            if (deviceNTP.timeZoneId === timeZoneId) {
                result.push(deviceNTP);
            }
        }
        return result;
    };

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;
    serviceFactory.getByTimeZoneKey = getByTimeZoneKey;

    return serviceFactory;

}]);