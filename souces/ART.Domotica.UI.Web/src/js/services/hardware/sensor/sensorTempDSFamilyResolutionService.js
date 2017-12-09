'use strict';
app.factory('sensorTempDSFamilyResolutionService', ['$http', '$log', '$rootScope', '$localStorage', 'ngAuthSettings', 'stompService', 'sensorContext', 'sensorTempDSFamilyResolutionConstant',
    function ($http, $log, $rootScope, $localStorage, ngAuthSettings, stompService, sensorContext, sensorTempDSFamilyResolutionConstant) {

        var serviceFactory = {};

        // Local cache        

        if ($localStorage.sensorTempDSFamilyResolutionData) {
            var data = JSON.parse(Base64.decode($localStorage.sensorTempDSFamilyResolutionData));
            for (var i = 0; i < data.length; i++) {
                sensorContext.sensorTempDSFamilyResolution.push(data[i]);
            }
            $rootScope.$emit(sensorTempDSFamilyResolutionConstant.getAllCompletedEventName);
            return serviceFactory;
        }

        // Get from Server

        var _initializing = false;
        var _initialized = false;

        var serviceBase = ngAuthSettings.distributedServicesUri;        

        var getAllCompletedSubscription = null;

        var onConnected = function () {

            getAllCompletedSubscription = stompService.subscribe(sensorTempDSFamilyResolutionConstant.getAllCompletedTopic, onGetAllCompleted);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAll();
            }
        }

        var getAll = function () {
            return $http.post(serviceBase + sensorTempDSFamilyResolutionConstant.getAllApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var onGetAllCompleted = function (payload) {

            var dataUTF8 = decodeURIComponent(escape(payload.body));

            $localStorage.sensorTempDSFamilyResolutionData = Base64.encode(dataUTF8);
            $localStorage.$save();

            var data = JSON.parse(dataUTF8);

            for (var i = 0; i < data.length; i++) {
                sensorContext.sensorTempDSFamilyResolution.push(data[i]);
            }

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllCompletedSubscription.unsubscribe();

            $rootScope.$emit(sensorTempDSFamilyResolutionConstant.getAllCompletedEventName);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        // stompService

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        if (stompService.connected()) onConnected();

        return serviceFactory;

    }]);