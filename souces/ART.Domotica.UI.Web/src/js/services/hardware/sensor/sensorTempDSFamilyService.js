'use strict';
app.factory('sensorTempDSFamilyService', ['$http', '$log', '$rootScope', 'ngAuthSettings', 'stompService', 'sensorContext', 'sensorTempDSFamilyFinder', 'sensorTempDSFamilyConstant',
    function ($http, $log, $rootScope, ngAuthSettings, stompService, sensorContext, sensorTempDSFamilyFinder, sensorTempDSFamilyConstant) {

        var serviceFactory = {};

        var serviceBase = ngAuthSettings.distributedServicesUri;

        var setResolutionCompletedSubscription = null;

        var onConnected = function () {
            setResolutionCompletedSubscription = stompService.subscribeAllViews(sensorTempDSFamilyConstant.setResolutionCompletedTopic, onSetResolutionCompleted);
        }

        var setResolution = function (sensorTempDSFamilyId, sensorDatasheetId, sensorTypeId, sensorTempDSFamilyResolutionId) {
            var data = {
                sensorTempDSFamilyId: sensorTempDSFamilyId,
                sensorDatasheetId: sensorDatasheetId,
                sensorTypeId: sensorTypeId,
                sensorTempDSFamilyResolutionId: sensorTempDSFamilyResolutionId,
            }
            return $http.post(serviceBase + sensorTempDSFamilyConstant.setResolutionApiUri, data).then(function (results) {
                return results;
            });
        };        

        var onSetResolutionCompleted = function (payload) {
            var result = JSON.parse(payload.body);
            var sensorTempDSFamily = sensorTempDSFamilyFinder.getByKey(result.sensorTempDSFamilyId, result.sensorDatasheetId, result.sensorTypeId);
            sensorTempDSFamily.sensorTempDSFamilyResolutionId = result.sensorTempDSFamilyResolutionId;
            sensorContext.$digest();
            $rootScope.$emit(sensorTempDSFamilyConstant.setResolutionCompletedEventName + result.sensorTempDSFamilyId, result);
        }

        $rootScope.$on('$destroy', function () {
            clearOnConnected();
            setResolutionCompletedSubscription.unsubscribe();
        });

        var clearOnConnected = $rootScope.$on(stompService.connectedEventName, onConnected);

        // stompService
        if (stompService.connected()) onConnected();

        // serviceFactory

        serviceFactory.setResolution = setResolution;

        return serviceFactory;

    }]);