'use strict';
app.factory('siMapper', [
    '$rootScope',
    'siContext',
    'numericalScaleFinder',
    'numericalScalePrefixFinder',
    'numericalScaleTypeFinder',
    'unitMeasurementScaleFinder',
    'unitMeasurementFinder',
    'unitMeasurementTypeFinder',
    'localeContext',
    'countryFinder',
    'numericalScalePrefixConstant',
    'numericalScaleConstant',
    'numericalScaleTypeCountryConstant',
    'numericalScaleTypeConstant',
    'unitMeasurementScaleConstant',
    'unitMeasurementConstant',
    'unitMeasurementTypeConstant',
    function (
        $rootScope,
        siContext,
        numericalScaleFinder,
        numericalScalePrefixFinder,
        numericalScaleTypeFinder,
        unitMeasurementScaleFinder,
        unitMeasurementFinder,
        unitMeasurementTypeFinder,
        localeContext,
        countryFinder,
        numericalScalePrefixConstant,
        numericalScaleConstant,
        numericalScaleTypeCountryConstant,
        numericalScaleTypeConstant,
        unitMeasurementScaleConstant,
        unitMeasurementConstant,
        unitMeasurementTypeConstant) {

        var serviceFactory = {};

        siContext.$watchCollection('numericalScaleTypeCountry', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var numericalScaleTypeCountry = newValues[i];
                numericalScaleTypeCountry.country = function () { return countryFinder.getByKey(this.countryId); }
            }
        });

        localeContext.$watchCollection('country', function (newValues, oldValues) {
            for (var i = 0; i < newValues.length; i++) {
                var country = newValues[i];
                country.numericalScaleTypes = function () {
                    var result = [];
                    for (var i = 0; i < siContext.numericalScaleTypeCountry.length; i++) {
                        if (siContext.numericalScaleTypeCountry[i].countryId === this.countryId) {
                            result.push(siContext.numericalScaleTypeCountry[i]);
                        }
                    }
                    return result;
                }
            }
        });

        // *** Navigation Properties Mappers ***

        var mapper_NumericalScaleTypeCountry_Init = false;
        var mapper_NumericalScaleTypeCountry = function () {
            if (!mapper_NumericalScaleTypeCountry_Init && siContext.numericalScaleTypeCountryLoaded && siContext.numericalScaleTypeLoaded && localeContext.countryLoaded) {
                mapper_NumericalScaleTypeCountry_Init = true;
                countryLoadedUnbinding();
                for (var i = 0; i < siContext.numericalScaleTypeCountry.length; i++) {
                    var numericalScaleTypeCountry = siContext.numericalScaleTypeCountry[i];
                    var numericalScaleType = numericalScaleTypeFinder.getByKey(numericalScaleTypeCountry.numericalScaleTypeId);
                    var country = countryFinder.getByKey(numericalScaleTypeCountry.countryId);
                    //Atach in numericalScaleType
                    if (numericalScaleType.countries === undefined) {
                        numericalScaleType.countries = [];
                    }
                    numericalScaleType.countries.push(country);
                    //Atach in country
                    //if (country.numericalScaleTypes === undefined) {
                    //    country.numericalScaleTypes = [];
                    //}
                    //country.numericalScaleTypes.push(numericalScaleType);
                }
                //delete siContext.numericalScaleTypeCountry;
                //delete siContext.numericalScaleTypeCountryLoaded;
            }
        };

        var mapper_NumericalScale_NumericalScalePrefix_Init = false;
        var mapper_NumericalScale_NumericalScalePrefix = function () {
            if (!mapper_NumericalScale_NumericalScalePrefix_Init && siContext.numericalScaleLoaded && siContext.numericalScalePrefixLoaded) {
                mapper_NumericalScale_NumericalScalePrefix_Init = true;
                for (var i = 0; i < siContext.numericalScale.length; i++) {
                    var numericalScale = siContext.numericalScale[i];
                    var numericalScalePrefix = numericalScalePrefixFinder.getByKey(numericalScale.numericalScalePrefixId);
                    numericalScale.numericalScalePrefix = numericalScalePrefix;
                    if (numericalScalePrefix.numericalScales === undefined) {
                        numericalScalePrefix.numericalScales = [];
                    }
                    numericalScalePrefix.numericalScales.push(numericalScale);
                }
            }
        };

        var mapper_NumericalScale_NumericalScaleType_Init = false;
        var mapper_NumericalScale_NumericalScaleType = function () {
            if (!mapper_NumericalScale_NumericalScaleType_Init && siContext.numericalScaleLoaded && siContext.numericalScaleTypeLoaded) {
                mapper_NumericalScale_NumericalScaleType_Init = true;
                for (var i = 0; i < siContext.numericalScale.length; i++) {
                    var numericalScale = siContext.numericalScale[i];
                    var numericalScaleType = numericalScaleTypeFinder.getByKey(numericalScale.numericalScaleTypeId);
                    numericalScale.numericalScaleType = numericalScaleType;
                    if (numericalScaleType.numericalScales === undefined) {
                        numericalScaleType.numericalScales = [];
                    }
                    numericalScaleType.numericalScales.push(numericalScale);
                }
            }
        };

        var mapper_UnitMeasurement_UnitMeasurementType_Init = false;
        var mapper_UnitMeasurement_UnitMeasurementType = function () {
            if (!mapper_UnitMeasurement_UnitMeasurementType_Init && siContext.unitMeasurementTypeLoaded && siContext.unitMeasurementLoaded) {
                mapper_UnitMeasurement_UnitMeasurementType_Init = true;
                for (var i = 0; i < siContext.unitMeasurement.length; i++) {
                    var unitMeasurement = siContext.unitMeasurement[i];
                    var unitMeasurementType = unitMeasurementTypeFinder.getByKey(unitMeasurement.unitMeasurementTypeId);
                    unitMeasurement.unitMeasurementType = unitMeasurementType;
                    if (unitMeasurementType.unitMeasurements === undefined) {
                        unitMeasurementType.unitMeasurements = [];
                    }
                    unitMeasurementType.unitMeasurements.push(unitMeasurement);
                }
            }
        };

        var mapper_UnitMeasurementScale_UnitMeasurement_Init = false;
        var mapper_UnitMeasurementScale_UnitMeasurement = function () {
            if (!mapper_UnitMeasurementScale_UnitMeasurement_Init && siContext.unitMeasurementScaleLoaded && siContext.unitMeasurementLoaded) {
                mapper_UnitMeasurementScale_UnitMeasurement_Init = true;
                for (var i = 0; i < siContext.unitMeasurementScale.length; i++) {
                    var unitMeasurementScale = siContext.unitMeasurementScale[i];
                    var unitMeasurement = unitMeasurementFinder.getByKey(unitMeasurementScale.unitMeasurementId, unitMeasurementScale.unitMeasurementTypeId);
                    unitMeasurementScale.unitMeasurement = unitMeasurement;
                    if (unitMeasurement.unitMeasurementScales === undefined) {
                        unitMeasurement.unitMeasurementScales = [];
                    }
                    unitMeasurement.unitMeasurementScales.push(unitMeasurementScale);
                }
            }
        };

        var mapper_UnitMeasurementScale_NumericalScale_Init = false;
        var mapper_UnitMeasurementScale_NumericalScale = function () {
            if (!mapper_UnitMeasurementScale_NumericalScale_Init && siContext.unitMeasurementScaleLoaded && siContext.numericalScaleLoaded) {
                mapper_UnitMeasurementScale_NumericalScale_Init = true;
                for (var i = 0; i < siContext.unitMeasurementScale.length; i++) {
                    var unitMeasurementScale = siContext.unitMeasurementScale[i];
                    var numericalScale = numericalScaleFinder.getByKey(unitMeasurementScale.numericalScalePrefixId, unitMeasurementScale.numericalScaleTypeId);
                    unitMeasurementScale.numericalScale = numericalScale;
                    if (numericalScale.unitMeasurementScales === undefined) {
                        numericalScale.unitMeasurementScales = [];
                    }
                    numericalScale.unitMeasurementScales.push(unitMeasurementScale);
                }
            }
        };

        // *** Navigation Properties Mappers ***


        // *** Events Subscriptions    

        var onNumericalScalePrefixGetAllCompleted = function (event, data) {
            numericalScalePrefixGetAllCompletedSubscription();
            siContext.numericalScalePrefixLoaded = true;
            mapper_NumericalScale_NumericalScalePrefix();
        }

        var onNumericalScaleGetAllCompleted = function (event, data) {
            numericalScaleGetAllCompletedSubscription();
            siContext.numericalScaleLoaded = true;
            mapper_NumericalScale_NumericalScalePrefix();
            mapper_NumericalScale_NumericalScaleType();
            mapper_UnitMeasurementScale_NumericalScale();
        }

        var onNumericalScaleTypeCountryGetAllCompleted = function (event, data) {
            numericalScaleTypeCountryGetAllCompletedSubscription();
            siContext.numericalScaleTypeCountryLoaded = true;
            mapper_NumericalScaleTypeCountry();
        }

        var onNumericalScaleTypeGetAllCompleted = function (event, data) {
            numericalScaleTypeGetAllCompletedSubscription();
            siContext.numericalScaleTypeLoaded = true;
            mapper_NumericalScaleTypeCountry();
            mapper_NumericalScale_NumericalScaleType();
            mapper_NumericalScaleTypeCountry();
        }

        var onUnitMeasurementScaleGetAllCompleted = function (event, data) {
            unitMeasurementScaleGetAllCompletedSubscription();
            siContext.unitMeasurementScaleLoaded = true;
            mapper_UnitMeasurementScale_UnitMeasurement();
            mapper_UnitMeasurementScale_NumericalScale();
        }

        var onUnitMeasurementGetAllCompleted = function (event, data) {
            unitMeasurementGetAllCompletedSubscription();
            siContext.unitMeasurementLoaded = true;
            mapper_UnitMeasurement_UnitMeasurementType();
            mapper_UnitMeasurementScale_UnitMeasurement();
        }

        var onUnitMeasurementTypeGetAllCompleted = function (event, data) {
            unitMeasurementTypeGetAllCompletedSubscription();
            siContext.unitMeasurementTypeLoaded = true;
            mapper_UnitMeasurement_UnitMeasurementType();
        }

        var numericalScalePrefixGetAllCompletedSubscription = $rootScope.$on(numericalScalePrefixConstant.getAllCompletedEventName, onNumericalScalePrefixGetAllCompleted);
        var numericalScaleGetAllCompletedSubscription = $rootScope.$on(numericalScaleConstant.getAllCompletedEventName, onNumericalScaleGetAllCompleted);
        var numericalScaleTypeCountryGetAllCompletedSubscription = $rootScope.$on(numericalScaleTypeCountryConstant.getAllCompletedEventName, onNumericalScaleTypeCountryGetAllCompleted);
        var numericalScaleTypeGetAllCompletedSubscription = $rootScope.$on(numericalScaleTypeConstant.getAllCompletedEventName, onNumericalScaleTypeGetAllCompleted);
        var unitMeasurementScaleGetAllCompletedSubscription = $rootScope.$on(unitMeasurementScaleConstant.getAllCompletedEventName, onUnitMeasurementScaleGetAllCompleted);
        var unitMeasurementGetAllCompletedSubscription = $rootScope.$on(unitMeasurementConstant.getAllCompletedEventName, onUnitMeasurementGetAllCompleted);
        var unitMeasurementTypeGetAllCompletedSubscription = $rootScope.$on(unitMeasurementTypeConstant.getAllCompletedEventName, onUnitMeasurementTypeGetAllCompleted);

        $rootScope.$on('$destroy', function () {
            numericalScalePrefixGetAllCompletedSubscription();
            numericalScaleGetAllCompletedSubscription();
            numericalScaleTypeCountryGetAllCompletedSubscription();
            numericalScaleTypeGetAllCompletedSubscription();
            unitMeasurementScaleGetAllCompletedSubscription();
            unitMeasurementGetAllCompletedSubscription();
            unitMeasurementTypeGetAllCompletedSubscription();
        });

        // *** Events Subscriptions

        // *** Watches

        var countryLoadedUnbinding = localeContext.$watch('countryLoaded', function (newValue, oldValue) {
            mapper_NumericalScaleTypeCountry();
        })

        // *** Watches

        return serviceFactory;

    }]);