'use strict';
app.factory('unitMeasurementScaleService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'siContext', function ($http, ngAuthSettings, $rootScope, stompService, siContext) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var getAllCompletedTopic = 'Locale.Country.GetAllViewCompleted';
    var getAllCompletedSubscription = null;

    var serviceFactory = {};    

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
        return $http.post(serviceBase + 'api/si/unitMeasurementScale/getAll').then(function (results) {
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

        $rootScope.$emit('unitMeasurementScaleService_Initialized');
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected);        

    // stompService
    if (stompService.connected())
        onConnected();

    // serviceFactory

    serviceFactory.initialized = initialized;

    return serviceFactory;

}]);