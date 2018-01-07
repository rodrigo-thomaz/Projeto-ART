'use strict';
app.controller('deviceDebugController', ['$scope', '$rootScope', '$timeout', '$log', 'toaster', 'deviceDebugService', 'deviceDebugConstant',
    function ($scope, $rootScope, $timeout, $log, toaster, deviceDebugService, deviceDebugConstant) {

        $scope.deviceDebug = null;

        $scope.remoteEnabledView = null;
        $scope.resetCmdEnabledView = null;
        $scope.serialEnabledView = null;
        $scope.showColorsView = null;
        $scope.showDebugLevelView = null;
        $scope.showProfilerView = null;
        $scope.showTimeView = null;

        $scope.init = function (deviceDebug) {

            $scope.deviceDebug = deviceDebug;

            $scope.remoteEnabledView = deviceDebug.remoteEnabled;
            $scope.resetCmdEnabledView = deviceDebug.resetCmdEnabled;
            $scope.serialEnabledView = deviceDebug.serialEnabled;
            $scope.showColorsView = deviceDebug.showColors;
            $scope.showDebugLevelView = deviceDebug.showDebugLevel;
            $scope.showProfilerView = deviceDebug.showProfiler;
            $scope.showTimeView = deviceDebug.showTime;

            initializeRemoteEnabledViewWatch();
            initializeResetCmdEnabledViewWatch();
            initializeSerialEnabledViewWatch();
            initializeShowColorsViewWatch();
            initializeShowDebugLevelViewWatch();
            initializeShowProfilerViewWatch();
            initializeShowTimeViewWatch();

            clearOnSetRemoteEnabledCompleted = $rootScope.$on(deviceDebugConstant.setRemoteEnabledCompletedEventName + $scope.deviceDebug.deviceDebugId, onSetRemoteEnabledCompleted);
            clearOnSetResetCmdEnabledCompleted = $rootScope.$on(deviceDebugConstant.setResetCmdEnabledCompletedEventName + $scope.deviceDebug.deviceDebugId, onSetResetCmdEnabledCompleted);
            clearOnSetSerialEnabledCompleted = $rootScope.$on(deviceDebugConstant.setSerialEnabledCompletedEventName + $scope.deviceDebug.deviceDebugId, onSetSerialEnabledCompleted);
            clearOnSetShowColorsCompleted = $rootScope.$on(deviceDebugConstant.setShowColorsCompletedEventName + $scope.deviceDebug.deviceDebugId, onSetShowColorsCompleted);
            clearOnSetShowDebugLevelCompleted = $rootScope.$on(deviceDebugConstant.setShowDebugLevelCompletedEventName + $scope.deviceDebug.deviceDebugId, onSetShowDebugLevelCompleted);
            clearOnSetShowProfilerCompleted = $rootScope.$on(deviceDebugConstant.setShowProfilerCompletedEventName + $scope.deviceDebug.deviceDebugId, onSetShowProfilerCompleted);
            clearOnSetShowTimeCompleted = $rootScope.$on(deviceDebugConstant.setShowTimeCompletedEventName + $scope.deviceDebug.deviceDebugId, onSetShowTimeCompleted);
        }

        var remoteEnabledViewWatch = null;
        var resetCmdEnabledViewWatch = null;
        var serialEnabledViewWatch = null;
        var showColorsViewWatch = null;
        var showDebugLevelViewWatch = null;
        var showProfilerViewWatch = null;
        var showTimeViewWatch = null;

        var initializeRemoteEnabledViewWatch = function () {
            remoteEnabledViewWatch = $scope.$watch('remoteEnabledView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setRemoteEnabled(
                    $scope.deviceDebug.deviceDebugId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    newValue);
            });
        };

        var initializeResetCmdEnabledViewWatch = function () {
            resetCmdEnabledViewWatch = $scope.$watch('resetCmdEnabledView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setResetCmdEnabled(
                    $scope.deviceDebug.deviceDebugId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    newValue);
            });
        };

        var initializeSerialEnabledViewWatch = function () {
            serialEnabledViewWatch = $scope.$watch('serialEnabledView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setSerialEnabled(
                    $scope.deviceDebug.deviceDebugId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    newValue);
            });
        };

        var initializeShowColorsViewWatch = function () {
            showColorsViewWatch = $scope.$watch('showColorsView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setShowColors(
                    $scope.deviceDebug.deviceDebugId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    newValue);
            });
        };

        var initializeShowDebugLevelViewWatch = function () {
            showDebugLevelViewWatch = $scope.$watch('showDebugLevelView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setShowDebugLevel(
                    $scope.deviceDebug.deviceDebugId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    newValue);
            });
        };

        var initializeShowProfilerViewWatch = function () {
            showProfilerViewWatch = $scope.$watch('showProfilerView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setShowProfiler(
                    $scope.deviceDebug.deviceDebugId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    newValue);
            });
        };

        var initializeShowTimeViewWatch = function () {
            showTimeViewWatch = $scope.$watch('showTimeView', function (newValue, oldValue) {
                if (newValue === oldValue) return;
                deviceDebugService.setShowTime(
                    $scope.deviceDebug.deviceDebugId, 
                    $scope.deviceDebug.deviceDatasheetId, 
                    newValue);
            });
        };

        var clearOnSetRemoteEnabledCompleted = null;
        var clearOnSetResetCmdEnabledCompleted = null;
        var clearOnSetSerialEnabledCompleted = null;
        var clearOnSetShowColorsCompleted = null;
        var clearOnSetShowDebugLevelCompleted = null;
        var clearOnSetShowProfilerCompleted = null;
        var clearOnSetShowTimeCompleted = null;

        $scope.$on('$destroy', function () {
            clearOnSetRemoteEnabledCompleted();
            clearOnSetResetCmdEnabledCompleted();
            clearOnSetSerialEnabledCompleted();
            clearOnSetShowColorsCompleted();
            clearOnSetShowDebugLevelCompleted();
            clearOnSetShowProfilerCompleted();
            clearOnSetShowTimeCompleted();

            remoteEnabledViewWatch();
            resetCmdEnabledViewWatch();
            serialEnabledViewWatch();
            showColorsViewWatch();
            showDebugLevelViewWatch();
            showProfilerViewWatch();
            showTimeViewWatch();
        });

        var onSetRemoteEnabledCompleted = function (event, data) {
            $scope.remoteEnabledView = $scope.deviceDebug.remoteEnabled;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug RemoteEnabled alterado');
        };

        var onSetResetCmdEnabledCompleted = function (event, data) {
            $scope.resetCmdEnabledView = $scope.deviceDebug.resetCmdEnabled;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug ResetCmdEnabled alterado');
        };

        var onSetSerialEnabledCompleted = function (event, data) {
            $scope.serialEnabledView = $scope.deviceDebug.serialEnabled;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug SerialEnabled alterado');
        };

        var onSetShowColorsCompleted = function (event, data) {
            $scope.showColorsView = $scope.deviceDebug.showColors;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug ShowColors alterado');
        };

        var onSetShowDebugLevelCompleted = function (event, data) {
            $scope.showDebugLevelView = $scope.deviceDebug.showDebugLevel;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug ShowDebugLevel alterado');
        };
        
        var onSetShowProfilerCompleted = function (event, data) {
            $scope.showProfilerView = $scope.deviceDebug.showProfiler;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug ShowProfiler alterado');
        };

        var onSetShowTimeCompleted = function (event, data) {
            $scope.showTimeView = $scope.deviceDebug.showTime;
            $scope.$apply();
            toaster.pop('success', 'Sucesso', 'Debug ShowTime alterado');
        };

    }]);