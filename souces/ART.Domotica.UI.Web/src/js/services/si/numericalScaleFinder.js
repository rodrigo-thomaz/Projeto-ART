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
       
    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);