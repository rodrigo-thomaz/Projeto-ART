'use strict';
app.controller('empresaController', ['$scope', 'empresaService', function ($scope, empresaService) {

    $scope.lista = [];

    empresaService.get().then(function (results) {

        $scope.lista = results.data;

    }, function (error) {
        alert(error.data.message);
    });  

}]);