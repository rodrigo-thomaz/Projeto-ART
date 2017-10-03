'use strict';

app.controller('dsFamilyTempSensorController', ['$scope', '$timeout', '$log', 'temperatureScaleService', 'dsFamilyTempSensorService', function ($scope, $timeout, $log, temperatureScaleService, dsFamilyTempSensorService) {    

    $scope.resolutions = dsFamilyTempSensorService.resolutions;
    $scope.scales = temperatureScaleService.scales;

    //dsFamilyTempSensorService.setResolution('28FFFE6593164B6', 10).then(function (results) {

    //    alert('yeah!');

    //}, function (error) {
    //    if (error.status !== 401) {
    //        alert(error.data.message);
    //    }
    //});

}]);
