'use strict';
app.factory('sensorUnitMeasurementScaleFinder', ['$rootScope', 'sensorContext', function ($rootScope, sensorContext) {

    var context = sensorContext;

    var serviceFactory = {};

    var getByKey = function (sensorUnitMeasurementScaleId) {
        for (var i = 0; i < context.sensorUnitMeasurementScale.length; i++) {
            var item = context.sensorUnitMeasurementScale[i];
            if (item.sensorUnitMeasurementScaleId === sensorUnitMeasurementScaleId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);