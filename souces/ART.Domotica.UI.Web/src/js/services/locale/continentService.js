'use strict';
app.factory('continentService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'localeContext', function ($http, ngAuthSettings, $rootScope, stompService, localeContext) {

    var serviceFactory = {};    

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var getAllCompletedTopic = 'Locale.Continent.GetAllViewCompleted';
    var getAllCompletedSubscription = null;

    var initializedEventName = 'continentService.onInitialized';
    
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
        return $http.post(serviceBase + 'api/locale/continent/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };       

    var onGetAllCompleted = function (payload) {

        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);

        for (var i = 0; i < data.length; i++) {
            localeContext.continents.push(data[i]);
        }
        
        _initializing = false;
        _initialized = true;

        localeContext.continentLoaded = true;
        clearOnConnected();

        getAllCompletedSubscription.unsubscribe();

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