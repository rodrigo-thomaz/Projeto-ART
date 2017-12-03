'use strict';
app.factory('contextScope', ['$rootScope', function ($rootScope) {

    var context = $rootScope.$new();

    // *** Public Properties ***

    // Locale

    context.continents = [];    
    context.continentLoaded = false;    

    context.countries = [];    
    context.countryLoaded = false;    

    //SI

    context.numericalScales = [];    
    context.numericalScaleLoaded = false;

    context.numericalScalePrefixes = [];    
    context.numericalScalePrefixLoaded = false;    

    context.numericalScaleTypes = [];    
    context.numericalScaleTypeLoaded = false;    

    context.numericalScaleTypeCountries = []; 
    context.numericalScaleTypeCountryLoaded = false;    
    
    context.unitMeasurementScales = [];
    context.unitMeasurementScaleLoaded = false;
        
    context.unitMeasurementTypes = [];    
    context.unitMeasurementTypeLoaded = false;
        
    context.unitMeasurements = [];
    context.unitMeasurementLoaded = false;

    //

    context.sensorTypeLoaded = false;
    context.sensorTypes = [];

    context.sensorDatasheetLoaded = false;
    context.sensorDatasheets = [];

    context.sensorUnitMeasurementDefaultLoaded = false;
    context.sensorUnitMeasurementDefaults = [];

    context.sensorsLoaded = false;
    context.sensors = [];    

    // *** Finders ***

    // Locale

    var getContinentByKey = function (continentId) {
        for (var i = 0; i < context.continents.length; i++) {
            var item = context.continents[i];
            if (item.continentId === continentId) {
                return item;
            }
        }
    }

    var getCountryByKey = function (countryId) {
        for (var i = 0; i < context.countries.length; i++) {
            var item = context.countries[i];
            if (item.countryId === countryId) {
                return item;
            }
        }
    }

    // SI

    var getNumericalScaleTypeByKey = function (numericalScaleTypeId) {
        for (var i = 0; i < context.numericalScaleTypes.length; i++) {
            var item = context.numericalScaleTypes[i];
            if (item.numericalScaleTypeId === numericalScaleTypeId) {
                return item;
            }
        }
    }

    var getNumericalScalePrefixByKey = function (numericalScalePrefixId) {
        for (var i = 0; i < context.numericalScalePrefixes.length; i++) {
            var item = context.numericalScalePrefixes[i];
            if (item.numericalScalePrefixId === numericalScalePrefixId) {
                return item;
            }
        }
    }

    var getUnitMeasurementTypeByKey = function (unitMeasurementTypeId) {
        for (var i = 0; i < context.unitMeasurementTypes.length; i++) {
            var item = context.unitMeasurementTypes[i];
            if (item.unitMeasurementTypeId === unitMeasurementTypeId) {
                return item;
            }
        }
    }

    var getUnitMeasurementByKey = function (unitMeasurementId, unitMeasurementTypeId) {
        for (var i = 0; i < context.unitMeasurements.length; i++) {
            var item = context.unitMeasurements[i];
            if (item.unitMeasurementId === unitMeasurementId && item.unitMeasurementTypeId === unitMeasurementTypeId) {
                return item;
            }
        }
    }

    var getUnitMeasurementScaleByKey = function (unitMeasurementId, unitMeasurementTypeId, numericalScalePrefixId, numericalScaleTypeId) {
        for (var i = 0; i < context.unitMeasurementScales.length; i++) {
            var item = context.unitMeasurementScales[i];
            if (item.unitMeasurementId === unitMeasurementId && item.unitMeasurementTypeId === unitMeasurementTypeId && item.numericalScalePrefixId === numericalScalePrefixId && item.numericalScaleTypeId === numericalScaleTypeId) {
                return item;
            }
        }
    }    

    //
    
    var getSensorTypeByKey = function (sensorTypeId) {
        for (var i = 0; i < context.sensorTypes.length; i++) {
            var item = context.sensorTypes[i];
            if (item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    }

    var getSensorDatasheetByKey = function (sensorDatasheetId, sensorTypeId) {
        for (var i = 0; i < context.sensorDatasheets.length; i++) {
            var item = context.sensorDatasheets[i];
            if (item.sensorDatasheetId === sensorDatasheetId && item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    }

    var getSensorUnitMeasurementDefaultByKey = function (sensorUnitMeasurementDefaultId, sensorTypeId) {
        for (var i = 0; i < context.sensorUnitMeasurementDefaults.length; i++) {
            var item = context.sensorUnitMeasurementDefaults[i];
            if (item.sensorUnitMeasurementDefaultId === sensorUnitMeasurementDefaultId && item.sensorTypeId === sensorTypeId) {
                return item;
            }
        }
    };

    var getSensorByKey = function (sensorId) {
        for (var i = 0; i < context.sensors.length; i++) {
            var item = context.sensors[i];
            if (item.sensorId === sensorId) {
                return item;
            }
        }
    };


    // *** Navigation Properties Mappers ***

    // Locale

    var mapper_Country_Continent_Init = false;
    var mapper_Country_Continent = function () {
        if (!mapper_Country_Continent_Init && context.countryLoaded && context.continentLoaded) {
            mapper_Country_Continent_Init = true;
            for (var i = 0; i < context.countries.length; i++) {
                var country = context.countries[i];
                var continent = getContinentByKey(country.continentId);
                country.continent = continent;
                delete country.continentId; // removendo a foreing key
                if (continent.countries === undefined) {
                    continent.countries = [];
                }
                continent.countries.push(country);
            }
        }
    };

    // SI

    var mapper_NumericalScaleTypeCountry_Init = false;
    var mapper_NumericalScaleTypeCountry = function () {
        if (!mapper_NumericalScaleTypeCountry_Init && context.numericalScaleTypeCountryLoaded && context.countryLoaded) {
            mapper_NumericalScaleTypeCountry_Init = true;
            for (var i = 0; i < context.numericalScaleTypeCountries.length; i++) {
                var numericalScaleTypeCountry = context.numericalScaleTypeCountries[i];
                var numericalScaleType = getNumericalScaleTypeByKey(numericalScaleTypeCountry.numericalScaleTypeId);
                var country = getCountryByKey(numericalScaleTypeCountry.countryId);
                //Atach in numericalScaleType
                if (numericalScaleType.countries === undefined) {
                    numericalScaleType.countries = [];
                }
                numericalScaleType.countries.push(country);
                //Atach in country
                if (country.numericalScaleTypes === undefined) {
                    country.numericalScaleTypes = [];
                }
                country.numericalScaleTypes.push(numericalScaleType);
            }
            delete context.numericalScaleTypeCountries;
            delete context.numericalScaleTypeCountryLoaded;
        }
    };

    var mapper_NumericalScale_NumericalScalePrefix_Init = false;
    var mapper_NumericalScale_NumericalScalePrefix = function () {
        if (!mapper_NumericalScale_NumericalScalePrefix_Init && context.numericalScaleLoaded && context.numericalScalePrefixLoaded) {
            mapper_NumericalScale_NumericalScalePrefix_Init = true;            
            for (var i = 0; i < context.numericalScales.length; i++) {
                var numericalScale = context.numericalScales[i];
                var numericalScalePrefix = getNumericalScalePrefixByKey(numericalScale.numericalScalePrefixId);
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
        if (!mapper_NumericalScale_NumericalScaleType_Init && context.numericalScaleLoaded && context.numericalScaleTypeLoaded) {
            mapper_NumericalScale_NumericalScaleType_Init = true;
            for (var i = 0; i < context.numericalScales.length; i++) {
                var numericalScale = context.numericalScales[i];
                var numericalScaleType = getNumericalScaleTypeByKey(numericalScale.numericalScaleTypeId);
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
        if (!mapper_UnitMeasurement_UnitMeasurementType_Init && context.unitMeasurementTypeLoaded && context.unitMeasurementLoaded) {
            mapper_UnitMeasurement_UnitMeasurementType_Init = true;
            for (var i = 0; i < context.unitMeasurements.length; i++) {
                var unitMeasurement = context.unitMeasurements[i];
                var unitMeasurementType = getUnitMeasurementTypeByKey(unitMeasurement.unitMeasurementTypeId);
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
        if (!mapper_UnitMeasurementScale_UnitMeasurement_Init && context.unitMeasurementScaleLoaded && context.unitMeasurementLoaded) {
            mapper_UnitMeasurementScale_UnitMeasurement_Init = true;
            for (var i = 0; i < context.unitMeasurementScales.length; i++) {
                var unitMeasurementScale = context.unitMeasurementScales[i];
                var unitMeasurement = getUnitMeasurementByKey(unitMeasurementScale.unitMeasurementId, unitMeasurementScale.unitMeasurementTypeId);
                unitMeasurementScale.unitMeasurement = unitMeasurement;
                if (unitMeasurement.unitMeasurementScales === undefined) {
                    unitMeasurement.unitMeasurementScales = [];
                }
                unitMeasurement.unitMeasurementScales.push(unitMeasurementScale);
            }
        }
    };

    //

    var mapper_SensorDatasheet_SensorTypeType_Init = false;
    var mapper_SensorDatasheet_SensorTypeType = function () {
        if (!mapper_SensorDatasheet_SensorTypeType_Init && context.sensorDatasheetLoaded && context.sensorTypeLoaded) {
            mapper_SensorDatasheet_SensorTypeType_Init = true;
            for (var i = 0; i < context.sensorDatasheets.length; i++) {
                var sensorDatasheet = context.sensorDatasheets[i];
                var sensorType = getSensorTypeByKey(sensorDatasheet.sensorTypeId);
                sensorDatasheet.sensorType = sensorType;
                if (sensorType.sensorDatasheets === undefined) {
                    sensorType.sensorDatasheets = [];
                }
                sensorType.sensorDatasheets.push(sensorDatasheet);
            }
        }
    };

    var mapper_SensorUnitMeasurementDefault_UnitMeasurement_Init = false;
    var mapper_SensorUnitMeasurementDefault_UnitMeasurement = function () {
        if (!mapper_SensorUnitMeasurementDefault_UnitMeasurement_Init && context.sensorUnitMeasurementDefaultLoaded && context.unitMeasurementLoaded) {
            mapper_SensorUnitMeasurementDefault_UnitMeasurement_Init = true;
            for (var i = 0; i < context.sensorUnitMeasurementDefaults.length; i++) {
                var sensorUnitMeasurementDefault = context.sensorUnitMeasurementDefaults[i];
                var unitMeasurement = getUnitMeasurementByKey(sensorUnitMeasurementDefault.unitMeasurementId, sensorUnitMeasurementDefault.unitMeasurementTypeId);
                sensorUnitMeasurementDefault.unitMeasurement = unitMeasurement;
                if (unitMeasurement.sensorUnitMeasurementDefaults === undefined) {
                    unitMeasurement.sensorUnitMeasurementDefaults = [];
                }
                unitMeasurement.sensorUnitMeasurementDefaults.push(sensorUnitMeasurementDefault);
            }
        }
    };

    var mapper_SensorUnitMeasurementDefault_SensorType_Init = false;
    var mapper_SensorUnitMeasurementDefault_SensorType = function () {
        if (!mapper_SensorUnitMeasurementDefault_SensorType_Init && context.sensorUnitMeasurementDefaultLoaded && context.sensorTypeLoaded) {
            mapper_SensorUnitMeasurementDefault_SensorType_Init = true;
            for (var i = 0; i < context.sensorUnitMeasurementDefaults.length; i++) {
                var sensorUnitMeasurementDefault = context.sensorUnitMeasurementDefaults[i];
                var sensorType = getSensorTypeByKey(sensorUnitMeasurementDefault.sensorTypeId);
                sensorUnitMeasurementDefault.sensorType = sensorType;
                if (sensorType.sensorUnitMeasurementDefaults === undefined) {
                    sensorType.sensorUnitMeasurementDefaults = [];
                }
                sensorType.sensorUnitMeasurementDefaults.push(sensorUnitMeasurementDefault);
            }
        }
    };

    // *** Watches ***

    // Locale

    context.$watch('continentLoaded', function (newValue, oldValue) {
        mapper_Country_Continent();
    });

    context.$watch('countryLoaded', function (newValue, oldValue) {
        mapper_Country_Continent();
        mapper_NumericalScaleTypeCountry();
    });

    // SI

    context.$watch('numericalScaleLoaded', function (newValue, oldValue) {
        mapper_NumericalScale_NumericalScalePrefix();
        mapper_NumericalScale_NumericalScaleType();
    });

    context.$watch('numericalScalePrefixLoaded', function (newValue, oldValue) {
        mapper_NumericalScale_NumericalScalePrefix();
    });

    context.$watch('numericalScaleTypeLoaded', function (newValue, oldValue) {
        mapper_NumericalScaleTypeCountry();
        mapper_NumericalScale_NumericalScaleType();
    });

    context.$watch('numericalScaleTypeCountryLoaded', function (newValue, oldValue) {
        mapper_NumericalScaleTypeCountry();
    });

    context.$watch('unitMeasurementScaleLoaded', function (newValue, oldValue) {
        mapper_UnitMeasurementScale_UnitMeasurement();
    });

    //

    context.$watch('sensorUnitMeasurementScaleLoaded', function (newValue, oldValue) {

    });

    context.$watch('unitMeasurementTypeLoaded', function (newValue, oldValue) {
        mapper_UnitMeasurement_UnitMeasurementType();
    });

    context.$watch('unitMeasurementLoaded', function (newValue, oldValue) {
        mapper_UnitMeasurement_UnitMeasurementType();
        mapper_UnitMeasurementScale_UnitMeasurement();
        mapper_SensorUnitMeasurementDefault_UnitMeasurement();
    });    

    context.$watch('sensorTypeLoaded', function (newValue, oldValue) {
        mapper_SensorDatasheet_SensorTypeType();
        mapper_SensorUnitMeasurementDefault_SensorType();
    });

    context.$watch('sensorDatasheetLoaded', function (newValue, oldValue) {
        mapper_SensorDatasheet_SensorTypeType();
    });

    context.$watch('sensorUnitMeasurementDefaultLoaded', function (newValue, oldValue) {
        mapper_SensorUnitMeasurementDefault_UnitMeasurement();
        mapper_SensorUnitMeasurementDefault_SensorType();
    });

    context.$watch('sensorsLoaded', function (newValue, oldValue) {
        
    });

    // *** Public Methods ***

    // Locale

    context.getContinentByKey = getContinentByKey;
    context.getCountryByKey = getCountryByKey;

    // SI

    context.getUnitMeasurementTypeByKey = getUnitMeasurementTypeByKey;
    context.getUnitMeasurementByKey = getUnitMeasurementByKey;

    //
        
    context.getSensorTypeByKey = getSensorTypeByKey;
    context.getSensorDatasheetByKey = getSensorDatasheetByKey;
    context.getSensorUnitMeasurementDefaultByKey = getSensorUnitMeasurementDefaultByKey;
    context.getSensorByKey = getSensorByKey;

    return context;

}]);