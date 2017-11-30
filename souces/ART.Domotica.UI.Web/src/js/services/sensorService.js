'use strict';
app.factory('sensorService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', function ($http, ngAuthSettings, $rootScope, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var serviceFactory = {};    

    var onConnected = function () {

        stompService.subscribe('Sensor.GetAllViewCompleted', onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + 'api/sensor/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };     

    var getSensorById = function (sensorId) {
        for (var i = 0; i < serviceFactory.sensors.length; i++) {
            if (serviceFactory.sensors[i].id === sensorId) {
                return serviceFactory.sensors[i];
            }
        }
    };

    var onGetAllCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            serviceFactory.sensors.push(data[i]);
        }
        _initializing = false;
        _initialized = true;
        $rootScope.$emit('sensorService_Initialized');
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected);        

    // stompService
    if (stompService.connected())
        onConnected();

    // serviceFactory
        
    serviceFactory.sensors = [];  

    serviceFactory.initialized = initialized;
    serviceFactory.getSensorById = getSensorById;    

    return serviceFactory;

}]);