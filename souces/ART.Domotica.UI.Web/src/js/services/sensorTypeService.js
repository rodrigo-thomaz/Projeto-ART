'use strict';
app.factory('sensorTypeService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', function ($http, ngAuthSettings, $rootScope, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var serviceFactory = {};    

    var onConnected = function () {

        stompService.subscribe('SensorType.GetAllViewCompleted', onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + 'api/sensorType/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };     

    var getSensorTypeById = function (sensorTypeId) {
        for (var i = 0; i < serviceFactory.sensorTypes.length; i++) {
            if (serviceFactory.sensorTypes[i].id === sensorTypeId) {
                return serviceFactory.sensorTypes[i];
            }
        }
    };

    var onGetAllCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            serviceFactory.sensorTypes.push(data[i]);
        }
        _initializing = false;
        _initialized = true;
        $rootScope.$emit('sensorTypeService_Initialized');
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected);        

    // stompService
    if (stompService.connected())
        onConnected();

    // serviceFactory
        
    serviceFactory.sensorTypes = [];  

    serviceFactory.initialized = initialized;
    serviceFactory.getSensorTypeById = getSensorTypeById;    

    return serviceFactory;

}]);