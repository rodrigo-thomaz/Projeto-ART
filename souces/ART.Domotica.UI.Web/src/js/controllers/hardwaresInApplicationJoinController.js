'use strict';
app.controller('hardwaresInApplicationJoinController', ['$scope', '$timeout', '$log', 'uiGridConstants', 'EventDispatcher', 'hardwaresInApplicationJoinService', function ($scope, $timeout, $log, uiGridConstants, EventDispatcher, hardwaresInApplicationJoinService) {    

    var onSearchClick = function () {
        hardwaresInApplicationJoinService.searchPin($scope.pin);
    }

    var onSearchPinCompleted = function (payload) {

    }

    EventDispatcher.on('hardwaresInApplicationService_onSearchPinReceived', onSearchPinCompleted);

    $scope.pin = "";
    $scope.onSearchClick = onSearchClick;

}]);
