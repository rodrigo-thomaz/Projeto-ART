'use strict';
app.factory('sensorContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();
        
    context.sensorLoaded = false;
    context.sensor = [];

    return context;

}]);