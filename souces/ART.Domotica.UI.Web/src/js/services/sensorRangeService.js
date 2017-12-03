'use strict';
app.factory('sensorRangeService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', function ($http, ngAuthSettings, $rootScope, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var serviceFactory = {};    

    var onConnected = function () {

        stompService.subscribe('SensorRange.GetAllViewCompleted', onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + 'api/sensorRange/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };     

    var getById = function (sensorRangeId) {
        for (var i = 0; i < serviceFactory.sensorRanges.length; i++) {
            if (serviceFactory.sensorRanges[i].sensorRangeId === sensorRangeId) {
                return serviceFactory.sensorRanges[i];
            }
        }
    };

    var onGetAllCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            serviceFactory.sensorRanges.push(data[i]);
        }
        _initializing = false;
        _initialized = true;
        $rootScope.$emit('sensorRangeService_Initialized');
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);       

    // stompService
    if (stompService.connected())
        onConnected();

    // serviceFactory
        
    serviceFactory.sensorRanges = [];  

    serviceFactory.initialized = initialized;
    serviceFactory.getById = getById;    

    return serviceFactory;

}]);