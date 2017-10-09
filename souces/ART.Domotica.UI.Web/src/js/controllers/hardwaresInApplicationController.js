'use strict';
app.controller('hardwaresInApplicationController', ['$scope', '$timeout', '$log', 'uiGridConstants', 'EventDispatcher', 'hardwaresInApplicationService', function ($scope, $timeout, $log, uiGridConstants, EventDispatcher, hardwaresInApplicationService) {    
        
    var onGetListCompleted = function (data) {
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
            { name: 'Id', field: 'id', width: 270 },
            { name: 'Pin', field: 'pin', width: 70 },
            { name: 'Em uso', field: 'inApplication', cellTemplate: '<div class="checkbox text-center"><label class="i-checks"><input disabled type="checkbox" ng-checked="{{grid.getCellValue(row, col)}}" value=""><i></i> </label></div>', width: 85 },
            { name: 'Data criação', field: 'createDateFormatted', width: 150 }
        ],
    };

    hardwareService.getList();

    EventDispatcher.on('hardwaresInApplicationService_onGetListCompleted', onGetListCompleted);

}]);
