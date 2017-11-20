
var app = angular.module('AngularAuthApp', ['ngRoute', 'ngStorage', 'angular-loading-bar', 'angular-event-dispatcher']);

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

var segurancaDistributedServicesUri = 'http://dev02-pc/ART.Security.WebApi/';
var distributedServicesUri = 'http://dev02-pc/ART.DistributedServices/';
var defaultRedirectUri = 'http://dev02-pc/ART.Corporativo.UI.Web/';

var wsBrokerHostName = 'file-server';
var wsBrokerPort = 15674;

app.constant('ngAuthSettings', {
    segurancaDistributedServicesUri: segurancaDistributedServicesUri,
    distributedServicesUri: distributedServicesUri,
    wsBrokerHostName: wsBrokerHostName,
    wsBrokerPort: wsBrokerPort,
    defaultRedirectUri: defaultRedirectUri,
    clientId: 'ngAuthApp',
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


