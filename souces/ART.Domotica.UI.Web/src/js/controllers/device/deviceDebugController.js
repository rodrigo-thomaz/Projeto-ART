'use strict';
app.controller('deviceDebugController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'deviceDebugService', 'deviceDebugConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, deviceDebugService, deviceDebugConstant) {

        $scope.deviceDebug = null;

        $scope.init = function (deviceDebug) {

            $scope.deviceDebug = deviceDebug;

            $scope.activeView = deviceDebug.active;

            clearOnSetActiveCompleted = $rootScope.$on(deviceDebugConstant.setActiveCompletedEventName + $scope.deviceDebug.deviceDebugId, onSetActiveCompleted);
        }

        var clearOnSetActiveCompleted = null;

        $scope.$on('$destroy', function () {
            clearOnSetActiveCompleted();
        });

        var onSetActiveCompleted = function (event, data) {
            $scope.activeView = $scope.deviceDebug.active;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug active alterado');
        };

        $scope.changeActive = function () {
            if (!$scope.deviceDebug || !$scope.activeView) return;
            deviceDebugService.setActive($scope.deviceDebug.deviceDebugId, $scope.deviceDebug.deviceDatasheetId, $scope.activeView);
        };

    }]);