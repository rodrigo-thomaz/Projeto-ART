'use strict';

app.controller('dsFamilyTempSensorController', ['$scope', '$timeout', '$log', 'temperatureScaleService', 'dsFamilyTempSensorService', function ($scope, $timeout, $log, temperatureScaleService, dsFamilyTempSensorService) {    

    $scope.gridOptions = {
        enableFiltering: true,
        enableSorting: true,
        showFooter: true,
        rowHeight: 36,
        data: [],
        columnDefs: [
            { name: 'hardwareInApplicationId', field: 'hardwareInApplicationId', width: 270 },
            { name: 'dsFamilyTempSensorId', field: 'dsFamilyTempSensorId', width: 270 },
            { name: 'dsFamilyTempSensorResolutionId', field: 'dsFamilyTempSensorResolutionId', width: 270 },
            { name: 'deviceAddress', field: 'deviceAddress', width: 270 },
            { name: 'family', field: 'family', width: 270 },
            { name: 'highAlarm', field: 'highAlarm', width: 270 },
            { name: 'lowAlarm', field: 'lowAlarm', width: 270 },
            { name: 'temperatureScaleId', field: 'temperatureScaleId', width: 150 },
            { name: 'Ações', cellTemplate: '<div class="text-center"><a ng-click="grid.appScope.deleteFromApplicationClick(row.entity)" class="btn btn-danger" href="" aria-label="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></a></div>', width: 85 },
        ],
    };

    $scope.gridOptions.data = dsFamilyTempSensorService.sensors;
    

}]);
