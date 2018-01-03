'use strict';
app.controller('deviceDebugController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'deviceDebugService', 'deviceDebugConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, deviceDebugService, deviceDebugConstant) {

        $scope.deviceDebug = null;

        $scope.activeView = null;

        $scope.init = function (deviceDebug) {

            $scope.deviceDebug = deviceDebug;

            $scope.activeView = deviceDebug.active;

            initializeActiveViewWatch();

            clearOnSetActiveCompleted = $rootScope.$on(deviceDebugConstant.setActiveCompletedEventName + $scope.deviceDebug.deviceDebugId, onSetActiveCompleted);
        }

        var activeViewWatch = null;

        var initializeActiveViewWatch = function () {
            activeViewWatch = $scope.$watch('activeView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setActive(
                    $scope.deviceDebug.deviceDebugId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    newValue);
            });
        };

        var clearOnSetActiveCompleted = null;

        $scope.$on('$destroy', function () {
            clearOnSetActiveCompleted();
            activeViewWatch();
        });

        var onSetActiveCompleted = function (event, data) {
            $scope.activeView = $scope.deviceDebug.active;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug active alterado');
        };

    }]);