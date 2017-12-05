'use strict';
app.factory('mainContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    // *** Public Properties ***

    context.application = {};
    context.applicationLoaded = false;

    // *** Finders ***

    // *** Public Methods ***

    return context;

}]);