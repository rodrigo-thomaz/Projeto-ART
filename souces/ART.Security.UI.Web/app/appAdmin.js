
var app = angular.module('AngularAuthApp', ['ngRoute', 'ngStorage', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "app/views/admin/home.html"
    });

    $routeProvider.when("/orders", {
        controller: "ordersController",
        templateUrl: "app/views/admin/orders.html"
    });

    $routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "app/views/admin/tokens.html"
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "app/views/admin/associate.html"
    });

    $routeProvider.when("/dashboard", {
        controller: "dashboardController",
        templateUrl: "app/views/admin/dashboard.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });

});

//var segurancaDistributedServicesUri = 'http://localhost:26264/';
var segurancaDistributedServicesUri = 'http://localhost/ART.Security.WebApi/';
var distributedServicesUri = 'http://localhost/ART.DistributedServices/';

app.constant('ngAuthSettings', {
    segurancaDistributedServicesUri: segurancaDistributedServicesUri,
    distributedServicesUri: distributedServicesUri,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


