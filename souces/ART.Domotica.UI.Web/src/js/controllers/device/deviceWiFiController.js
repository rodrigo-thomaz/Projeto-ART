'use strict';
app.controller('deviceWiFiController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'deviceWiFiService', 'deviceWiFiConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, deviceWiFiService, deviceWiFiConstant) {

        $scope.deviceWiFi = null;

        $scope.init = function (deviceWiFi) {

            $scope.deviceWiFi = deviceWiFi;

            $scope.hostNameView = deviceWiFi.hostName;
            $scope.publishIntervalInMilliSecondsView = deviceWiFi.publishIntervalInMilliSeconds;

            clearOnSetHostNameCompleted = $rootScope.$on(deviceWiFiConstant.setHostNameCompletedEventName + $scope.deviceWiFi.deviceWiFiId, onSetHostNameCompleted);
            clearOnSetPublishIntervalInMilliSecondsCompleted = $rootScope.$on(deviceWiFiConstant.setPublishIntervalInMilliSecondsCompletedEventName + $scope.deviceWiFi.deviceWiFiId, onSetPublishIntervalInMilliSecondsCompleted);        
        }

        var clearOnSetHostNameCompleted = null;
        var clearOnSetPublishIntervalInMilliSecondsCompleted = null;

        $scope.$on('$destroy', function () {
            clearOnSetHostNameCompleted();
            clearOnSetPublishIntervalInMilliSecondsCompleted();
        });

        var onSetHostNameCompleted = function (event, data) {
            $scope.hostNameView = $scope.deviceWiFi.hostName;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'HostName alterado');
        };

        var onSetPublishIntervalInMilliSecondsCompleted = function (event, data) {
            $scope.publishIntervalInMilliSecondsView = data.publishIntervalInMilliSeconds;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'WiFi PublishIntervalInMilliSeconds alterado');
        };  

        $scope.changeHostName = function () {
            if (!$scope.deviceWiFi || !$scope.hostNameView) return;
            deviceWiFiService.setHostName($scope.deviceWiFi.deviceWiFiId, $scope.deviceWiFi.deviceDatasheetId, $scope.hostNameView);
        };

        $scope.changePublishIntervalInMilliSeconds = function () {
            if (!$scope.deviceWiFi || !$scope.publishIntervalInMilliSecondsView) return;
            deviceWiFiService.setPublishIntervalInMilliSeconds($scope.deviceWiFi.deviceWiFiId, $scope.deviceWiFi.deviceDatasheetId, $scope.publishIntervalInMilliSecondsView);
        };

    }]);