'use strict';
app.factory('applicationService', ['$http', '$log', 'ngAuthSettings', '$rootScope', 'stompService', function ($http, $log, ngAuthSettings, $rootScope, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};    

    var initialized = false;

    var onConnected = function () {
        stompService.subscribe('Application.GetViewCompleted', onGetCompleted);
        if (!initialized) {
            initialized = true;
            get();
        }
    }   

    var get = function () {
        return $http.post(serviceBase + 'api/application/get').then(function (results) {
            //alert('envio bem sucedido');
        });
    };

    var onGetCompleted = function (payload) {        
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        serviceFactory.application = JSON.parse(dataUTF8);
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);       

    // stompService
    if (stompService.connected()) onConnected();

    // serviceFactory

    serviceFactory.application = {};  

    return serviceFactory;   

}]);