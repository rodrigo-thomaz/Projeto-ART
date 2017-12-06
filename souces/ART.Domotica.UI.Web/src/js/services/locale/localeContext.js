'use strict';
app.factory('localeContext', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    context.continents = [];    
    context.continentLoaded = false;    

    context.countries = [];    
    context.countryLoaded = false;            

    return context;

}]);