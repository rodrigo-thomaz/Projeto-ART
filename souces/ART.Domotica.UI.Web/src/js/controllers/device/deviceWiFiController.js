'use strict';
app.controller('deviceWiFiController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'deviceWiFiService', 'deviceWiFiConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, deviceWiFiService, deviceWiFiConstant) {

        $scope.deviceWiFi = null;

        $scope.init = function (deviceWiFi) {

            $scope.deviceWiFi = deviceWiFi;

            $scope.hostNameView = deviceWiFi.hostName;

            clearOnSetHostNameCompleted = $rootScope.$on(deviceWiFiConstant.setHostNameCompletedEventName + $scope.deviceWiFi.deviceWiFiId, onSetHostNameCompleted);
        }

        var clearOnSetHostNameCompleted = null;

        $scope.$on('$destroy', function () {
            clearOnSetHostNameCompleted();
        });

        var onSetHostNameCompleted = function (event, data) {
            $scope.hostNameView = $scope.deviceWiFi.hostName;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'HostName alterado');
        };

        $scope.changeHostName = function () {
            if (!$scope.deviceWiFi || !$scope.hostNameView) return;
            deviceWiFiService.setHostName($scope.deviceWiFi.deviceWiFiId, $scope.deviceWiFi.deviceDatasheetId, $scope.hostNameView);
        };

    }]);