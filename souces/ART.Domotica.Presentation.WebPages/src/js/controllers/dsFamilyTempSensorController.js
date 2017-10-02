'use strict';

app.controller('dsFamilyTempSensorController', ['$scope', '$timeout', 'dsFamilyTempSensorResolutionService', 'dsFamilyTempSensorService', '$log', function ($scope, $timeout, dsFamilyTempSensorResolutionService, dsFamilyTempSensorService, $log) {    

    $scope.resolutions = dsFamilyTempSensorResolutionService.get();

    //dsFamilyTempSensorService.setResolution('28FFFE6593164B6', 10).then(function (results) {

    //    alert('yeah!');

    //}, function (error) {
    //    if (error.status !== 401) {
    //        alert(error.data.message);
    //    }
    //});

}]);
