
var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "app/views/home.html"
    });

    $routeProvider.when("/test", {
        controller: "testController",
        templateUrl: "app/views/test.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });

});

var segurancaPresentationWebPagesUri = 'http://localhost/ART.Seguranca.Presentation.WebPages/index.html';
var segurancaDistributedServicesUri = 'http://localhost/ART.Seguranca.DistributedServices/';
var domoticaDistributedServicesUri = 'http://localhost/ART.Domotica.DistributedServices/';

app.constant('ngAuthSettings', {
    segurancaPresentationWebPagesUri: segurancaPresentationWebPagesUri,
    segurancaDistributedServicesUri: segurancaDistributedServicesUri,
    domoticaDistributedServicesUri: domoticaDistributedServicesUri,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);