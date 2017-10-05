'use strict';
app.factory('authService', ['$http', '$q', '$localStorage', 'ngAuthSettings', function ($http, $q, $localStorage, ngAuthSettings) {

    var serviceBase = ngAuthSettings.segurancaDistributedServicesUri;
    var segurancaPresentationWebPagesUri = ngAuthSettings.segurancaPresentationWebPagesUri;

    var authServiceFactory = {};

    var _authentication = {
        isAuth: false,
        userName: "",
        useRefreshTokens: false
    };

    var _logIn = function () {      
        window.location = segurancaPresentationWebPagesUri + '#/login' + '?returnurl=' + window.location.href;
    };

    var _signUp = function () {
        window.location = segurancaPresentationWebPagesUri + '#/signUp' + '?returnurl=' + window.location.href;
    };
    
    var _logOut = function () {

        delete $localStorage.authorizationData;

        _authentication.isAuth = false;
        _authentication.userName = "";
        _authentication.useRefreshTokens = false;        
    };

    var _fillAuthData = function () {

        var authData = $localStorage.authorizationData;
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
            _authentication.useRefreshTokens = authData.useRefreshTokens;
        }

    };

    var _refreshToken = function () {
        window.location = segurancaPresentationWebPagesUri + '#/refresh' + '?returnurl=' + window.location.href;
    };

    authServiceFactory.logIn = _logIn;
    authServiceFactory.signUp = _signUp;
    authServiceFactory.logOut = _logOut;
    authServiceFactory.fillAuthData = _fillAuthData;
    authServiceFactory.authentication = _authentication;
    authServiceFactory.refreshToken = _refreshToken;

    return authServiceFactory;
}]);