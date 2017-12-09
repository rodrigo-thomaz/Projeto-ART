'use strict';
app.factory('numericalScaleTypeFinder', ['$rootScope', 'siContext', function ($rootScope, siContext) {

    var context = siContext;

    var serviceFactory = {};    

    var getByKey = function (numericalScaleTypeId) {
        for (var i = 0; i < context.numericalScaleType.length; i++) {
            var item = context.numericalScaleType[i];
            if (item.numericalScaleTypeId === numericalScaleTypeId) {
                return item;
            }
        }
    }

    // *** Public Methods ***

    serviceFactory.getByKey = getByKey;

    return serviceFactory;

}]);