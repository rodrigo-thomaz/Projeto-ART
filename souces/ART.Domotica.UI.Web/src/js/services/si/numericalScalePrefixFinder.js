'use strict';
app.factory('numericalScalePrefixFinder', ['$rootScope', 'siContext', function ($rootScope, siContext) {

    var context = siContext;

    var serviceFactory = {};        

    var getByKey = function (numericalScalePrefixId) {
        for (var i = 0; i < context.numericalScalePrefix.length; i++) {
            var item = context.numericalScalePrefix[i];
            if (item.numericalScalePrefixId === numericalScalePrefixId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);