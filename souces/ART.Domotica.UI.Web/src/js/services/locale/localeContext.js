'use strict';
app.factory('localeContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    context.continent = [];    
    context.continentLoaded = false;    

    context.country = [];    
    context.countryLoaded = false;            

    return context;

}]);