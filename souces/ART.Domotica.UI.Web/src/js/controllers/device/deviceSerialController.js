'use strict';
app.controller('deviceSerialController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'globalizationContext', 'deviceSerialService', 'deviceSerialConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, globalizationContext, deviceSerialService, deviceSerialConstant) {

        $scope.deviceSerial = null;

        $scope.init = function (deviceSerial) {

            $scope.deviceSerial = deviceSerial;

            $scope.enabledView = deviceSerial.enabled;
            $scope.pinRXView = deviceSerial.pinRX;
            $scope.pinTXView = deviceSerial.pinTX;

            clearOnSetEnabledCompleted = $rootScope.$on(deviceSerialConstant.setEnabledCompletedEventName + $scope.deviceSerial.deviceSerialId, onSetEnabledCompleted);
            clearOnSetPinCompleted = $rootScope.$on(deviceSerialConstant.setPinCompletedEventName + $scope.deviceSerial.deviceSerialId, onSetPinCompleted);
        }

        var clearOnSetEnabledCompleted = null;
        var clearOnSetPinCompleted = null;

        $scope.$on('$destroy', function () {
            clearOnSetEnabledCompleted();
            clearOnSetPinCompleted();
        });

        var onSetEnabledCompleted = function (event, data) {
            $scope.enabledView = $scope.deviceSerial.enabled;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Enabled alterado');
        };

        var onSetPinCompleted = function (event, data) {
            if(data.direction === 'Receive'){
                $scope.pinRXView = $scope.deviceSerial.pinRX;
                $scope.$apply();
                toaster.pop('success', 'Sucesso', 'Pino RX alterado');
            }
            else if(data.direction === 'Transmit'){
                $scope.pinTXView = $scope.deviceSerial.pinTX;
                $scope.$apply();
                toaster.pop('success', 'Sucesso', 'Pino TX alterado');
            }                        
        };

        $scope.changeEnabled = function () {
            if (!$scope.deviceSerial) return;
            deviceSerialService.setEnabled($scope.deviceSerial.deviceSerialId, $scope.deviceSerial.deviceId, $scope.deviceSerial.deviceDatasheetId, $scope.enabledView);
        };

        $scope.changePin = function (value, direction) {
            if (!$scope.deviceSerial) return;
            deviceSerialService.setPin($scope.deviceSerial.deviceSerialId, $scope.deviceSerial.deviceId, $scope.deviceSerial.deviceDatasheetId, value, direction);
        };

    }]);