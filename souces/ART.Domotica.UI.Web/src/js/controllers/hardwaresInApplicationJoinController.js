'use strict';
app.controller('hardwaresInApplicationJoinController', ['$scope', '$timeout', '$log', 'uiGridConstants', 'EventDispatcher', 'hardwaresInApplicationJoinService', function ($scope, $timeout, $log, uiGridConstants, EventDispatcher, hardwaresInApplicationJoinService) {    

    var onSearchPinClick = function () {    
        $scope.searchingPin = true;
            hardwaresInApplicationJoinService.searchPin($scope.pin).then(function successCallback(response) {
            }, function errorCallback(response) {
                $scope.searchingPin = false;
                $scope.$apply();
            });
        //$timeout(function () {
            
        //})        
    }

    var onSearchPinCompleted = function (payload) {
        $scope.searchingPin = false;
        $scope.hardware = payload;
        $scope.$apply();
    }

    var onInsertHardwareClick = function () {
        hardwaresInApplicationJoinService.insertHardware($scope.pin);
    }

    var onInsertHardwareCompleted = function (payload) {
        alert("hardware inserido!!!");
    }    

    EventDispatcher.on('hardwaresInApplicationService_onSearchPinReceived', onSearchPinCompleted);
    EventDispatcher.on('hardwaresInApplicationService_onInsertHardwareReceived', onInsertHardwareCompleted);    

    $scope.pin = "";
    $scope.hardware = null;
    $scope.searchPinClick = onSearchPinClick;
    $scope.searchingPin = false;
    $scope.insertHardwareClick = onInsertHardwareClick;    

}]);
