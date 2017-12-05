'use strict';
app.factory('deviceMapper', ['$rootScope', 'deviceContext', function ($rootScope, deviceContext) {

    var serviceFactory = {};    

    // *** Navigation Properties Mappers ***
        

    // *** Watches ***    

    context.$watch('devicesLoaded', function (newValue, oldValue) {

    });

    context.$watch('deviceNTPsLoaded', function (newValue, oldValue) {

    });

    context.$watch('deviceSensorsLoaded', function (newValue, oldValue) {

    });

    context.$watch('sensorsInDevicesLoaded', function (newValue, oldValue) {

    });        
    
    return serviceFactory;

}]);