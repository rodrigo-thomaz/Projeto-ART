'use strict';
app.factory('authInterceptorService', ['$q', '$injector', '$location', '$localStorage', 'applicationMQ', function ($q, $injector, $location, $localStorage, applicationMQ) {

    var authInterceptorServiceFactory = {};

    var _request = function (config) {

        config.headers = config.headers || {};

        config.headers.webUITopic = applicationMQ.webUITopic;
        
        var authData = $localStorage.authorizationData;
        if (authData) {
            config.headers.Authorization = 'Bearer ' + authData.token;
        }

        return config;
    }

    var _responseError = function (rejection) {
        if (rejection.status === 401) {
            var authService = $injector.get('authService');
            var authData = $localStorage.authorizationData;

            if (authData) {
                if (authData.useRefreshTokens) {
                    authService.refreshToken();
                    return $q.reject(rejection);
                }
            }
            authService.logOut();
        }
        return $q.reject(rejection);
    }

    authInterceptorServiceFactory.request = _request;
    authInterceptorServiceFactory.responseError = _responseError;

    return authInterceptorServiceFactory;
}]);