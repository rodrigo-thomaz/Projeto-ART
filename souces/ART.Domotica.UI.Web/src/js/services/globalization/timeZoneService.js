'use strict';
app.factory('timeZoneService', ['$http', 'ngAuthSettings', '$rootScope', '$localStorage', 'stompService', 'globalizationContext', 'timeZoneConstant',
    function ($http, ngAuthSettings, $rootScope, $localStorage, stompService, globalizationContext, timeZoneConstant) {

        var serviceFactory = {};

        // Local cache        

        if ($localStorage.timeZoneData) {
            var data = JSON.parse(Base64.decode($localStorage.timeZoneData));
            for (var i = 0; i < data.length; i++) {
                globalizationContext.timeZone.push(data[i]);
            }
            $rootScope.$emit(timeZoneConstant.getAllCompletedEventName);
            return serviceFactory;
        }

        // Get from Server

        var _initialized = false;
        var _initializing = false;

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var getAllCompletedSubscription = null;

        var onConnected = function () {

            getAllCompletedSubscription = stompService.subscribe(timeZoneConstant.getAllCompletedTopic, onGetAllCompleted);

            if (!_initializing && !_initialized) {
                _initializing = true;
                getAll();
            }
        }

        var getAll = function () {
            return $http.post(serviceBase + timeZoneConstant.getAllApiUri).then(function (results) {
                //alert('envio bem sucedido');
            });
        };

        var onGetAllCompleted = function (payload) {
            
            var dataUTF8 = decodeURIComponent(escape(payload.body));            

            $localStorage.timeZoneData = Base64.encode(dataUTF8);
            $localStorage.$save();

            var data = JSON.parse(dataUTF8);
            
            for (var i = 0; i < data.length; i++) {
                globalizationContext.timeZone.push(data[i]);
            }

            globalizationContext.$digest();

            _initializing = false;
            _initialized = true;

            clearOnConnected();

            getAllCompletedSubscription.unsubscribe();

            $rootScope.$emit(timeZoneConstant.getAllCompletedEventName);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
        });

        // stompService

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        if (stompService.connected()) onConnected();

        return serviceFactory;

    }]);