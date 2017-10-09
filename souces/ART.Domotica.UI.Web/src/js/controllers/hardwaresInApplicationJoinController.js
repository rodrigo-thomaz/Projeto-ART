'use strict';
app.controller('hardwaresInApplicationJoinController', ['$scope', '$timeout', '$log', 'uiGridConstants', 'EventDispatcher', 'hardwaresInApplicationJoinService', function ($scope, $timeout, $log, uiGridConstants, EventDispatcher, hardwaresInApplicationJoinService) {    

    var onSearchClick = function () {
        alert();
    }

    $scope.onSearchClick = onSearchClick;

}]);
