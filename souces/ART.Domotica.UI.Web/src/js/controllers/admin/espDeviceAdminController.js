'use strict';
app.controller('espDeviceAdminController', ['$scope', '$timeout', '$log', 'uiGridConstants', 'EventDispatcher', 'espDeviceAdminService', function ($scope, $timeout, $log, uiGridConstants, EventDispatcher, espDeviceAdminService) {    

    var onGetAllCompleted = function (data) {
        for (var i = 0; i < data.length; i++) {
            data[i].createDateFormatted = new Date(data[i].createDate * 1000).toLocaleString();
        }
        $scope.gridOptions.data = data;
        $scope.$apply();
    }

    $scope.gridOptions = {
        enableFiltering: true,
        enableSorting: true,
        showFooter: true,
        rowHeight: 36,
        data: [],
        columnDefs: [
            { name: 'HardwareId', field: 'hardwareId', width: 270 },
            { name: 'MacAddress', field: 'macAddress', width: 150 },
            { name: 'ChipId', field: 'chipId', width: 100 },
            { name: 'FlashChipId', field: 'flashChipId', width: 120 },
            { name: 'Pin', field: 'pin', width: 70 },
            { name: 'Em uso', field: 'inApplication', cellTemplate: '<div class="checkbox text-center"><label class="i-checks"><input disabled type="checkbox" ng-checked="{{grid.getCellValue(row, col)}}" value=""><i></i> </label></div>', width: 85 },
            { name: 'Data criação', field: 'createDateFormatted', width: 150 }
        ],
    };    

    espDeviceAdminService.getAll();

    EventDispatcher.on('espDeviceAdminService_onGetAllCompleted', onGetAllCompleted);

}]);
