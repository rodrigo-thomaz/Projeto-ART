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

    var getBySensorTempDSFamilyResolutionKey = function (sensorTempDSFamilyResolutionId) {
        var result = [];
        for (var i = 0; i < context.sensorTempDSFamily.length; i++) {
            var sensorTempDSFamily = context.sensorTempDSFamily[i];
            if (sensorTempDSFamily.sensorTempDSFamilyResolutionId === sensorTempDSFamilyResolutionId) {
                result.push(sensorTempDSFamily);
            }
        }
        return result;
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;
    serviceFactory.getBySensorTempDSFamilyResolutionKey = getBySensorTempDSFamilyResolutionKey;

    return serviceFactory;

}]);