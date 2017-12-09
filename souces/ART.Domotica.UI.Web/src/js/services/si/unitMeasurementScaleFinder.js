'use strict';
app.factory('unitMeasurementScaleFinder', ['$rootScope', 'siContext', function ($rootScope, siContext) {

    var context = siContext;

    var serviceFactory = {};    
    
    var getByKey = function (unitMeasurementId, unitMeasurementTypeId, numericalScalePrefixId, numericalScaleTypeId) {
        for (var i = 0; i < context.unitMeasurementScale.length; i++) {
            var item = context.unitMeasurementScale[i];
            if (item.unitMeasurementId === unitMeasurementId && item.unitMeasurementTypeId === unitMeasurementTypeId && item.numericalScalePrefixId === numericalScalePrefixId && item.numericalScaleTypeId === numericalScaleTypeId) {
                return item;
            }
        }
    }      

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);