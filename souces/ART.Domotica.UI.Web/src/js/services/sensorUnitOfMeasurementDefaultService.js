'use strict';
app.factory('sensorUnitOfMeasurementDefaultService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', function ($http, ngAuthSettings, $rootScope, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var serviceFactory = {};    

    var onConnected = function () {

        stompService.subscribe('SensorUnitOfMeasurementDefault.GetAllViewCompleted', onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + 'api/sensorUnitOfMeasurementDefault/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };     

    var getSensorUnitOfMeasurementDefaultById = function (sensorUnitOfMeasurementDefaultId) {
        for (var i = 0; i < serviceFactory.sensorUnitOfMeasurementDefaults.length; i++) {
            if (serviceFactory.sensorUnitOfMeasurementDefaults[i].id === sensorUnitOfMeasurementDefaultId) {
                return serviceFactory.sensorUnitOfMeasurementDefaults[i];
            }
        }
    };

    var onGetAllCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            serviceFactory.sensorUnitOfMeasurementDefaults.push(data[i]);
        }
        _initializing = false;
        _initialized = true;
        $rootScope.$emit('sensorUnitOfMeasurementDefaultService_Initialized');
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected);        

    // stompService
    if (stompService.connected())
        onConnected();

    // serviceFactory
        
    serviceFactory.sensorUnitOfMeasurementDefaults = [];  

    serviceFactory.initialized = initialized;
    serviceFactory.getSensorUnitOfMeasurementDefaultById = getSensorUnitOfMeasurementDefaultById;    

    return serviceFactory;

}]);