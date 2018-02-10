'use strict';
app.controller('deviceSerialController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'globalizationContext', 'deviceSerialService', 'deviceSerialConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, globalizationContext, deviceSerialService, deviceSerialConstant) {

        $scope.deviceSerial = null;

        $scope.init = function (deviceSerial) {

            $scope.deviceSerial = deviceSerial;

            $scope.enabledView = deviceSerial.enabled;

            clearOnSetEnabledCompleted = $rootScope.$on(deviceSerialConstant.setEnabledCompletedEventName + $scope.deviceSerial.deviceSerialId, onSetEnabledCompleted);
        }

        var clearOnSetEnabledCompleted = null;

        $scope.$on('$destroy', function () {
            clearOnSetEnabledCompleted();
        });

        var onSetEnabledCompleted = function (event, data) {
            $scope.enabledView = $scope.deviceSerial.enabled;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Enabled alterado');
        };

        $scope.changeEnabled = function () {
            if (!$scope.deviceSerial) return;
            deviceSerialService.setEnabled($scope.deviceSerial.deviceSerialId, $scope.deviceSerial.deviceId, $scope.deviceSerial.deviceDatasheetId, $scope.enabledView);
        };

    }]);