'use strict';
app.controller('contatoController', ['$scope', 'contatoService', function ($scope, contatoService) {

    $scope.lista = [];

    contatoService.get().then(function (results) {

        $scope.lista = results.data;

    }, function (error) {
        alert(error.data.message);
    }); 

}]);