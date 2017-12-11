'use strict';
app.factory('deviceNTPFinder', ['$rootScope', 'deviceContext', function ($rootScope, deviceContext) {

    var context = deviceContext;

    var serviceFactory = {};      

    var getByKey = function (deviceNTPId) {
        for (var i = 0; i < context.deviceNTP.length; i++) {
            var item = context.deviceNTP[i];
            if (item.deviceNTPId === deviceNTPId) {
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