
var app = angular.module('AngularAuthApp', ['ngRoute', 'ngStorage', 'angular-loading-bar']);

app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "app/views/home.html"
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

    $routeProvider.when("/minhaConta", {
        controller: "minhaContaController",
        templateUrl: "app/views/minhaConta.html"
    });

    $routeProvider.otherwise({ redirectTo: "/home" });

});

var segurancaPresentationWebPagesUri = 'http://dev02-pc/ART.Security.UI.Web/';
var segurancaDistributedServicesUri = 'http://dev02-pc/ART.Security.WebApi/';
var corporativoDistributedServicesUri = 'http://dev02-pc/ART.Corporativo.WebApi/';

app.constant('ngAuthSettings', {
    segurancaPresentationWebPagesUri: segurancaPresentationWebPagesUri,
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


