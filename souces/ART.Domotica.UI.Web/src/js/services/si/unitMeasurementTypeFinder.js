'use strict';
app.factory('unitMeasurementTypeFinder', ['$rootScope', 'siContext', function ($rootScope, siContext) {

    var context = siContext;

    var serviceFactory = {};     

    var getByKey = function (unitMeasurementTypeId) {
        for (var i = 0; i < context.unitMeasurementType.length; i++) {
            var item = context.unitMeasurementType[i];
            if (item.unitMeasurementTypeId === unitMeasurementTypeId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);