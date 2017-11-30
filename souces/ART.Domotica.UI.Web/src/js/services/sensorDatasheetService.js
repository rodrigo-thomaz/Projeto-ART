'use strict';
app.factory('sensorDatasheetService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', function ($http, ngAuthSettings, $rootScope, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var serviceFactory = {};    

    var onConnected = function () {

        stompService.subscribe('SensorDatasheet.GetAllViewCompleted', onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + 'api/sensorDatasheet/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };     

    var getSensorDatasheetById = function (sensorDatasheetId) {
        for (var i = 0; i < serviceFactory.sensorDatasheets.length; i++) {
            if (serviceFactory.sensorDatasheets[i].id === sensorDatasheetId) {
                return serviceFactory.sensorDatasheets[i];
            }
        }
    };

    var onGetAllCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            serviceFactory.sensorDatasheets.push(data[i]);
        }
        _initializing = false;
        _initialized = true;
        $rootScope.$emit('sensorDatasheetService_Initialized');
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected);        

    // stompService
    if (stompService.connected())
        onConnected();

    // serviceFactory
        
    serviceFactory.sensorDatasheets = [];  

    serviceFactory.initialized = initialized;
    serviceFactory.getSensorDatasheetById = getSensorDatasheetById;    

    return serviceFactory;

}]);