'use strict';
app.controller('deviceNTPController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'globalizationContext', 'deviceNTPService', 'deviceNTPConstant', 'timeZoneConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, globalizationContext, deviceNTPService, deviceNTPConstant, timeZoneConstant) {

        $scope.deviceNTP = null;

        $scope.init = function (deviceNTP) {

            $scope.deviceNTP = deviceNTP;

            $scope.updateIntervalInMilliSecondView = deviceNTP.updateIntervalInMilliSecond;

            // Time Zone
            if (globalizationContext.timeZoneLoaded)
                setSelectedTimeZone();
            else {
                var timeZoneLoadedWatch = globalizationContext.$watch('timeZoneLoaded', function (newValue) {
                    if (newValue) {
                        timeZoneLoadedWatch();
                        setSelectedTimeZone();
                    }
                })
            }                

            clearOnSetTimeZoneCompleted = $rootScope.$on(deviceNTPConstant.setTimeZoneCompletedEventName + $scope.deviceNTP.deviceId, onSetTimeZoneCompleted);
            clearOnSetUpdateIntervalInMilliSecondCompleted = $rootScope.$on(deviceNTPConstant.setUpdateIntervalInMilliSecondCompletedEventName + $scope.deviceNTP.deviceId, onSetUpdateIntervalInMilliSecondCompleted);
        }

        var clearOnSetTimeZoneCompleted = null;
        var clearOnSetUpdateIntervalInMilliSecondCompleted = null;

        $scope.$on('$destroy', function () {
            clearOnSetTimeZoneCompleted();
            clearOnSetUpdateIntervalInMilliSecondCompleted();
        });

        var setSelectedTimeZone = function () {
            $scope.timeZone.selectedTimeZone = $scope.deviceNTP.timeZone();
        };

        var onSetTimeZoneCompleted = function (event, data) {
            setSelectedTimeZone();
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Fuso horário alterado');
        };

        var onSetUpdateIntervalInMilliSecondCompleted = function (event, data) {
            $scope.updateIntervalInMilliSecondView = $scope.deviceNTP.updateIntervalInMilliSecond;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'UpdateIntervalInMilliSecond alterado');
        };

        $scope.timeZone = {
            availableTimeZones: globalizationContext.timeZone,
            selectedTimeZone: {},
        };

        $scope.changeTimeZone = function () {
            if (!$scope.deviceNTP) return;
            deviceNTPService.setTimeZone($scope.deviceNTP.deviceTypeId, $scope.deviceNTP.deviceDatasheetId, $scope.deviceNTP.deviceId, $scope.timeZone.selectedTimeZone.timeZoneId);
        };

        $scope.changeUpdateIntervalInMilliSecond = function () {
            if (!$scope.deviceNTP || !$scope.updateIntervalInMilliSecondView) return;
            deviceNTPService.setUpdateIntervalInMilliSecond($scope.deviceNTP.deviceTypeId, $scope.deviceNTP.deviceDatasheetId, $scope.deviceNTP.deviceId, $scope.updateIntervalInMilliSecondView);
        };

    }]);