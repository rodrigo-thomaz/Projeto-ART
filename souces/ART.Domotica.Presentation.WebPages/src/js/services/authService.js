'use strict';
app.factory('authService', ['$http', '$q', 'localStorageService', 'ngAuthSettings', function ($http, $q, localStorageService, ngAuthSettings) {

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

        localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";
        _authentication.useRefreshTokens = false;
    };

    var _fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
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