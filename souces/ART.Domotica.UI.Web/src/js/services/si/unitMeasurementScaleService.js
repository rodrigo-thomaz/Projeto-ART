'use strict';
app.factory('unitMeasurementScaleService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'siContext', function ($http, ngAuthSettings, $rootScope, stompService, siContext) {

    var serviceFactory = {};    

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var getAllApiUri = 'api/si/unitMeasurementScale/getAll';
    var getAllCompletedTopic = 'Locale.Country.GetAllViewCompleted';
    var getAllCompletedSubscription = null;

    var initializedEventName = 'unitMeasurementScaleService.onInitialized';

    var onConnected = function () {

        getAllCompletedSubscription = stompService.subscribe('SI.UnitMeasurementScale.GetAllViewCompleted', onGetAllCompleted);

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
            siContext.unitMeasurementScales.push(data[i]);
        }
        
        _initializing = false;
        _initialized = true;

        siContext.unitMeasurementScaleLoaded = true;
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