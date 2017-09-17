var ApplicationScript = function () {

    var _applicationUrl;
    var _templateUrl;
    var _appWebApiUrl;
    var _identityWebApiUrl;

    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-bottom-right",
        "onclick": null,
        "showDuration": "1000",
        "hideDuration": "1000",
        "timeOut": "5000",
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }

    var handleInit = function (applicationUrl, templateUrl, appWebApiUrl, identityWebApiUrl) {
        _applicationUrl = applicationUrl;
        _templateUrl = templateUrl;
        _appWebApiUrl = appWebApiUrl;
        _identityWebApiUrl = identityWebApiUrl;
    }

    var handleGetApplicationUrl = function () {
        return _applicationUrl;
    }

    var handleGetTemplateUrl = function () {
        return _templateUrl;
    }

    var handleGetAppWebApiUrl = function () {
        return _appWebApiUrl;
    }

    var handleGetIdentityWebApiUrl = function () {
        return _identityWebApiUrl;
    }

    var handleGetToken = function () {
        return localStorage.getItem("token");
    }

    var handleSetToken = function (token) {
        localStorage.setItem("token", token);
    }

    var handleError = function (jqXHR, textStatus, errorThrown) {
        if (jqXHR.status == 401 || jqXHR.status == 403) {
            window.location.href = '/Seguranca/Login?ReturnUrl=' + window.location.pathname;
        }
        else{
            toastr['error']("Ops, erro!', 'Ocorreu um erro inesperado no sistema. O administrador já foi informado");
            console.log(jqXHR.responseJSON)
        }        
    }

    return {
        init: function (applicationUrl, templateUrl, appWebApiUrl, identityWebApiUrl) {
            handleInit(applicationUrl, templateUrl, appWebApiUrl, identityWebApiUrl);
        },
        getApplicationUrl: function () {
            return handleGetApplicationUrl();
        },
        getTemplateUrl: function () {
            return handleGetTemplateUrl();
        },
        getAppWebApiUrl: function () {
            return handleGetAppWebApiUrl();
        },
        getIdentityWebApiUrl: function () {
            return handleGetIdentityWebApiUrl();
        },
        getToken: function () {
            return handleGetToken();
        },
        setToken: function (token) {
            handleSetToken(token);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            handleError(jqXHR, textStatus, errorThrown);
        },
    };

}();