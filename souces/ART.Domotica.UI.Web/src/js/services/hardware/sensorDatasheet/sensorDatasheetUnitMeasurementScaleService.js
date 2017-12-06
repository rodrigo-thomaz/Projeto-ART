'use strict';
app.factory('sensorDatasheetUnitMeasurementScaleService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'sensorDatasheetContext', 'sensorDatasheetUnitMeasurementScaleConstant', function ($http, ngAuthSettings, $rootScope, stompService, sensorDatasheetContext, sensorDatasheetUnitMeasurementScaleConstant) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var serviceFactory = {};    

    var onConnected = function () {

        stompService.subscribe('SensorDatasheetUnitMeasurementScale.GetAllViewCompleted', onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + 'api/sensorDatasheetUnitMeasurementScale/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };       

    var onGetAllCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            sensorDatasheetContext.sensorDatasheetUnitMeasurementScale.push(data[i]);
        }
        sensorDatasheetContext.sensorDatasheetUnitMeasurementScaleLoaded = true;
        _initializing = false;
        _initialized = true;
        $rootScope.$emit('sensorDatasheetUnitMeasurementScaleService_Initialized');
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