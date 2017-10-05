
var app = angular.module('AngularAuthApp', ['ngRoute', 'ngStorage', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "app/views/authentication/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "app/views/authentication/signup.html"
    });

    $routeProvider.when("/refresh", {
        controller: "refreshController",
        templateUrl: "app/views/authentication/refresh.html"
    });    

    $routeProvider.otherwise({ redirectTo: "/login" });

});

//var segurancaDistributedServicesUri = 'http://localhost:26264/';
var segurancaDistributedServicesUri = 'http://localhost/ART.Security.WebApi/';
var distributedServicesUri = 'http://localhost/ART.DistributedServices/';
var defaultRedirectUri = 'http://localhost/ART.Corporativo.UI.Web/';

app.constant('ngAuthSettings', {
    segurancaDistributedServicesUri: segurancaDistributedServicesUri,
    distributedServicesUri: distributedServicesUri,
    defaultRedirectUri: defaultRedirectUri,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


