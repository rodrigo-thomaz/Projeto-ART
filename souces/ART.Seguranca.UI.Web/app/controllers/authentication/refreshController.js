'use strict';
app.controller('refreshController', ['$scope', '$location', '$timeout', 'authService', function ($scope, $location, $timeout, authService) {

    $scope.authentication = authService.authentication;
    $scope.tokenRefreshed = false;
    $scope.tokenResponse = null;

    $scope.refreshToken = function () {

        authService.refreshToken().then(function (response) {

            $scope.tokenRefreshed = true;
            $scope.tokenResponse = response;

            var returnurl = $location.search().returnurl;
            if (returnurl) {
                // Workarround de 2s para esperar a autenticação replicar 
                // nas applicações do IIS
                $timeout(function () {
                    window.location = returnurl;
                }, 2000);
            }
            else {
                $location.path('/orders');
            }
        },
         function (err) {
             $location.path('/login');
         });
    };

}]);