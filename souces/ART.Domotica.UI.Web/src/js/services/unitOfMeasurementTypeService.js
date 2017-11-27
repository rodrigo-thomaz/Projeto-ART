'use strict';
app.factory('unitOfMeasurementTypeService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', function ($http, ngAuthSettings, $rootScope, stompService) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var _initializing = false;
    var _initialized  = false;

    var serviceFactory = {};    

    var onConnected = function () {

        stompService.subscribe('UnitOfMeasurementType.GetAllViewCompleted', onGetAllCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAll();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAll = function () {
        return $http.post(serviceBase + 'api/unitOfMeasurementType/getAll').then(function (results) {
            //alert('envio bem sucedido');
        });
    };     

    var getUnitOfMeasurementTypeById = function (unitOfMeasurementTypeId) {
        for (var i = 0; i < serviceFactory.unitOfMeasurementTypes.length; i++) {
            if (serviceFactory.unitOfMeasurementTypes[i].id == unitOfMeasurementTypeId) {
                return serviceFactory.unitOfMeasurementTypes[i];
            }
        }
    };

    var onGetAllCompleted = function (payload) {
        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);
        for (var i = 0; i < data.length; i++) {
            serviceFactory.unitOfMeasurementTypes.push(data[i]);
        }
        _initializing = false;
        _initialized = true;
        $rootScope.$emit('unitOfMeasurementTypeService_Initialized');
    }

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on('stompService_onConnected', onConnected);        

    // stompService
    if (stompService.connected())
        onConnected();

    // serviceFactory
        
    serviceFactory.unitOfMeasurementTypes = [];  

    serviceFactory.initialized = initialized;
    serviceFactory.getUnitOfMeasurementTypeById = getUnitOfMeasurementTypeById;    

    return serviceFactory;

}]);