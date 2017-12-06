'use strict';
app.factory('sensorDatasheetUnitMeasurementScaleService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'sensorDatasheetContext', 'sensorDatasheetUnitMeasurementScaleConstant', function ($http, ngAuthSettings, $rootScope, stompService, sensorDatasheetContext, sensorDatasheetUnitMeasurementScaleConstant) {

    var serviceFactory = {};    

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var getAllCompletedSubscription = null;

    var onConnected = function () {

        getAllCompletedSubscription = stompService.subscribe(sensorDatasheetUnitMeasurementScaleConstant.getAllCompletedTopic, onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + sensorDatasheetUnitMeasurementScaleConstant.getAllApiUri).then(function (results) {
            //alert('envio bem sucedido');
        });
    };       

    var onGetAllCompleted = function (payload) {

        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);

        for (var i = 0; i < data.length; i++) {
            sensorDatasheetContext.sensorDatasheetUnitMeasurementScale.push(data[i]);
        }
                
        _initializing = false;
        _initialized = true;

        clearOnConnected();

        getAllCompletedSubscription.unsubscribe();

        $rootScope.$emit(sensorDatasheetUnitMeasurementScaleConstant.getAllCompletedEventName);
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