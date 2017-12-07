'use strict';
app.factory('deviceContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();
        
    context.device = [];   
    context.deviceLoaded = false;
        
    context.deviceNTP = [];   
    context.deviceNTPLoaded = false;

    context.deviceSensors = [];   
    context.deviceSensorsLoaded = false;

    context.sensorInDevice = []; 
    context.sensorInDeviceLoaded = false;
        
    return context;

}]);