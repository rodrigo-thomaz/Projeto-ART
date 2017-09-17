
var app = angular.module('AngularAuthApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar']);

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

var segurancaPresentationWebPagesUri = 'http://localhost/ART.Seguranca.Presentation.WebPages/index.html';
var segurancaDistributedServicesUri = 'http://localhost/ART.Seguranca.DistributedServices/';
var corporativoDistributedServicesUri = 'http://localhost/ART.Corporativo.DistributedServices/';

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


