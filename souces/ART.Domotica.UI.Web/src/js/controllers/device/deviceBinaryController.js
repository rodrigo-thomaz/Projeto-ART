'use strict';
app.controller('deviceBinaryController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster',
    function ($scope, $rootScope, $timeout, $log, toaster) {

        $scope.deviceBinary = null;

        $scope.init = function (deviceBinary) {
            $scope.deviceBinary = deviceBinary;           
        }

    }]);