'use strict';
app.factory('unitMeasurementService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'siContext', function ($http, ngAuthSettings, $rootScope, stompService, siContext) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var getAllCompletedTopic = 'SI.UnitMeasurement.GetAllViewCompleted';
    var getAllCompletedSubscription = null;

    var serviceFactory = {};    

    var onConnected = function () {

        getAllCompletedSubscription = stompService.subscribe(getAllCompletedTopic, onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + 'api/si/unitMeasurement/getAll').then(function (results) {
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

        $rootScope.$emit('UnitMeasurementService_Initialized');
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected);        

    // stompService
    if (stompService.connected())
        onConnected();

    // serviceFactory

    serviceFactory.initialized = initialized;

    return serviceFactory;

}]);