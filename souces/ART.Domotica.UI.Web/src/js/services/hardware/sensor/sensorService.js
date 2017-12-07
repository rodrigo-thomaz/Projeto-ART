'use strict';
app.factory('sensorService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'sensorContext', 'sensorConstant', function ($http, ngAuthSettings, $rootScope, stompService, sensorContext, sensorConstant) {

    var serviceBase = ngAuthSettings.distributedServicesUri;

    var serviceFactory = {};    

    var _initializing = false;
    var _initialized  = false;

    var getAllByApplicationIdCompletedSubscription = null;
    var setLabelCompletedSubscription = null;

    var onConnected = function () {

        getAllByApplicationIdCompletedSubscription = stompService.subscribe(sensorConstant.getAllByApplicationIdCompletedTopic, onGetAllByApplicationIdCompleted);
        setLabelCompletedSubscription = stompService.subscribeAllViews(sensorConstant.setLabelCompletedTopic, onSetLabelCompleted);

        if (!_initializing && !_initialized) {
            _initializing = true;
            getAllByApplicationId();
        }
    }   

    var initialized = function () {
        return _initialized;
    };

    var getAllByApplicationId = function () {
        return $http.post(serviceBase + sensorConstant.getAllByApplicationIdApiUri).then(function (results) {
            //alert('envio bem sucedido');
        });
    };     

    var setLabel = function (sensorTempDSFamilyId, label) {
        var data = {
            sensorTempDSFamilyId: sensorTempDSFamilyId,
            label: label,
        }
        return $http.post(serviceBase + sensorConstant.setLabelApiUri, data).then(function (results) {
            return results;
        });
    };  

    var onGetAllByApplicationIdCompleted = function (payload) {

        var dataUTF8 = decodeURIComponent(escape(payload.body));
        var data = JSON.parse(dataUTF8);

        for (var i = 0; i < data.length; i++) {
            sensorContext.sensor.push(data[i]);
        }

        _initializing = false;
        _initialized = true;

        sensorContext.sensorsLoaded = true;
        clearOnConnected();

        getAllByApplicationIdCompletedSubscription.unsubscribe();

        $rootScope.$emit(sensorConstant.getAllByApplicationIdCompletedEventName);
    }    

    var onSetLabelCompleted = function (payload) {
        var result = JSON.parse(payload.body);
        var sensor = getByKey(result.deviceId, result.sensorTempDSFamilyId);
        sensor.label = result.label;
        $rootScope.$emit(sensorConstant.setLabelCompletedEventName + result.sensorTempDSFamilyId, result);
    }   

    $rootScope.$on('$destroy', function () {
        clearOnConnected();
    });

    var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);       

    // stompService
    if (stompService.connected()) onConnected();

    // serviceFactory
        
    serviceFactory.initialized = initialized;        
    serviceFactory.setLabel = setLabel;   

    return serviceFactory;

}]);