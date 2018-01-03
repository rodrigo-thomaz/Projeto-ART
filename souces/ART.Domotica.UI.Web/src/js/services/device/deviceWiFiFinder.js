'use strict';
app.factory('deviceWiFiFinder', ['$rootScope', 'deviceContext', function ($rootScope, deviceContext) {

    var context = deviceContext;

    var serviceFactory = {};      

    var getByKey = function (deviceWiFiId, deviceDatasheetId) {
        for (var i = 0; i < context.deviceWiFi.length; i++) {
            var item = context.deviceWiFi[i];
            if (item.deviceWiFiId === deviceWiFiId && item.deviceDatasheetId === deviceDatasheetId) {
                return item;
            }
        }
    };

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);