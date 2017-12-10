'use strict';
app.factory('sensorInDeviceFinder', ['$rootScope', 'deviceContext', function ($rootScope, deviceContext) {

    var context = deviceContext;

    var serviceFactory = {};    

    var getByKey = function (sensorInDeviceId) {
        for (var i = 0; i < context.sensorInDevice.length; i++) {
            var item = context.sensorInDevice[i];
            if (item.sensorInDeviceId === sensorInDeviceId) {
                return item;
            }
        }
    };

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);