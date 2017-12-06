'use strict';
app.factory('unitMeasurementService', ['$http', 'ngAuthSettings', 'unitMeasurementConstant', '$rootScope', 'stompService', 'siContext', function ($http, ngAuthSettings, unitMeasurementConstant, $rootScope, stompService, siContext) {

    var serviceFactory = {};    

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var getAllCompletedSubscription = null;

    var onConnected = function () {

        getAllCompletedSubscription = stompService.subscribe(unitMeasurementConstant.getAllCompletedTopic, onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + unitMeasurementConstant.getAllApiUri).then(function (results) {
            //alert('envio bem sucedido');
        });
    };        

    var onGetAllCompleted = function (payload) {

        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);

        for (var i = 0; i < data.length; i++) {
            siContext.unitMeasurements.push(data[i]);
        }
        
        _initializing = false;
        _initialized = true;

        siContext.unitMeasurementLoaded = true;
        clearOnConnected();

        getAllCompletedSubscription.unsubscribe();

        $rootScope.$emit(unitMeasurementConstant.getAllCompletedEventName);
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