'use strict';
app.factory('hardwareFinder', ['$rootScope', 'hardwareContext', function ($rootScope, hardwareContext) {

    var context = hardwareContext;

    var serviceFactory = {};    

    var getByKey = function (hardwareId) {
        for (var i = 0; i < context.hardware.length; i++) {
            var item = context.hardware[i];
            if (item.hardwareId === hardwareId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);