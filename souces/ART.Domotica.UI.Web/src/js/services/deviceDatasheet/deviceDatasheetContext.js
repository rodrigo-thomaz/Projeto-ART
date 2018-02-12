'use strict';
app.factory('deviceDatasheetContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();
        
    context.deviceTypeLoaded = false;
    context.deviceType = [];

    context.deviceDatasheetLoaded = false;
    context.deviceDatasheet = [];

    return context;

}]);