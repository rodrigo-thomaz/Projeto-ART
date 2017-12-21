'use strict';
app.factory('deviceDatasheetContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();
        
    context.deviceDatasheetLoaded = false;
    context.deviceDatasheet = [];

    return context;

}]);