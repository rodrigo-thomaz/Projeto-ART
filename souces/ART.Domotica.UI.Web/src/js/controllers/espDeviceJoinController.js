'use strict';
app.controller('espDeviceJoinController', ['$scope', '$timeout', '$log', '$rootScope', 'espDeviceService', function ($scope, $timeout, $log, $rootScope, espDeviceService) {    

    var onGetByPinClick = function () {    
        $scope.searchingPin = true;
        espDeviceService.getByPin($scope.pin).then(function successCallback(response) {

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
        espDeviceService.insertInApplication($scope.pin);
    }

    var onInsertInApplicationCompleted = function (payload) {
        alert("ESP Device inserido!!!");
    } 

    $scope.$on('$destroy', function () {
        clearOnGetByPinCompleted();
        clearOnInsertInApplicationCompleted();
    });

    var clearOnGetByPinCompleted = $rootScope.$on('espDeviceService_onGetByPinCompleted', onGetByPinCompleted);        
    var clearOnInsertInApplicationCompleted = $rootScope.$on('espDeviceService_onInsertInApplicationCompleted', onInsertInApplicationCompleted);        
    
    $scope.pin = "";
    $scope.espDevice = null;
    $scope.getByPinClick = onGetByPinClick;
    $scope.searchingPin = false;
    $scope.insertInApplicationClick = onInsertInApplicationClick;            

}]);
