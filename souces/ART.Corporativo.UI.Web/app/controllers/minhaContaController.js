'use strict';
app.controller('minhaContaController', ['$scope', 'minhaContaService', function ($scope, minhaContaService) {

    $scope.lista = [];

    minhaContaService.get().then(function (results) {

        $scope.lista = results.data;

    }, function (error) {
        alert(error.data.message);
    });

}]);