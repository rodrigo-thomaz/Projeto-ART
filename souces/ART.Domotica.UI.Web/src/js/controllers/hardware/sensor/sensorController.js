'use strict';
app.controller('sensorController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'sensorService',
    function ($scope, $rootScope, $timeout, $log, toaster, sensorService) {

        $scope.sensor = null;

        $scope.init = function (sensor) {

            $scope.sensor = sensor; 
     
    }

    $scope.$on('$destroy', function () {
        
    });

}]);