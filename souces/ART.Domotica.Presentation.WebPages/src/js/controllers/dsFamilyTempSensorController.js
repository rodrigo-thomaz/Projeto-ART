'use strict';

app.controller('dsFamilyTempSensorController', ['$scope', '$timeout', 'dsFamilyTempSensorService', '$log', function ($scope, $timeout, dsFamilyTempSensorService, $log) {    

    $scope.resolutions = dsFamilyTempSensorService.resolutions;

    //dsFamilyTempSensorService.setResolution('28FFFE6593164B6', 10).then(function (results) {

    //    alert('yeah!');

    //}, function (error) {
    //    if (error.status !== 401) {
    //        alert(error.data.message);
    //    }
    //});

}]);
