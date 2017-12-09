'use strict';
app.factory('sensorDatasheetUnitMeasurementDefaultService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'sensorDatasheetContext', 'sensorDatasheetUnitMeasurementDefaultConstant', function ($http, ngAuthSettings, $rootScope, stompService, sensorDatasheetContext, sensorDatasheetUnitMeasurementDefaultConstant) {

    var serviceFactory = {};    

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var getAllCompletedSubscription = null;

    var onConnected = function () {

        getAllCompletedSubscription = stompService.subscribe(sensorDatasheetUnitMeasurementDefaultConstant.getAllCompletedTopic, onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + sensorDatasheetUnitMeasurementDefaultConstant.getAllApiUri).then(function (results) {
            //alert('envio bem sucedido');
        });
    };     
    
    var onGetAllCompleted = function (payload) {

        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);

        for (var i = 0; i < data.length; i++) {
            sensorDatasheetContext.sensorDatasheetUnitMeasurementDefault.push(data[i]);
        }

        sensorDatasheetContext.$digest();

        _initializing = false;
        _initialized = true;

        clearOnConnected();

        getAllCompletedSubscription.unsubscribe();

        $rootScope.$emit(sensorDatasheetUnitMeasurementDefaultConstant.getAllCompletedEventName);
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