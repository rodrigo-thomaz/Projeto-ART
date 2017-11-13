'use strict';

app.controller('dsFamilyTempSensorListController', ['$scope', '$timeout', '$log', 'temperatureScaleService', 'dsFamilyTempSensorService', function ($scope, $timeout, $log, temperatureScaleService, dsFamilyTempSensorService) {           
    $scope.sensors = dsFamilyTempSensorService.sensors;
}]);
