'use strict';
app.controller('espDeviceJoinController', ['$scope', '$timeout', '$log', 'uiGridConstants', 'EventDispatcher', 'espDeviceJoinService', function ($scope, $timeout, $log, uiGridConstants, EventDispatcher, espDeviceJoinService) {    

    var onGetByPinClick = function () {    
        $scope.searchingPin = true;
        espDeviceJoinService.getByPin($scope.pin).then(function successCallback(response) {
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
        espDeviceJoinService.insertInApplication($scope.pin);
    }

    var onInsertInApplicationCompleted = function (payload) {
        alert("ESP Device inserido!!!");
    }    

    EventDispatcher.on('espDeviceService_onGetByPinCompleted', onGetByPinCompleted);
    EventDispatcher.on('espDeviceService_onInsertInApplicationCompleted', onInsertInApplicationCompleted);    

    $scope.pin = "";
    $scope.espDevice = null;
    $scope.getByPinClick = onGetByPinClick;
    $scope.searchingPin = false;
    $scope.insertInApplicationClick = onInsertInApplicationClick;    

}]);
