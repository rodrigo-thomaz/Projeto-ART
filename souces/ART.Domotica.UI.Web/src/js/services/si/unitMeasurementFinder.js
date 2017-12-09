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

    var getByUnitMeasurementTypeKey = function (unitMeasurementTypeId) {
        var result = [];
        for (var i = 0; i < context.unitMeasurement.length; i++) {
            var unitMeasurement = context.unitMeasurement[i];
            if (unitMeasurement.unitMeasurementTypeId === unitMeasurementTypeId) {
                result.push(context.unitMeasurement[i]);
            }
        }
        return result;
    }  

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;
    serviceFactory.getByUnitMeasurementTypeKey = getByUnitMeasurementTypeKey;

    return serviceFactory;

}]);