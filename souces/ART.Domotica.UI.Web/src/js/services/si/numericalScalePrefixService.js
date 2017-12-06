'use strict';
app.factory('numericalScalePrefixService', ['$http', 'ngAuthSettings', 'numericalScalePrefixConstant', '$rootScope', 'stompService', 'siContext', function ($http, ngAuthSettings, numericalScalePrefixConstant, $rootScope, stompService, siContext) {

    var serviceFactory = {};    

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var getAllCompletedSubscription = null;

    var initializedEventName = 'numericalScalePrefixService.onInitialized';
    
    var onConnected = function () {

        getAllCompletedSubscription = stompService.subscribe(numericalScalePrefixConstant.getAllCompletedTopic, onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + numericalScalePrefixConstant.getAllApiUri).then(function (results) {
            //alert('envio bem sucedido');
        });
    };       

    var onGetAllCompleted = function (payload) {

        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);

        for (var i = 0; i < data.length; i++) {
            siContext.numericalScalePrefixes.push(data[i]);
        }
        
        _initializing = false;
        _initialized = true;

        siContext.numericalScalePrefixLoaded = true;
        clearOnConnected();

        getAllCompletedSubscription.unsubscribe();

        $rootScope.$emit(numericalScalePrefixConstant.initializedEventName);
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);       

    // stompService
    if (stompService.connected()) onConnected();

    // serviceFactory

    serviceFactory.initialized = initialized;

    return serviceFactory;

}]);