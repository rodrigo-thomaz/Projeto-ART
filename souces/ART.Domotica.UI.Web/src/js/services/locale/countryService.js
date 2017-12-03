'use strict';
app.factory('countryService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'localeContext', function ($http, ngAuthSettings, $rootScope, stompService, localeContext) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var getAllCompletedTopic = 'Locale.Country.GetAllViewCompleted';
    var getAllCompletedSubscription = null;

    var serviceFactory = {};    

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
        return $http.post(serviceBase + 'api/locale/country/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };       

    var onGetAllCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            localeContext.countries.push(data[i]);
        }

        _initializing = false;
        _initialized = true;

        localeContext.countryLoaded = true;        
        clearOnConnected();

        getAllCompletedSubscription.unsubscribe();

        $rootScope.$emit('countryService_Initialized');
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);       

    // stompService
    if (stompService.connected())
        onConnected();

    // serviceFactory

    serviceFactory.initialized = initialized;

    return serviceFactory;

}]);