'use strict';
app.controller('deviceMQController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'globalizationContext',
    function ($scope, $rootScope, $timeout, $log, toaster, globalizationContext) {

        $scope.deviceMQ = null;

        $scope.init = function (deviceMQ) {
            $scope.deviceMQ = deviceMQ;           
        }

    }]);