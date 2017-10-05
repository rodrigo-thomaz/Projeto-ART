
var app = angular.module('AngularAuthApp', ['ngRoute', 'ngStorage', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "app/views/signup.html"
    });

    $routeProvider.when("/orders", {
        controller: "ordersController",
        templateUrl: "app/views/orders.html"
    });

    $routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl: "app/views/refresh.html"
    });

    $routeProvider.when("/tokens", {
        controller: "tokensManagerController",
        templateUrl: "app/views/tokens.html"
    });

    $routeProvider.when("/associate", {
        controller: "associateController",
        templateUrl: "app/views/associate.html"
    });

    $routeProvider.when("/dashboard", {
        controller: "dashboardController",
        templateUrl: "app/views/dashboard.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });

});

//var segurancaDistributedServicesUri = 'http://localhost:26264/';
var segurancaDistributedServicesUri = 'http://localhost/ART.Seguranca.DistributedServices/';
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


