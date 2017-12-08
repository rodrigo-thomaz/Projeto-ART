'use strict';
app.controller('espDeviceJoinController', ['$scope', '$timeout', '$log', '$rootScope', 'toaster', 'deviceConstant', 'deviceService', function ($scope, $timeout, $log, $rootScope, toaster, deviceConstant, deviceService) {    

    var onGetByPinClick = function () {    
        $scope.searchingPin = true;
        deviceService.getByPin($scope.pin).then(function successCallback(response) {

            }, function errorCallback(response) {
                $scope.searchingPin = false;
                $scope.$apply();
            });
        //$timeout(function () {
            
        //})        
    }

    var onGetByPinCompleted = function (payload) {
        $scope.searchingPin = false;
        $scope.espDevice = payload;
        $scope.$apply();
    }

    var onInsertInApplicationClick = function () {
        deviceService.insertInApplication($scope.pin);
    }

    var onInsertInApplicationCompleted = function (payload) {
        toaster.pop('success', 'Sucesso', 'Device inserido');
    } 

    $scope.$on('$destroy', function () {
        clearOnGetByPinCompleted();
        clearOnInsertInApplicationCompleted();
    });

    var clearOnGetByPinCompleted = $rootScope.$on(deviceConstant.getByPinCompletedEventName, onGetByPinCompleted);        
    var clearOnInsertInApplicationCompleted = $rootScope.$on(deviceConstant.insertInApplicationCompletedEventName, onInsertInApplicationCompleted);        
    
    $scope.pin = "";
    $scope.espDevice = null;
    $scope.getByPinClick = onGetByPinClick;
    $scope.searchingPin = false;
    $scope.insertInApplicationClick = onInsertInApplicationClick;            

}]);
