'use strict';
app.controller('sensorInDeviceController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'sensorInDeviceService',
    function ($scope, $rootScope, $timeout, $log, toaster, sensorInDeviceService) {

        $scope.sensorInDevice = [];

        $scope.init = function (sensorInDevice) {

            $scope.sensorInDevice = sensorInDevice;

        }

        $scope.$on('$destroy', function () {

        });

    }]);