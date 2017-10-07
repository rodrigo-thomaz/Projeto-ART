'use strict';
app.controller('signupController', ['$scope', '$location', '$timeout', 'authService', 'EventDispatcher', 'stompService', function ($scope, $location, $timeout, authService, EventDispatcher, stompService) {

    $scope.savedSuccessfully = false;
    $scope.message = "";

    $scope.registration = {
        userName: "",
        password: "",
        confirmPassword: ""
    };

    $scope.signUp = function () {

        authService.saveRegistration($scope.registration).then(function (response) {                        
            $scope.savedSuccessfully = true;
            $scope.message = "Aguarde, solicitação enviada para o servidor...";
        },
         function (response) {
             var errors = [];
             for (var key in response.data.modelState) {
                 for (var i = 0; i < response.data.modelState[key].length; i++) {
                     errors.push(response.data.modelState[key][i]);
                 }
             }
             $scope.message = "Failed to register user due to:" + errors.join(' ');
         });
    };

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/login');
        }, 2000);
    }

    // stompService

    var onConnected = function () {
        stompService.client.subscribe('/topic/' + stompService.session + '-Security.RegisterUserCompleted', onRegisterUserCompleted);
    }

    var onRegisterUserCompleted = function (payload) {
        $scope.savedSuccessfully = true;
        $scope.message = "User has been registered successfully, you will be redicted to login page in 2 seconds.";
        startTimer();
    }

    EventDispatcher.on('stompService_onConnected', onConnected);

    if (stompService.client.connected)
        onConnected();

    // stompService

}]);