'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', function ($scope, $location, authService) {
    
    $scope.logIn = function () {
        authService.logIn();
    }

    $scope.signUp = function () {
        authService.signUp();
    }

    $scope.logOut = function () {
        authService.logOut();        
        $location.path('/home');
    }

    $scope.authentication = authService.authentication;    

}]);