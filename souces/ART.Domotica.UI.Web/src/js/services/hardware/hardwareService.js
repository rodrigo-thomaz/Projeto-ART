'use strict';
app.factory('hardwareService', ['$http', 'ngAuthSettings', '$rootScope', 'stompService', 'hardwareContext', 'hardwareConstant', 'hardwareFinder',
    function ($http, ngAuthSettings, $rootScope, stompService, hardwareContext, hardwareConstant, hardwareFinder) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setLabelCompletedSubscription = null;

        var onConnected = function () {
            setLabelCompletedSubscription = stompService.subscribeAllViews(hardwareConstant.setLabelCompletedTopic, onSetLabelCompleted);
        }

        var setLabel = function (hardwareId, label) {
            var data = {
                hardwareId: hardwareId,
                label: label,
            }
            return $http.post(serviceBase + hardwareConstant.setLabelApiUri, data).then(function (results) {
                return results;
            });
        };

        var onSetLabelCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var hardware = hardwareFinder.getByKey(result.hardwareId);
            hardware.label = hardware.label;
            hardwareContext.$digest();
            $rootScope.$emit(hardwareConstant.setLabelCompletedEventName + result.hardwareId, result);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setLabelCompletedSubscription.unsubscribe();            
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.setLabel = setLabel;

        return serviceFactory;

    }]);