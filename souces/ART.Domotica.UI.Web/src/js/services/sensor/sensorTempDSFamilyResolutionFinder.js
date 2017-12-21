'use strict';
app.factory('sensorTempDSFamilyResolutionFinder', ['$rootScope', 'sensorContext', function ($rootScope, sensorContext) {

    var context = sensorContext;

    var serviceFactory = {};

    var getByKey = function (sensorTempDSFamilyResolutionId) {
        for (var i = 0; i < context.sensorTempDSFamilyResolution.length; i++) {
            var item = context.sensorTempDSFamilyResolution[i];
            if (item.sensorTempDSFamilyResolutionId === sensorTempDSFamilyResolutionId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);