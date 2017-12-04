'use strict';
app.factory('sensorTempDSFamilyResolutionService', ['$http', '$log', '$rootScope', 'ngAuthSettings', 'stompService', function ($http, $log, $rootScope, ngAuthSettings, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized = false;

    var serviceFactory = {};

    var onConnected = function () {
        stompService.subscribe('SensorTempDSFamily.GetAllResolutionsViewCompleted', onGetAllCompleted);
        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + 'api/sensorTempDSFamily/getAllResolutions').then(function (results) {
            //alert('envio bem sucedido');
        });
    };   

    var getResolutionById = function (sensorTempDSFamilyResolutionId) {
        for (var i = 0; i < serviceFactory.resolutions.length; i++) {
            if (serviceFactory.resolutions[i].id === sensorTempDSFamilyResolutionId) {
                return serviceFactory.resolutions[i];
            }
        }
    };    
    
    var onGetAllCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            serviceFactory.resolutions.push(data[i]);
        }
        _initializing = false;
        _initialized = true;
        $rootScope.$emit('SensorTempDSFamilyResolutionService_Initialized');
    }
    
    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected); 

    // stompService
    if (stompService.connected()) onConnected();

    // serviceFactory

    serviceFactory.resolutions = [];

    serviceFactory.initialized = initialized;
    serviceFactory.getResolutionById = getResolutionById;

    return serviceFactory;

}]);