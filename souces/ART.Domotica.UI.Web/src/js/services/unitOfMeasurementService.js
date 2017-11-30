'use strict';
app.factory('unitOfMeasurementService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'contextScope', function ($http, ngAuthSettings, $rootScope, stompService, contextScope) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var serviceFactory = {};    

    var onConnected = function () {

        stompService.subscribe('UnitOfMeasurement.GetAllViewCompleted', onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + 'api/unitOfMeasurement/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };     

    var getByKey = function (unitOfMeasurementId, unitOfMeasurementTypeId) {
        for (var i = 0; i < contextScope.unitOfMeasurements.length; i++) {
            if (contextScope.unitOfMeasurements[i].id === unitOfMeasurementId && contextScope.unitOfMeasurements[i].unitOfMeasurementTypeId === unitOfMeasurementTypeId) {
                return contextScope.unitOfMeasurements[i];
            }
        }
    };

    var onGetAllCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            contextScope.unitOfMeasurements.push(data[i]);
        }
        contextScope.unitOfMeasurementLoaded = true;
        _initializing = false;
        _initialized = true;
        $rootScope.$emit('UnitOfMeasurementService_Initialized');
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
    serviceFactory.getByKey = getByKey;    

    return serviceFactory;

}]);