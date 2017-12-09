'use strict';
app.factory('numericalScaleFinder', ['$rootScope', 'siContext', function ($rootScope, siContext) {

    var context = siContext;

    var serviceFactory = {};       

    var getByKey = function (numericalScalePrefixId, numericalScaleTypeId) {
        for (var i = 0; i < context.numericalScale.length; i++) {
            var item = context.numericalScale[i];
            if (item.numericalScalePrefixId === numericalScalePrefixId && item.numericalScaleTypeId === numericalScaleTypeId) {
                return item;
            }
        }
    }

    var getByNumericalScaleTypeKey = function (numericalScaleTypeId) {
        var result = [];
        for (var i = 0; i < context.numericalScale.length; i++) {
            if (context.numericalScale[i].numericalScaleTypeId === numericalScaleTypeId) {
                result.push(context.numericalScale[i]);
            }
        }
        return result;
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;
    serviceFactory.getByNumericalScaleTypeKey = getByNumericalScaleTypeKey;

    return serviceFactory;

}]);