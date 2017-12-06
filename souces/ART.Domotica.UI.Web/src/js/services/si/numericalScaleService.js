'use strict';
app.factory('numericalScaleService', ['$http', 'ngAuthSettings', 'numericalScaleConstant', '$rootScope', 'stompService', 'siContext', function ($http, ngAuthSettings, numericalScaleConstant, $rootScope, stompService, siContext) {

    var serviceFactory = {};    

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var getAllCompletedSubscription = null;

    var initializedEventName = 'numericalScaleService.onInitialized';

    var onConnected = function () {

        getAllCompletedSubscription = stompService.subscribe(numericalScaleConstant.getAllCompletedTopic, onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + numericalScaleConstant.getAllApiUri).then(function (results) {
            //alert('envio bem sucedido');
        });
    };       

    var onGetAllCompleted = function (payload) {

        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);

        for (var i = 0; i < data.length; i++) {
            siContext.numericalScales.push(data[i]);
        }
        
        _initializing = false;
        _initialized = true;

        siContext.numericalScaleLoaded = true;
        clearOnConnected();

        getAllCompletedSubscription.unsubscribe();

        $rootScope.$emit(numericalScaleConstant.initializedEventName);
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