
var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

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

    $routeProvider.when("/empresa", {
        controller: "empresaController",
        templateUrl: "app/views/empresa.html"
    });

    $routeProvider.when("/produtos", {
        controller: "produtosController",
        templateUrl: "app/views/produtos.html"
    });

    $routeProvider.when("/contato", {
        controller: "contatoController",
        templateUrl: "app/views/contato.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });

});

var segurancaDistributedServicesUri = 'http://localhost/ART.Seguranca.DistributedServices/';
var corporativoDistributedServicesUri = 'http://localhost/ART.Corporativo.DistributedServices/';

app.constant('ngAuthSettings', {
    segurancaDistributedServicesUri: segurancaDistributedServicesUri,
    corporativoDistributedServicesUri: corporativoDistributedServicesUri,
    clientId: 'ngAuthApp'
});

app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


