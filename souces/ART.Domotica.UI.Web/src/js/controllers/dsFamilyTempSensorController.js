'use strict';

app.controller('dsFamilyTempSensorController', ['$scope', '$timeout', '$log', 'temperatureScaleService', 'dsFamilyTempSensorService', function ($scope, $timeout, $log, temperatureScaleService, dsFamilyTempSensorService) {    

    $scope.gridOptions = {
        enableFiltering: true,
        enableSorting: true,
        showFooter: true,
        rowHeight: 36,
        data: [],
        columnDefs: [
            { name: 'Id', field: 'hardwareInApplicationId', width: 270 },
            { name: 'HardwareId', field: 'hardwareId', width: 270 },
            { name: 'ChipId', field: 'chipId', width: 270 },
            { name: 'FlashChipId', field: 'flashChipId', width: 270 },
            { name: 'MacAddress', field: 'macAddress', width: 270 },
            { name: 'Data criação', field: 'createDate', width: 150 },
            { name: 'Ações', cellTemplate: '<div class="text-center"><a ng-click="grid.appScope.deleteFromApplicationClick(row.entity)" class="btn btn-danger" href="" aria-label="Delete"><i class="fa fa-trash-o" aria-hidden="true"></i></a></div>', width: 85 },
        ],
    };

    $scope.gridOptions.data = dsFamilyTempSensorService.sensors;
    

}]);
