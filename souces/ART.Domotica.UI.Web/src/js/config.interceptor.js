// config

var app =
    angular.module('app')        
        .run(
        ['authService', function (authService) {            
            authService.fillAuthData();
        }])
        .config(['$httpProvider', function ($httpProvider) {
            $httpProvider.interceptors.push('authInterceptorService');
        }]);