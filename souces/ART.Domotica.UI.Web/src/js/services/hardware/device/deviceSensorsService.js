'use strict';
app.factory('deviceSensorsService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', function ($http, ngAuthSettings, $rootScope, stompService, deviceContext) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};    

    var _initializing = false;
    var _initialized  = false;
        
    var getAllByApplicationIdApiUri = 'api/deviceSensors/getAllByApplicationId';
    var getAllByApplicationIdCompletedTopic = 'DeviceSensors.GetAllByApplicationIdViewCompleted';
    var getAllByApplicationIdCompletedSubscription = null;

    var initializedEventName = 'deviceSensorsService.onInitialized';

    var onConnected = function () {

        getAllByApplicationIdCompletedSubscription = stompService.subscribe(getAllByApplicationIdCompletedTopic, onGetAllByApplicationIdCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAllByApplicationId();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAllByApplicationId = function () {
        return $http.post(serviceBase + getAllByApplicationIdApiUri).then(function (results) {
            //alert('envio bem sucedido');
        });
    };       

    var onGetAllByApplicationIdCompleted = function (payload) {

        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);

        for (var i = 0; i < data.length; i++) {
            deviceContext.deviceSensors.push(data[i]);
        }

        _initializing = false;
        _initialized = true;

        deviceContext.deviceSensorsLoaded = true;
        clearOnConnected();

        getAllByApplicationIdCompletedSubscription.unsubscribe();

        $rootScope.$emit(initializedEventName);
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);       

    // stompService
    if (stompService.connected()) onConnected();

    // serviceFactory
        
    serviceFactory.initialized = initialized;
    serviceFactory.initializedEventName = initializedEventName;

    return serviceFactory;

}]);