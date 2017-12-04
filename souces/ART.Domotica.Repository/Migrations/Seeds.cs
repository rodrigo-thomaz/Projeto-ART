namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;

    using ART.Domotica.Constant;
    using ART.Domotica.Enums;
    using ART.Domotica.Enums.Locale;
    using ART.Domotica.Enums.SI;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Entities.Locale;
    using ART.Domotica.Repository.Entities.SI;
    using ART.Infra.CrossCutting.MQ;
    using ART.Infra.CrossCutting.Setting;
    using ART.Infra.CrossCutting.Utils;

    public class Seeds
    {
        #region Methods

        public static void Execute(ARTDbContext context)
        {
            ExecuteNumericalScaleType(context);
            ExecuteNumericalScalePrefix(context);
            ExecuteNumericalScale(context);
            ExecuteContinent(context);
            ExecuteCountry(context);
            ExecuteTimeZone(context);
            ExecuteUnitMeasurementType(context);
            ExecuteUnitMeasurement(context);
            ExecuteUnitMeasurementScale(context);
            ExecuteSensorType(context);
            ExecuteActuatorType(context);
            ExecuteSensorDatasheet(context);
            ExecuteSensorUnitMeasurementDefault(context);
            ExecuteSensorDatasheetUnitMeasurementScale(context);

            #region SensorTempDSFamilyResolutions

            var sensorTempDSFamilyResolution9 = context.SensorTempDSFamilyResolution.SingleOrDefault(x => x.Id == 9);
            var sensorTempDSFamilyResolution10 = context.SensorTempDSFamilyResolution.SingleOrDefault(x => x.Id == 10);
            var sensorTempDSFamilyResolution11 = context.SensorTempDSFamilyResolution.SingleOrDefault(x => x.Id == 11);
            var sensorTempDSFamilyResolution12 = context.SensorTempDSFamilyResolution.SingleOrDefault(x => x.Id == 12);

            if (sensorTempDSFamilyResolution9 == null)
            {
                sensorTempDSFamilyResolution9 = new SensorTempDSFamilyResolution { Id = 9 };
                context.SensorTempDSFamilyResolution.Add(sensorTempDSFamilyResolution9);
            }

            if (sensorTempDSFamilyResolution10 == null)
            {
                sensorTempDSFamilyResolution10 = new SensorTempDSFamilyResolution { Id = 10 };
                context.SensorTempDSFamilyResolution.Add(sensorTempDSFamilyResolution10);
            }

            if (sensorTempDSFamilyResolution11 == null)
            {
                sensorTempDSFamilyResolution11 = new SensorTempDSFamilyResolution { Id = 11 };
                context.SensorTempDSFamilyResolution.Add(sensorTempDSFamilyResolution11);
            }

            if (sensorTempDSFamilyResolution12 == null)
            {
                sensorTempDSFamilyResolution12 = new SensorTempDSFamilyResolution { Id = 12 };
                context.SensorTempDSFamilyResolution.Add(sensorTempDSFamilyResolution12);
            }

            sensorTempDSFamilyResolution9.Name = "9 bits";
            sensorTempDSFamilyResolution9.Bits = 9;
            sensorTempDSFamilyResolution9.Resolution = 0.5M;
            sensorTempDSFamilyResolution9.ConversionTime = 93.75M;
            sensorTempDSFamilyResolution9.DecimalPlaces = 1;
            sensorTempDSFamilyResolution9.Description = "Resolução de 9 bits";

            sensorTempDSFamilyResolution10.Name = "10 bits";
            sensorTempDSFamilyResolution10.Bits = 10;
            sensorTempDSFamilyResolution10.Resolution = 0.25M;
            sensorTempDSFamilyResolution10.ConversionTime = 187.5M;
            sensorTempDSFamilyResolution10.DecimalPlaces = 2;
            sensorTempDSFamilyResolution10.Description = "Resolução de 10 bits";

            sensorTempDSFamilyResolution11.Name = "11 bits";
            sensorTempDSFamilyResolution11.Bits = 11;
            sensorTempDSFamilyResolution11.Resolution = 0.125M;
            sensorTempDSFamilyResolution11.ConversionTime = 375;
            sensorTempDSFamilyResolution11.DecimalPlaces = 3;
            sensorTempDSFamilyResolution11.Description = "Resolução de 11 bits";

            sensorTempDSFamilyResolution12.Name = "12 bits";
            sensorTempDSFamilyResolution12.Bits = 12;
            sensorTempDSFamilyResolution12.Resolution = 0.0625M;
            sensorTempDSFamilyResolution12.ConversionTime = 750;
            sensorTempDSFamilyResolution12.DecimalPlaces = 4;
            sensorTempDSFamilyResolution12.Description = "Resolução de 12 bits";

            context.SaveChanges();

            #endregion

            #region SensorTempDSFamily

            var sensor_1_Address = "28fff62293165b0";
            var sensor_2_1_Address = "40:255:231:109:162:22:3:211";
            var sensor_2_2_Address = "40:255:254:101:147:22:4:182";
            var sensor_3_1_Address = "40:255:192:95:147:22:4:195";
            var sensor_3_2_Address = "40:255:113:95:147:22:4:65";

            var sensor_1 = context.Sensor.Include(x => x.SensorTempDSFamily).Include(x => x.SensorUnitMeasurementScale).Include(x => x.SensorTriggers).SingleOrDefault(x => x.SensorTempDSFamily.DeviceAddress.ToLower() == sensor_1_Address.ToLower());
            var sensor_2_1 = context.Sensor.Include(x => x.SensorTempDSFamily).Include(x => x.SensorUnitMeasurementScale).Include(x => x.SensorTriggers).SingleOrDefault(x => x.SensorTempDSFamily.DeviceAddress.ToLower() == sensor_2_1_Address.ToLower());
            var sensor_2_2 = context.Sensor.Include(x => x.SensorTempDSFamily).Include(x => x.SensorUnitMeasurementScale).Include(x => x.SensorTriggers).SingleOrDefault(x => x.SensorTempDSFamily.DeviceAddress.ToLower() == sensor_2_2_Address.ToLower());
            var sensor_3_1 = context.Sensor.Include(x => x.SensorTempDSFamily).Include(x => x.SensorUnitMeasurementScale).Include(x => x.SensorTriggers).SingleOrDefault(x => x.SensorTempDSFamily.DeviceAddress.ToLower() == sensor_3_1_Address.ToLower());
            var sensor_3_2 = context.Sensor.Include(x => x.SensorTempDSFamily).Include(x => x.SensorUnitMeasurementScale).Include(x => x.SensorTriggers).SingleOrDefault(x => x.SensorTempDSFamily.DeviceAddress.ToLower() == sensor_3_2_Address.ToLower());

            if (sensor_1 == null)
            {
                sensor_1 = new Sensor
                {
                    SensorDatasheetId = SensorDatasheetEnum.Temperature_DS18B20,
                    SensorTypeId = SensorTypeEnum.Temperature,
                    SensorTempDSFamily = new SensorTempDSFamily
                    {
                        DeviceAddress = sensor_1_Address,
                        Family = "DS18B20",
                        SensorTempDSFamilyResolutionId = sensorTempDSFamilyResolution9.Id,
                        SensorTempDSFamilyResolution = sensorTempDSFamilyResolution9,
                    },
                    Label = "Sensor 1",
                    SensorTriggers = new List<SensorTrigger>
                    {
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "<min>-55</min><max>125</max>",
                            BuzzerOn = true,
                        },
                    },
                    SensorUnitMeasurementScale = new SensorUnitMeasurementScale
                    {
                        UnitMeasurementId = UnitMeasurementEnum.Celsius,
                        UnitMeasurementTypeId = UnitMeasurementTypeEnum.Temperature,
                        NumericalScalePrefixId = NumericalScalePrefixEnum.None,
                        NumericalScaleTypeId = NumericalScaleTypeEnum.Long,
                        ChartLimiterMin = 20,
                        ChartLimiterMax = 30,
                    },
                    CreateDate = DateTime.Now,
                };
                context.Sensor.Add(sensor_1);
            }
            else
            {
                sensor_1.SensorTempDSFamily.Family = "DS18B20";
                sensor_1.SensorTempDSFamily.DeviceAddress = sensor_1_Address;

                if (!sensor_1.SensorTriggers.Any())
                {
                    sensor_1.SensorTriggers = new List<SensorTrigger>();

                    sensor_1.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "<min>-55</min><max>125</max>",
                        BuzzerOn = true,
                    });
                }

                if (sensor_1.SensorUnitMeasurementScale == null)
                {
                    sensor_1.SensorUnitMeasurementScale = new SensorUnitMeasurementScale
                    {
                        UnitMeasurementId = UnitMeasurementEnum.Celsius,
                        UnitMeasurementTypeId = UnitMeasurementTypeEnum.Temperature,
                        NumericalScalePrefixId = NumericalScalePrefixEnum.None,
                        NumericalScaleTypeId = NumericalScaleTypeEnum.Long,
                        ChartLimiterMin = 20,
                        ChartLimiterMax = 30,
                    };
                }

            }

            if (sensor_2_1 == null)
            {
                sensor_2_1 = new Sensor
                {
                    SensorDatasheetId = SensorDatasheetEnum.Temperature_DS18B20,
                    SensorTypeId = SensorTypeEnum.Temperature,
                    SensorTempDSFamily = new SensorTempDSFamily
                    {
                        DeviceAddress = sensor_2_1_Address,
                        Family = "DS18B20",
                        SensorTempDSFamilyResolutionId = sensorTempDSFamilyResolution11.Id,
                        SensorTempDSFamilyResolution = sensorTempDSFamilyResolution11,
                    },
                    Label = "Sensor 1",
                    SensorTriggers = new List<SensorTrigger>
                    {
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "<min>-55</min><max>125</max>",
                            BuzzerOn = true,
                        },
                    },
                    SensorUnitMeasurementScale = new SensorUnitMeasurementScale
                    {
                        UnitMeasurementId = UnitMeasurementEnum.Celsius,
                        UnitMeasurementTypeId = UnitMeasurementTypeEnum.Temperature,
                        NumericalScalePrefixId = NumericalScalePrefixEnum.None,
                        NumericalScaleTypeId = NumericalScaleTypeEnum.Long,
                        ChartLimiterMin = 20,
                        ChartLimiterMax = 30,
                    },
                    CreateDate = DateTime.Now,
                };
                context.Sensor.Add(sensor_2_1);
            }
            else
            {
                sensor_2_1.SensorTempDSFamily.Family = "DS18B20";
                sensor_2_1.SensorTempDSFamily.DeviceAddress = sensor_2_1_Address;

                if (!sensor_2_1.SensorTriggers.Any())
                {
                    sensor_2_1.SensorTriggers = new List<SensorTrigger>();

                    sensor_2_1.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "<min>-55</min><max>125</max>",
                        BuzzerOn = true,
                    });
                }

                if (sensor_2_1.SensorUnitMeasurementScale == null)
                {
                    sensor_2_1.SensorUnitMeasurementScale = new SensorUnitMeasurementScale
                    {
                        UnitMeasurementId = UnitMeasurementEnum.Celsius,
                        UnitMeasurementTypeId = UnitMeasurementTypeEnum.Temperature,
                        NumericalScalePrefixId = NumericalScalePrefixEnum.None,
                        NumericalScaleTypeId = NumericalScaleTypeEnum.Long,
                        ChartLimiterMin = 20,
                        ChartLimiterMax = 30,
                    };
                }
            }

            if (sensor_2_2 == null)
            {
                sensor_2_2 = new Sensor
                {
                    SensorDatasheetId = SensorDatasheetEnum.Temperature_DS18B20,
                    SensorTypeId = SensorTypeEnum.Temperature,
                    SensorTempDSFamily = new SensorTempDSFamily
                    {
                        DeviceAddress = sensor_2_2_Address,
                        Family = "DS18B20",
                        SensorTempDSFamilyResolutionId = sensorTempDSFamilyResolution11.Id,
                        SensorTempDSFamilyResolution = sensorTempDSFamilyResolution11,
                    },
                    Label = "Sensor 2",
                    SensorTriggers = new List<SensorTrigger>
                    {
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "<min>-55</min><max>125</max>",
                            BuzzerOn = true,
                        },
                    },
                    SensorUnitMeasurementScale = new SensorUnitMeasurementScale
                    {
                        UnitMeasurementId = UnitMeasurementEnum.Celsius,
                        UnitMeasurementTypeId = UnitMeasurementTypeEnum.Temperature,
                        NumericalScalePrefixId = NumericalScalePrefixEnum.None,
                        NumericalScaleTypeId = NumericalScaleTypeEnum.Long,
                        ChartLimiterMin = 20,
                        ChartLimiterMax = 30,
                    },
                    CreateDate = DateTime.Now,
                };
                context.Sensor.Add(sensor_2_2);
            }
            else
            {
                sensor_2_2.SensorTempDSFamily.Family = "DS18B20";
                sensor_2_2.SensorTempDSFamily.DeviceAddress = sensor_2_2_Address;

                if (!sensor_2_2.SensorTriggers.Any())
                {
                    sensor_2_2.SensorTriggers = new List<SensorTrigger>();

                    sensor_2_2.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "<min>-55</min><max>125</max>",
                        BuzzerOn = true,
                    });
                }

                if (sensor_2_2.SensorUnitMeasurementScale == null)
                {
                    sensor_2_2.SensorUnitMeasurementScale = new SensorUnitMeasurementScale
                    {
                        UnitMeasurementId = UnitMeasurementEnum.Celsius,
                        UnitMeasurementTypeId = UnitMeasurementTypeEnum.Temperature,
                        NumericalScalePrefixId = NumericalScalePrefixEnum.None,
                        NumericalScaleTypeId = NumericalScaleTypeEnum.Long,
                        ChartLimiterMin = 20,
                        ChartLimiterMax = 30,
                    };
                }
            }

            if (sensor_3_1 == null)
            {
                sensor_3_1 = new Sensor
                {
                    SensorDatasheetId = SensorDatasheetEnum.Temperature_DS18B20,
                    SensorTypeId = SensorTypeEnum.Temperature,
                    Label = "Sensor 3",
                    SensorTempDSFamily = new SensorTempDSFamily
                    {
                        DeviceAddress = sensor_3_1_Address,
                        Family = "DS18B20",
                        SensorTempDSFamilyResolutionId = sensorTempDSFamilyResolution11.Id,
                        SensorTempDSFamilyResolution = sensorTempDSFamilyResolution11,
                    },
                    SensorTriggers = new List<SensorTrigger>
                    {
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "<min>-55</min><max>125</max>",
                            BuzzerOn = true,
                        },
                    },
                    SensorUnitMeasurementScale = new SensorUnitMeasurementScale
                    {
                        UnitMeasurementId = UnitMeasurementEnum.Celsius,
                        UnitMeasurementTypeId = UnitMeasurementTypeEnum.Temperature,
                        NumericalScalePrefixId = NumericalScalePrefixEnum.None,
                        NumericalScaleTypeId = NumericalScaleTypeEnum.Long,
                        ChartLimiterMin = 20,
                        ChartLimiterMax = 30,
                    },
                    CreateDate = DateTime.Now,
                };
                context.Sensor.Add(sensor_3_1);
            }
            else
            {
                sensor_3_1.SensorTempDSFamily.Family = "DS18B20";
                sensor_3_1.SensorTempDSFamily.DeviceAddress = sensor_3_1_Address;

                if (!sensor_3_1.SensorTriggers.Any())
                {
                    sensor_3_1.SensorTriggers = new List<SensorTrigger>();

                    sensor_3_1.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "<min>-55</min><max>125</max>",
                        BuzzerOn = true,
                    });
                }

                if (sensor_3_1.SensorUnitMeasurementScale == null)
                {
                    sensor_3_1.SensorUnitMeasurementScale = new SensorUnitMeasurementScale
                    {
                        UnitMeasurementId = UnitMeasurementEnum.Celsius,
                        UnitMeasurementTypeId = UnitMeasurementTypeEnum.Temperature,
                        NumericalScalePrefixId = NumericalScalePrefixEnum.None,
                        NumericalScaleTypeId = NumericalScaleTypeEnum.Long,
                        ChartLimiterMin = 20,
                        ChartLimiterMax = 30,
                    };
                }
            }

            if (sensor_3_2 == null)
            {
                sensor_3_2 = new Sensor
                {
                    SensorDatasheetId = SensorDatasheetEnum.Temperature_DS18B20,
                    SensorTypeId = SensorTypeEnum.Temperature,
                    SensorTempDSFamily = new SensorTempDSFamily
                    {
                        DeviceAddress = sensor_3_2_Address,
                        Family = "DS18B20",
                        SensorTempDSFamilyResolutionId = sensorTempDSFamilyResolution11.Id,
                        SensorTempDSFamilyResolution = sensorTempDSFamilyResolution11,
                    },
                    Label = "Sensor 4",
                    SensorTriggers = new List<SensorTrigger>
                    {
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "<min>-55</min><max>125</max>",
                            BuzzerOn = true,
                        },
                    },
                    SensorUnitMeasurementScale = new SensorUnitMeasurementScale
                    {
                        UnitMeasurementId = UnitMeasurementEnum.Celsius,
                        UnitMeasurementTypeId = UnitMeasurementTypeEnum.Temperature,
                        NumericalScalePrefixId = NumericalScalePrefixEnum.None,
                        NumericalScaleTypeId = NumericalScaleTypeEnum.Long,
                        ChartLimiterMin = 20,
                        ChartLimiterMax = 30,
                    },
                    CreateDate = DateTime.Now,
                };
                context.Sensor.Add(sensor_3_2);
            }
            else
            {
                sensor_3_2.SensorTempDSFamily.Family = "DS18B20";
                sensor_3_2.SensorTempDSFamily.DeviceAddress = sensor_3_2_Address;

                if (!sensor_3_2.SensorTriggers.Any())
                {
                    sensor_3_2.SensorTriggers = new List<SensorTrigger>();

                    sensor_3_2.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "<min>-55</min><max>125</max>",
                        BuzzerOn = true,
                    });
                }

                if (sensor_3_2.SensorUnitMeasurementScale == null)
                {
                    sensor_3_2.SensorUnitMeasurementScale = new SensorUnitMeasurementScale
                    {
                        UnitMeasurementId = UnitMeasurementEnum.Celsius,
                        UnitMeasurementTypeId = UnitMeasurementTypeEnum.Temperature,
                        NumericalScalePrefixId = NumericalScalePrefixEnum.None,
                        NumericalScaleTypeId = NumericalScaleTypeEnum.Long,
                        ChartLimiterMin = 20,
                        ChartLimiterMax = 30,
                    };
                }
            }

            context.SaveChanges();

            #endregion

            #region ESPDevice

            var espDevice1MacAddress = "A0:20:A6:17:83:25";

            var espDevice1 = context.ESPDevice
                .Include(x => x.DeviceMQ)
                .Include(x => x.DeviceNTP)
                .Include(x => x.DeviceSensors)
                .SingleOrDefault(x => x.MacAddress.ToLower() == espDevice1MacAddress.ToLower());

            if (espDevice1 == null)
            {
                var timeZoneBrasilia = context.TimeZone.First(x => x.UtcTimeOffsetInSecond == -7200);

                espDevice1 = new ESPDevice
                {
                    ChipId = 1540901,
                    FlashChipId = 1458400,
                    MacAddress = espDevice1MacAddress,
                    Pin = RandonHelper.RandomString(4),
                    Label = "Device 1",
                    CreateDate = DateTime.Now,
                    DeviceMQ = new DeviceMQ
                    {
                        User = "test",
                        Password = "test",
                        ClientId = RandonHelper.RandomString(10),
                        Topic = RandonHelper.RandomString(10),
                    },
                    DeviceNTP = new DeviceNTP
                    {
                        TimeZoneId = timeZoneBrasilia.Id, // Cada hora são 3600 segundos
                        UpdateIntervalInMilliSecond = 60000,
                    },
                    DeviceSensors = new DeviceSensors
                    {
                        PublishIntervalInSeconds = 5,
                    },
                };

                context.ESPDevice.Add(espDevice1);
            }
            else
            {
                espDevice1.ChipId = 1540901;
                espDevice1.FlashChipId = 1458400;
                if (espDevice1.DeviceMQ == null)
                {
                    espDevice1.DeviceMQ = new DeviceMQ
                    {
                        User = "test",
                        Password = "test",
                        ClientId = RandonHelper.RandomString(10),
                        Topic = RandonHelper.RandomString(10),
                    };
                }
                if (espDevice1.DeviceNTP == null)
                {
                    var timeZoneBrasilia = context.TimeZone.First(x => x.UtcTimeOffsetInSecond == -7200);

                    espDevice1.DeviceNTP = new DeviceNTP
                    {
                        TimeZoneId = timeZoneBrasilia.Id, // Cada hora são 3600 segundos
                        UpdateIntervalInMilliSecond = 60000,
                    };
                }
                if (espDevice1.DeviceSensors == null)
                {
                    espDevice1.DeviceSensors = new DeviceSensors
                    {
                        PublishIntervalInSeconds = 5,
                    };
                }
            }

            context.SaveChanges();

            #endregion

            #region SensorsInDevice

            var sensorsInDevice_2_1 = new SensorsInDevice
            {
                SensorId = sensor_2_1.Id,
                DeviceSensorsId = espDevice1.DeviceSensors.Id,
                Ordination = 0,
            };

            context.SensorsInDevice.AddOrUpdate(sensorsInDevice_2_1);

            var sensorsInDevice_2_2 = new SensorsInDevice
            {
                SensorId = sensor_2_2.Id,
                DeviceSensorsId = espDevice1.DeviceSensors.Id,
                Ordination = 1,
            };

            context.SensorsInDevice.AddOrUpdate(sensorsInDevice_2_2);

            var sensorsInDevice_3_1 = new SensorsInDevice
            {
                SensorId = sensor_3_1.Id,
                DeviceSensorsId = espDevice1.DeviceSensors.Id,
                Ordination = 2,
            };

            context.SensorsInDevice.AddOrUpdate(sensorsInDevice_3_1);

            var sensorsInDevice_3_2 = new SensorsInDevice
            {
                SensorId = sensor_3_2.Id,
                DeviceSensorsId = espDevice1.DeviceSensors.Id,
                Ordination = 3,
            };

            context.SensorsInDevice.AddOrUpdate(sensorsInDevice_3_2);

            context.SaveChanges();

            #endregion

            ExecuteSettings();
        }

        private static void ExecuteActuatorType(ARTDbContext context)
        {
            var lines = GetMatrixFromFile("ActuatorType.csv");

            foreach (var line in lines)
            {
                var actuatorTypeId = (ActuatorTypeEnum)Enum.Parse(typeof(ActuatorTypeEnum), line[0]);
                var name = line[1];

                var entity = context.ActuatorType
                    .Where(x => x.Id == actuatorTypeId)
                    .SingleOrDefault();

                if (entity == null)
                {
                    entity = new ActuatorType
                    {
                        Id = actuatorTypeId,
                    };
                    context.ActuatorType.Add(entity);
                }
                entity.Name = name;

                context.SaveChanges();
            }
        }

        private static void ExecuteContinent(ARTDbContext context)
        {
            var lines = GetMatrixFromFile("Continent.csv");

            foreach (var line in lines)
            {
                var continentId = (ContinentEnum)Enum.Parse(typeof(ContinentEnum), line[0]);
                var name = line[1];

                var entity = context.Continent
                    .Where(x => x.Id == continentId)
                    .SingleOrDefault();

                if (entity == null)
                {
                    entity = new Continent
                    {
                        Id = continentId,
                    };
                    context.Continent.Add(entity);
                }
                entity.Name = name;

                context.SaveChanges();
            }
        }

        private static void ExecuteCountry(ARTDbContext context)
        {
            if (context.Country.Any()) return;

            var lines = GetMatrixFromFile("Country.csv");

            foreach (var line in lines)
            {
                var name = line[0];
                var continentId = (ContinentEnum)Enum.Parse(typeof(ContinentEnum), line[1]);

                var entity = new Country
                {
                    Name = name,
                    ContinentId = continentId,
                };

                context.Country.Add(entity);

                var numericalScaleTypes = line[2].Split(',');

                foreach (var item in numericalScaleTypes)
                {
                    var numericalScaleTypeId = (NumericalScaleTypeEnum)Enum.Parse(typeof(NumericalScaleTypeEnum), item);

                    context.NumericalScaleTypeCountry.Add(new NumericalScaleTypeCountry
                    {
                        CountryId = entity.Id,
                        NumericalScaleTypeId = numericalScaleTypeId,
                    });
                }

                context.SaveChanges();
            }
        }

        private static void ExecuteNumericalScale(ARTDbContext context)
        {
            var lines = GetMatrixFromFile("NumericalScale.csv");

            foreach (var line in lines)
            {
                var numericalScalePrefixId = (NumericalScalePrefixEnum)Enum.Parse(typeof(NumericalScalePrefixEnum), line[0]);
                var numericalScaleTypeId = (NumericalScaleTypeEnum)Enum.Parse(typeof(NumericalScaleTypeEnum), line[1]);
                var name = line[2];
                var scientificNotationBase = Convert.ToDecimal(line[3]);
                var scientificNotationExponent = Convert.ToDecimal(line[4]);

                var entity = context.NumericalScale
                   .Where(x => x.NumericalScalePrefixId == numericalScalePrefixId)
                   .Where(x => x.NumericalScaleTypeId == numericalScaleTypeId)
                   .SingleOrDefault();

                if (entity == null)
                {
                    entity = new NumericalScale
                    {
                        NumericalScalePrefixId = numericalScalePrefixId,
                        NumericalScaleTypeId = numericalScaleTypeId,
                    };
                    context.NumericalScale.Add(entity);
                }
                entity.Name = name;
                entity.ScientificNotationBase = scientificNotationBase;
                entity.ScientificNotationExponent = scientificNotationExponent;

                context.SaveChanges();
            }
        }

        private static void ExecuteNumericalScalePrefix(ARTDbContext context)
        {
            var lines = GetMatrixFromFile("NumericalScalePrefix.csv");

            foreach (var line in lines)
            {
                var numericalScalePrefixId = (NumericalScalePrefixEnum)Enum.Parse(typeof(NumericalScalePrefixEnum), line[0]);
                var name = line[1];
                var symbol = line[2];

                var entity = context.NumericalScalePrefix
                    .Where(x => x.Id == numericalScalePrefixId)
                    .SingleOrDefault();

                if (entity == null)
                {
                    entity = new NumericalScalePrefix
                    {
                        Id = numericalScalePrefixId,
                    };
                    context.NumericalScalePrefix.Add(entity);
                }
                entity.Name = name;
                entity.Symbol = symbol;

                context.SaveChanges();
            }
        }

        private static void ExecuteNumericalScaleType(ARTDbContext context)
        {
            var lines = GetMatrixFromFile("NumericalScaleType.csv");

            foreach (var line in lines)
            {
                var numericalScaleTypeId = (NumericalScaleTypeEnum)Enum.Parse(typeof(NumericalScaleTypeEnum), line[0]);
                var name = line[1];

                var entity = context.NumericalScaleType
                    .Where(x => x.Id == numericalScaleTypeId)
                    .SingleOrDefault();

                if (entity == null)
                {
                    entity = new NumericalScaleType
                    {
                        Id = numericalScaleTypeId,
                    };
                    context.NumericalScaleType.Add(entity);
                }
                entity.Name = name;

                context.SaveChanges();
            }
        }

        private static void ExecuteSensorDatasheet(ARTDbContext context)
        {
            var lines = GetMatrixFromFile("SensorDatasheet.csv");

            foreach (var line in lines)
            {
                var sensorDatasheetId = (SensorDatasheetEnum)Enum.Parse(typeof(SensorDatasheetEnum), line[0]);
                var sensorTypeId = (SensorTypeEnum)Enum.Parse(typeof(SensorTypeEnum), line[1]);

                var entity = context.SensorDatasheet
                    .Where(x => x.Id == sensorDatasheetId)
                    .Where(x => x.SensorTypeId == sensorTypeId)
                    .SingleOrDefault();

                if (entity == null)
                {
                    entity = new SensorDatasheet
                    {
                        Id = sensorDatasheetId,
                        SensorTypeId = sensorTypeId,
                    };
                    context.SensorDatasheet.Add(entity);
                }

                context.SaveChanges();
            }
        }

        private static void ExecuteSensorDatasheetUnitMeasurementScale(ARTDbContext context)
        {
            var lines = GetMatrixFromFile("SensorDatasheetUnitMeasurementScale.csv");

            foreach (var line in lines)
            {
                var sensorDatasheetId = (SensorDatasheetEnum)Enum.Parse(typeof(SensorDatasheetEnum), line[0]);
                var sensorTypeId = (SensorTypeEnum)Enum.Parse(typeof(SensorTypeEnum), line[1]);
                var unitMeasurementId = (UnitMeasurementEnum)Enum.Parse(typeof(UnitMeasurementEnum), line[2]);
                var unitMeasurementTypeId = (UnitMeasurementTypeEnum)Enum.Parse(typeof(UnitMeasurementTypeEnum), line[3]);
                var numericalScalePrefixId = (NumericalScalePrefixEnum)Enum.Parse(typeof(NumericalScalePrefixEnum), line[4]);
                var numericalScaleTypeId = (NumericalScaleTypeEnum)Enum.Parse(typeof(NumericalScaleTypeEnum), line[5]);

                var entity = context.SensorDatasheetUnitMeasurementScale
                    .Where(x => x.SensorDatasheetId == sensorDatasheetId)
                    .Where(x => x.SensorTypeId == sensorTypeId)
                    .Where(x => x.UnitMeasurementId == unitMeasurementId)
                    .Where(x => x.UnitMeasurementTypeId == unitMeasurementTypeId)
                    .Where(x => x.NumericalScalePrefixId == numericalScalePrefixId)
                    .Where(x => x.NumericalScaleTypeId == numericalScaleTypeId)
                    .SingleOrDefault();

                if (entity == null)
                {
                    entity = new SensorDatasheetUnitMeasurementScale
                    {
                        SensorDatasheetId = sensorDatasheetId,
                        SensorTypeId = sensorTypeId,
                        UnitMeasurementId = unitMeasurementId,
                        UnitMeasurementTypeId = unitMeasurementTypeId,
                        NumericalScalePrefixId = numericalScalePrefixId,
                        NumericalScaleTypeId = numericalScaleTypeId,
                    };
                    context.SensorDatasheetUnitMeasurementScale.Add(entity);
                }

                context.SaveChanges();
            }
        }

        private static void ExecuteSensorType(ARTDbContext context)
        {
            var lines = GetMatrixFromFile("SensorType.csv");

            foreach (var line in lines)
            {
                var sensorTypeId = (SensorTypeEnum)Enum.Parse(typeof(SensorTypeEnum), line[0]);
                var name = line[1];

                var entity = context.SensorType
                    .Where(x => x.Id == sensorTypeId)
                    .SingleOrDefault();

                if (entity == null)
                {
                    entity = new SensorType
                    {
                        Id = sensorTypeId,
                    };
                    context.SensorType.Add(entity);
                }
                entity.Name = name;

                context.SaveChanges();
            }
        }

        private static void ExecuteSensorUnitMeasurementDefault(ARTDbContext context)
        {
            var lines = GetMatrixFromFile("SensorUnitMeasurementDefault.csv");

            foreach (var line in lines)
            {
                var sensorDatasheetId = (SensorDatasheetEnum)Enum.Parse(typeof(SensorDatasheetEnum), line[0]);
                var sensorTypeId = (SensorTypeEnum)Enum.Parse(typeof(SensorTypeEnum), line[1]);
                var unitMeasurementId = (UnitMeasurementEnum)Enum.Parse(typeof(UnitMeasurementEnum), line[2]);
                var unitMeasurementTypeId = (UnitMeasurementTypeEnum)Enum.Parse(typeof(UnitMeasurementTypeEnum), line[3]);
                var numericalScalePrefixId = (NumericalScalePrefixEnum)Enum.Parse(typeof(NumericalScalePrefixEnum), line[4]);
                var numericalScaleTypeId = (NumericalScaleTypeEnum)Enum.Parse(typeof(NumericalScaleTypeEnum), line[5]);

                var max = Convert.ToDecimal(line[6]);
                var min = Convert.ToDecimal(line[7]);

                var entity = context.SensorUnitMeasurementDefault
                    .Where(x => x.Id == sensorDatasheetId)
                    .Where(x => x.SensorTypeId == sensorTypeId)
                    .SingleOrDefault();

                if (entity == null)
                {
                    entity = new SensorUnitMeasurementDefault
                    {
                        Id = sensorDatasheetId,
                        SensorTypeId = sensorTypeId,
                    };
                    context.SensorUnitMeasurementDefault.Add(entity);
                }

                entity.UnitMeasurementId = unitMeasurementId;
                entity.UnitMeasurementTypeId = unitMeasurementTypeId;
                entity.NumericalScalePrefixId = numericalScalePrefixId;
                entity.NumericalScaleTypeId = numericalScaleTypeId;
                entity.Max = max;
                entity.Min = min;

                context.SaveChanges();
            }
        }

        private static void ExecuteSettings()
        {
            ISettingManager settingManager = new SettingManager();

            // ChangePinIntervalInSeconds
            if (!settingManager.Exist(SettingsConstants.ChangePinIntervalInSecondsSettingsKey))
            {
                settingManager.Insert(SettingsConstants.ChangePinIntervalInSecondsSettingsKey, 20);
            }

            // BrokerHost
            if (!settingManager.Exist(MQSettingsConstants.BrokerHostSettingsKey))
            {
                // BROKER_MQTT_FREE = "broker.hivemq.com"
                settingManager.Insert(MQSettingsConstants.BrokerHostSettingsKey, "file-server.rthomaz.local");
            }

            // BrokerVirtualHost
            if (!settingManager.Exist(MQSettingsConstants.BrokerVirtualHostSettingsKey))
            {
                settingManager.Insert(MQSettingsConstants.BrokerVirtualHostSettingsKey, "/");
            }

            // BrokerPort
            if (!settingManager.Exist(MQSettingsConstants.BrokerPortSettingsKey))
            {
                settingManager.Insert(MQSettingsConstants.BrokerPortSettingsKey, 1883);
            }

            // BrokerUser
            if (!settingManager.Exist(MQSettingsConstants.BrokerUserSettingsKey))
            {
                settingManager.Insert(MQSettingsConstants.BrokerUserSettingsKey, "test");
            }

            // BrokerPwd
            if (!settingManager.Exist(MQSettingsConstants.BrokerPwdSettingsKey))
            {
                settingManager.Insert(MQSettingsConstants.BrokerPwdSettingsKey, "test");
            }

            // RpcClientTimeOutMilliSeconds
            if (!settingManager.Exist(MQSettingsConstants.RpcClientTimeOutMilliSecondsSettingsKey))
            {
                settingManager.Insert(MQSettingsConstants.RpcClientTimeOutMilliSecondsSettingsKey, 5000);
            }

            // NTPHost
            if (!settingManager.Exist(SettingsConstants.NTPHostSettingsKey))
            {
                settingManager.Insert(SettingsConstants.NTPHostSettingsKey, "pdc-server.rthomaz.local");
            }

            // NTPPort
            if (!settingManager.Exist(SettingsConstants.NTPPortSettingsKey))
            {
                settingManager.Insert(SettingsConstants.NTPPortSettingsKey, 1337);
            }

            // PublishMessageInterval
            if (!settingManager.Exist(SettingsConstants.PublishMessageIntervalSettingsKey))
            {
                settingManager.Insert(SettingsConstants.PublishMessageIntervalSettingsKey, 4000);
            }
        }

        private static void ExecuteTimeZone(ARTDbContext context)
        {
            if (!context.TimeZone.Any())
            {
                var systemTimeZones = TimeZoneInfo.GetSystemTimeZones();

                foreach (var item in systemTimeZones)
                {
                    context.TimeZone.Add(new Entities.TimeZone
                    {
                        DisplayName = item.DisplayName,
                        SupportsDaylightSavingTime = item.SupportsDaylightSavingTime,
                        UtcTimeOffsetInSecond = (int)item.BaseUtcOffset.TotalSeconds,
                    });
                }

                context.SaveChanges();
            }
        }

        private static void ExecuteUnitMeasurement(ARTDbContext context)
        {
            var lines = GetMatrixFromFile("UnitMeasurement.csv");

            foreach (var line in lines)
            {
                var unitMeasurementId = (UnitMeasurementEnum)Enum.Parse(typeof(UnitMeasurementEnum), line[0]);
                var unitMeasurementTypeId = (UnitMeasurementTypeEnum)Enum.Parse(typeof(UnitMeasurementTypeEnum), line[1]);
                var name = line[2];
                var symbol = line[3];
                var description = line[4];

                var entity = context.UnitMeasurement
                    .Where(x => x.Id == unitMeasurementId)
                    .Where(x => x.UnitMeasurementTypeId == unitMeasurementTypeId)
                    .SingleOrDefault();

                if (entity == null)
                {
                    entity = new UnitMeasurement
                    {
                        Id = unitMeasurementId,
                        UnitMeasurementTypeId = unitMeasurementTypeId,
                    };
                    context.UnitMeasurement.Add(entity);
                }
                entity.Name = name;
                entity.Symbol = symbol;
                entity.Description = description;

                context.SaveChanges();
            }
        }

        private static void ExecuteUnitMeasurementScale(ARTDbContext context)
        {
            var lines = GetMatrixFromFile("UnitMeasurementScale.csv");

            foreach (var line in lines)
            {
                var unitMeasurementId = (UnitMeasurementEnum)Enum.Parse(typeof(UnitMeasurementEnum), line[0]);
                var unitMeasurementTypeId = (UnitMeasurementTypeEnum)Enum.Parse(typeof(UnitMeasurementTypeEnum), line[1]);
                var numericalScalePrefixId = (NumericalScalePrefixEnum)Enum.Parse(typeof(NumericalScalePrefixEnum), line[2]);
                var numericalScaleTypeId = (NumericalScaleTypeEnum)Enum.Parse(typeof(NumericalScaleTypeEnum), line[3]);

                var entity = context.UnitMeasurementScale
                    .Where(x => x.UnitMeasurementId == unitMeasurementId)
                    .Where(x => x.UnitMeasurementTypeId == unitMeasurementTypeId)
                    .Where(x => x.NumericalScalePrefixId == numericalScalePrefixId)
                    .Where(x => x.NumericalScaleTypeId == numericalScaleTypeId)
                    .SingleOrDefault();

                if (entity == null)
                {
                    entity = new UnitMeasurementScale
                    {
                        UnitMeasurementId = unitMeasurementId,
                        UnitMeasurementTypeId = unitMeasurementTypeId,
                        NumericalScalePrefixId = numericalScalePrefixId,
                        NumericalScaleTypeId = numericalScaleTypeId,
                    };

                    context.UnitMeasurementScale.Add(entity);
                }

                context.SaveChanges();
            }
        }

        private static void ExecuteUnitMeasurementType(ARTDbContext context)
        {
            var lines = GetMatrixFromFile("UnitMeasurementType.csv");

            foreach (var line in lines)
            {
                var unitMeasurementTypeId = (UnitMeasurementTypeEnum)Enum.Parse(typeof(UnitMeasurementTypeEnum), line[0]);
                var name = line[1];

                var entity = context.UnitMeasurementType
                    .Where(x => x.Id == unitMeasurementTypeId)
                    .SingleOrDefault();

                if (entity == null)
                {
                    entity = new UnitMeasurementType
                    {
                        Id = unitMeasurementTypeId,
                        Name = name,
                    };
                    context.UnitMeasurementType.Add(entity);
                }
                entity.Name = name;

                context.SaveChanges();
            }
        }

        private static IEnumerable<string[]> GetMatrixFromFile(string fileName)
        {
            var currentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.FullName;
            var directoryBase = Path.Combine(currentDirectory, "InitialFiles");
            string filePath = Path.Combine(directoryBase, fileName);
            var encoding = Encoding.GetEncoding(CultureInfo.GetCultureInfo("pt-BR").TextInfo.ANSICodePage);
            var lines = File.ReadAllLines(filePath, encoding).Select(a => a.Split(';'));
            return lines;
        }

        #endregion Methods
    }
}