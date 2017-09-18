'use strict';
app.controller('testController', ['$scope', 'testService', function ($scope, testService) {

    $scope.lista = [];

    testService.get().then(function (results) {

        $scope.lista = results.data;

    }, function (error) {
        alert(error.data.message);
    });

}]);