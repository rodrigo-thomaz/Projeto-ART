'use strict';
app.controller('homeController', ['$scope', 'homeService', function ($scope, homeService) {

    $scope.lista = [];

    homeService.get().then(function (results) {

        $scope.lista = results.data;

    }, function (error) {
        alert(error.data.message);
    });

}]);