'use strict';
app.factory('sensorTempDSFamilyFinder', ['$rootScope', 'sensorContext', function ($rootScope, sensorContext) {

    var context = sensorContext;

    var serviceFactory = {};

    var getByKey = function (sensorTempDSFamilyId) {
        for (var i = 0; i < context.sensorTempDSFamily.length; i++) {
            var item = context.sensorTempDSFamily[i];
            if (item.sensorTempDSFamilyId === sensorTempDSFamilyId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);