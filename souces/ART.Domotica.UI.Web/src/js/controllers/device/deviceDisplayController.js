'use strict';
app.controller('deviceDisplayController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'deviceDisplayService', 'deviceDisplayConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, deviceDisplayService, deviceDisplayConstant) {

        $scope.deviceDisplay = null;

        $scope.enabledView = null;

        $scope.init = function (deviceDisplay) {

            $scope.deviceDisplay = deviceDisplay;

            $scope.enabledView = deviceDisplay.enabled;

            initializeEnabledViewWatch();

            clearOnSetEnabledCompleted = $rootScope.$on(deviceDisplayConstant.setEnabledCompletedEventName + $scope.deviceDisplay.deviceId, onSetEnabledCompleted);
        }

        var enabledViewWatch = null;

        var initializeEnabledViewWatch = function () {
            enabledViewWatch = $scope.$watch('enabledView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDisplayService.setEnabled(
                    $scope.deviceDisplay.deviceTypeId, 
                    $scope.deviceDisplay.deviceDatasheetId, 
                    $scope.deviceDisplay.deviceId, 
                    newValue);
            });
        };        

        var clearOnSetEnabledCompleted = null;

        $scope.$on('$destroy', function () {
            clearOnSetEnabledCompleted();

            enabledViewWatch();
        });

        var onSetEnabledCompleted = function (event, data) {
            $scope.enabledView = $scope.deviceDisplay.enabled;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Display Enabled alterado');
        };

    }]);