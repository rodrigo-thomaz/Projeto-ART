'use strict';
app.factory('timeZoneService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'deviceContext', function ($http, ngAuthSettings, $rootScope, stompService, deviceContext) {

    var serviceFactory = {};    

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var getAllApiUri = 'api/timeZone/getAll';
    var getAllCompletedTopic = 'TimeZone.GetAllViewCompleted';
    var getAllCompletedSubscription = null;

    var initializedEventName = 'timeZoneService.onInitialized';

    var onConnected = function () {

        getAllCompletedSubscription = stompService.subscribe(getAllCompletedTopic, onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + getAllApiUri).then(function (results) {
            //alert('envio bem sucedido');
        });
    }; 

    var onGetAllCompleted = function (payload) {

        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);

        for (var i = 0; i < data.length; i++) {
            deviceContext.timeZone.push(data[i]);
        }

        _initializing = false;
        _initialized = true;

        deviceContext.timeZoneLoaded = true;
        clearOnConnected();

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