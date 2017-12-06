'use strict';
app.factory('deviceContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    // *** Public Properties ***       
    
    context.deviceLoaded = false;

    context.device = [];   
    context.deviceNTP = [];   
    context.deviceSensors = [];   
    context.sensorsInDevice = []; 
        
    return context;

}]);