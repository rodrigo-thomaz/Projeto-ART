'use strict';
app.controller('dashboardController', ['$scope', 'dashboardService', function ($scope, dashboardService) {

    $scope.orders = [];

    //dashboardService.getDashboards().then(function (results) {

    //    $scope.dashboards = results.data;

    //}, function (error) {
    //    if (error.status !== 401) {
    //        alert(error.data.message);
    //    }        
    //});

}]);