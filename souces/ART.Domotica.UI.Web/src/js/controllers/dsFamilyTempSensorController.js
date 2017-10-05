'use strict';

app.controller('dsFamilyTempSensorController', ['$scope', '$timeout', '$log', 'temperatureScaleService', 'dsFamilyTempSensorService', function ($scope, $timeout, $log, temperatureScaleService, dsFamilyTempSensorService) {    

    var onInit = function () {

    }

    $scope.init = onInit;
    
    $scope.resolutions = dsFamilyTempSensorService.resolutions;
    $scope.scales = temperatureScaleService.scales;
    

}]);
