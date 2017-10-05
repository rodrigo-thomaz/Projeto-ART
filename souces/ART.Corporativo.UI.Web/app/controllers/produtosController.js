'use strict';
app.controller('produtosController', ['$scope', 'produtosService', function ($scope, produtosService) {

    $scope.lista = [];

    produtosService.get().then(function (results) {

        $scope.lista = results.data;

    }, function (error) {
        alert(error.data.message);
    });

}]);