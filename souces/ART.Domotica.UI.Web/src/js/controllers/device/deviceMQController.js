'use strict';
app.controller('deviceMQController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 
    function ($scope, $rootScope, $timeout, $log, toaster) {

        $scope.deviceMQ = null;

        $scope.init = function (deviceMQ) {
            $scope.deviceMQ = deviceMQ;           
        }

    }]);