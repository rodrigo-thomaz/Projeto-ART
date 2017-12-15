'use strict';
app.controller('sensorInDeviceController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'sensorInDeviceService', 'sensorInDeviceConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, sensorInDeviceService, sensorInDeviceConstant) {

        $scope.sensorInDevice = [];

        $scope.init = function (sensorInDevice) {

            $scope.sensorInDevice = sensorInDevice;

            clearOnSetOrdinationCompleted = $rootScope.$on(sensorInDeviceConstant.setOrdinationCompletedEventName + $scope.sensorInDevice[0].deviceSensorsId, onSetOrdinationCompleted);
        }

        var clearOnSetOrdinationCompleted = null;        

        $scope.$on('$destroy', function () {
            clearOnSetOrdinationCompleted();
        });

        var onSetOrdinationCompleted = function (event, data) {
            //setSelectedTimeZone();
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Ordem dos sensores alterada');
        };

        $scope.dragControlListeners = {
            accept: function (sourceItemHandleScope, destSortableScope) { //override to determine drag is allowed or not. default is true.
                return true;
            },
            itemMoved: function (event) {
                //Do what you want
            },
            orderChanged: function (event) {
                var sensorInDevice = event.data;
                sensorInDeviceService.setOrdination(sensorInDevice.deviceSensorsId, sensorInDevice.sensorId, sensorInDevice.sensorDatasheetId, sensorInDevice.sensorTypeId, sensorInDevice.ordination);
            },
            //containment: '#board', //optional param.
            clone: false,//optional param for clone feature.
            allowDuplicates: false, //optional param allows duplicates to be dropped.
        };

        $scope.$watchCollection('sensorInDevice', function (newValues, oldValues) {

        });

    }]);