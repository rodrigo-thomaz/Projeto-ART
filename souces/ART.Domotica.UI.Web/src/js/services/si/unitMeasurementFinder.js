'use strict';
app.factory('unitMeasurementFinder', ['$rootScope', 'siContext', function ($rootScope, siContext) {

    var context = siContext;

    var serviceFactory = {};     

    var getByKey = function (unitMeasurementId, unitMeasurementTypeId) {
        for (var i = 0; i < context.unitMeasurement.length; i++) {
            var item = context.unitMeasurement[i];
            if (item.unitMeasurementId === unitMeasurementId && item.unitMeasurementTypeId === unitMeasurementTypeId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);