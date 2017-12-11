'use strict';
app.controller('sensorInDeviceController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'sensorInDeviceService',
    function ($scope, $rootScope, $timeout, $log, toaster, sensorInDeviceService) {

        $scope.sensorsInDevice = [];

    $scope.init = function (sensorsInDevice) {

        $scope.sensorsInDevice = sensorsInDevice; 
     
    }

    $scope.$on('$destroy', function () {
        
    });

}]);