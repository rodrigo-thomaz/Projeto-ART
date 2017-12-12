'use strict';
app.factory('sensorDatasheetMapper', [
    '$rootScope',
    'sensorDatasheetContext',
    'siContext',
    'sensorDatasheetFinder',
    'sensorDatasheetUnitMeasurementDefaultFinder',
    'sensorDatasheetUnitMeasurementScaleFinder',
    'sensorTypeFinder',
    'unitMeasurementScaleFinder',
    'sensorTypeConstant',
    'sensorDatasheetConstant',
    'sensorDatasheetUnitMeasurementDefaultConstant',
    'sensorDatasheetUnitMeasurementScaleConstant',
    'sensorUnitMeasurementScaleFinder',
    function (
        $rootScope,
        sensorDatasheetContext,
        siContext,
        sensorDatasheetFinder,
        sensorDatasheetUnitMeasurementDefaultFinder,
        sensorDatasheetUnitMeasurementScaleFinder,
        sensorTypeFinder,
        unitMeasurementScaleFinder,
        sensorTypeConstant,
        sensorDatasheetConstant,
        sensorDatasheetUnitMeasurementDefaultConstant,
        sensorDatasheetUnitMeasurementScaleConstant,
        sensorUnitMeasurementScaleFinder) {

        var serviceFactory = {};

        sensorDatasheetContext.$watchCollection('sensorType', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var sensorType = newValues[i];
                sensorType.sensorDatasheets = function () { return sensorDatasheetFinder.getBySensorTypeKey(this.sensorTypeId); }
            }
        });

        sensorDatasheetContext.$watchCollection('sensorDatasheet', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var sensorDatasheet = newValues[i];
                sensorDatasheet.sensorType = function () { return sensorTypeFinder.getByKey(this.sensorTypeId); }
                sensorDatasheet.sensorDatasheetUnitMeasurementDefault = function () { return sensorDatasheetUnitMeasurementDefaultFinder.getByKey(this.sensorDatasheetId, this.sensorTypeId); }
                sensorDatasheet.sensorDatasheetUnitMeasurementScales = function () { return sensorDatasheetUnitMeasurementScaleFinder.getBySensorDatasheetKey(this.sensorDatasheetId, this.sensorTypeId); }
                sensorDatasheet.sensors = function () { return sensorFinder.getBySensorDatasheetKey(this.sensorDatasheetId, this.sensorTypeId); }
            }
        });

        sensorDatasheetContext.$watchCollection('sensorDatasheetUnitMeasurementDefault', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var sensorDatasheetUnitMeasurementDefault = newValues[i];
                sensorDatasheetUnitMeasurementDefault.unitMeasurementScale = function () { return unitMeasurementScaleFinder.getByKey(this.unitMeasurementId, this.unitMeasurementTypeId, this.numericalScalePrefixId, this.numericalScaleTypeId); }
            }
        });

        sensorDatasheetContext.$watchCollection('sensorDatasheetUnitMeasurementScale', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var sensorDatasheetUnitMeasurementScale = newValues[i];
                sensorDatasheetUnitMeasurementScale.unitMeasurementScale = function () { return unitMeasurementScaleFinder.getByKey(this.unitMeasurementId, this.unitMeasurementTypeId, this.numericalScalePrefixId, this.numericalScaleTypeId); }
                sensorDatasheetUnitMeasurementScale.sensorUnitMeasurementScales = function () { return sensorUnitMeasurementScaleFinder.getBySensorUnitMeasurementScaleKey(this.sensorDatasheetId, this.sensorTypeId, this.unitMeasurementId, this.unitMeasurementTypeId, this.numericalScalePrefixId, this.numericalScaleTypeId); }
            }
        });

        // *** Events Subscriptions

        var onSensorTypeGetAllCompleted = function (event, data) {
            sensorTypeGetAllCompletedSubscription();
            sensorDatasheetContext.sensorTypeLoaded = true;
        }

        var onSensorDatasheetGetAllCompleted = function (event, data) {
            sensorDatasheetGetAllCompletedSubscription();
            sensorDatasheetContext.sensorDatasheetLoaded = true;
        }

        var onSensorDatasheetUnitMeasurementDefaultGetAllCompleted = function (event, data) {
            sensorDatasheetUnitMeasurementDefaultGetAllCompletedSubscription();
            sensorDatasheetContext.sensorDatasheetUnitMeasurementDefaultLoaded = true;
        }

        var onSensorDatasheetUnitMeasurementScaleGetAllCompleted = function (event, data) {
            sensorDatasheetUnitMeasurementScaleGetAllCompletedSubscription();
            sensorDatasheetContext.sensorDatasheetUnitMeasurementScaleLoaded = true;
        }

        var sensorTypeGetAllCompletedSubscription = $rootScope.$on(sensorTypeConstant.getAllCompletedEventName, onSensorTypeGetAllCompleted);
        var sensorDatasheetGetAllCompletedSubscription = $rootScope.$on(sensorDatasheetConstant.getAllCompletedEventName, onSensorDatasheetGetAllCompleted);
        var sensorDatasheetUnitMeasurementDefaultGetAllCompletedSubscription = $rootScope.$on(sensorDatasheetUnitMeasurementDefaultConstant.getAllCompletedEventName, onSensorDatasheetUnitMeasurementDefaultGetAllCompleted);
        var sensorDatasheetUnitMeasurementScaleGetAllCompletedSubscription = $rootScope.$on(sensorDatasheetUnitMeasurementScaleConstant.getAllCompletedEventName, onSensorDatasheetUnitMeasurementScaleGetAllCompleted);

        $rootScope.$on('$destroy', function () {
            sensorTypeGetAllCompletedSubscription();
            sensorDatasheetGetAllCompletedSubscription();
            sensorDatasheetUnitMeasurementDefaultGetAllCompletedSubscription();
            sensorDatasheetUnitMeasurementScaleGetAllCompletedSubscription();
        });

        // *** Events Subscriptions


        return serviceFactory;

    }]);