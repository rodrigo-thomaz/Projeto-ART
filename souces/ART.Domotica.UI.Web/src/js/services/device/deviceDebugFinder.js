'use strict';
app.factory('deviceDebugFinder', ['$rootScope', 'deviceContext', function ($rootScope, deviceContext) {

    var context = deviceContext;

    var serviceFactory = {};      

    var getByKey = function (deviceDebugId, deviceDatasheetId) {
        for (var i = 0; i < context.deviceDebug.length; i++) {
            var item = context.deviceDebug[i];
            if (item.deviceDebugId === deviceDebugId && item.deviceDatasheetId === deviceDatasheetId) {
                return item;
            }
        }
    };

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);