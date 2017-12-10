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

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);