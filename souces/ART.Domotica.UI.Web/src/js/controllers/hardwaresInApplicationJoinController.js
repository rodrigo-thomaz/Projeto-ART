'use strict';
app.controller('hardwaresInApplicationJoinController', ['$scope', '$timeout', '$log', 'uiGridConstants', 'EventDispatcher', 'hardwaresInApplicationJoinService', function ($scope, $timeout, $log, uiGridConstants, EventDispatcher, hardwaresInApplicationJoinService) {    

    var onSearchClick = function () {
        hardwaresInApplicationJoinService.searchPin($scope.pin);
    }

    var onSearchPinCompleted = function (payload) {
        $scope.hardware = payload;
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
    $scope.hardware = {};
    $scope.searchClick = onSearchClick;
    $scope.insertHardwareClick = onInsertHardwareClick;    

}]);
