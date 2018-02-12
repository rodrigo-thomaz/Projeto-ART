'use strict';
app.factory('deviceDatasheetMapper', [
    '$rootScope',
    'deviceDatasheetContext',
    'deviceTypeFinder',
    'deviceDatasheetFinder',
    'deviceDatasheetConstant',
    'deviceTypeConstant',
    'deviceFinder',
    function (
        $rootScope,
        deviceDatasheetContext,
        deviceTypeFinder,
        deviceDatasheetFinder,
        deviceDatasheetConstant,
        deviceTypeConstant,
        deviceFinder) {

        var serviceFactory = {};        

        deviceDatasheetContext.$watchCollection('deviceType', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceType = newValues[i];
                //deviceType.deviceDatasheets = function () { return deviceDatasheetFinder.getByDeviceTypeKey(this.deviceTypeId); }
            }
        });

        deviceDatasheetContext.$watchCollection('deviceDatasheet', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var deviceDatasheet = newValues[i];
                deviceDatasheet.devices = function () { return deviceFinder.getByDeviceDatasheetKey(this.deviceDatasheetId); }
            }
        });       

        // *** Events Subscriptions        

        var onDeviceTypeGetAllCompleted = function (event, data) {
            deviceTypeGetAllCompletedSubscription();
            deviceDatasheetContext.deviceTypeLoaded = true;
        }

        var onDeviceDatasheetGetAllCompleted = function (event, data) {
            deviceDatasheetGetAllCompletedSubscription();
            deviceDatasheetContext.deviceDatasheetLoaded = true;
        }       

        var deviceTypeGetAllCompletedSubscription = $rootScope.$on(deviceTypeConstant.getAllCompletedEventName, onDeviceTypeGetAllCompleted);
        var deviceDatasheetGetAllCompletedSubscription = $rootScope.$on(deviceDatasheetConstant.getAllCompletedEventName, onDeviceDatasheetGetAllCompleted);

        $rootScope.$on('$destroy', function () {
            deviceTypeGetAllCompletedSubscription();
            deviceDatasheetGetAllCompletedSubscription();
        });

        // *** Events Subscriptions


        return serviceFactory;

    }]);