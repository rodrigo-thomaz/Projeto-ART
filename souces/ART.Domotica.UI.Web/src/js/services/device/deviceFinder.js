'use strict';
app.factory('deviceFinder', ['$rootScope', 'deviceContext', function ($rootScope, deviceContext) {

    var context = deviceContext;

    var serviceFactory = {};    

    var getByKey = function (deviceId) {
        for (var i = 0; i < context.device.length; i++) {
            var item = context.device[i];
            if (item.deviceId === deviceId) {
                return item;
            }
        }
    };

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);