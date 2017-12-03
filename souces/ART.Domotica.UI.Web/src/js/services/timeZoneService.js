'use strict';
app.factory('timeZoneService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', function ($http, ngAuthSettings, $rootScope, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var serviceFactory = {};    

    var onConnected = function () {

        stompService.subscribe('TimeZone.GetAllViewCompleted', onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + 'api/timeZone/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };     

    var getTimeZoneById = function (timeZoneId) {
        for (var i = 0; i < serviceFactory.timeZones.length; i++) {
            if (serviceFactory.timeZones[i].id === timeZoneId) {
                return serviceFactory.timeZones[i];
            }
        }
    };

    var onGetAllCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            serviceFactory.timeZones.push(data[i]);
        }
        _initializing = false;
        _initialized = true;
        $rootScope.$emit('timeZoneService_Initialized');
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);       

    // stompService
    if (stompService.connected())
        onConnected();

    // serviceFactory
        
    serviceFactory.timeZones = [];  

    serviceFactory.initialized = initialized;
    serviceFactory.getTimeZoneById = getTimeZoneById;    

    return serviceFactory;

}]);