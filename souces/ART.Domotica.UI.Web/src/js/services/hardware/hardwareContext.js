'use strict';
app.factory('hardwareContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    context.hardware = [];
    context.hardwareLoaded = false;    

    return context;

}]);