'use strict';
app.factory('globalizationContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    context.timeZoneLoaded = false;
    context.timeZone = [];

    return context;

}]);