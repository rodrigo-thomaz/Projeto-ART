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
    using ART.Domotica.Repository.Entities;
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

            #region SensorType

            var genericSensorType = context.SensorType.FirstOrDefault(x => x.Id == SensorTypeEnum.Generic);
            var luminositySensorType = context.SensorType.FirstOrDefault(x => x.Id == SensorTypeEnum.Luminosity);
            var motionSensorType = context.SensorType.FirstOrDefault(x => x.Id == SensorTypeEnum.Motion);
            var pressureSensorType = context.SensorType.FirstOrDefault(x => x.Id == SensorTypeEnum.Pressure);
            var proximityDistanceSensorType = context.SensorType.FirstOrDefault(x => x.Id == SensorTypeEnum.ProximityDistance);
            var temperatureSensorType = context.SensorType.FirstOrDefault(x => x.Id == SensorTypeEnum.Temperature);

            if (genericSensorType == null)
            {
                genericSensorType = new SensorType
                {
                    Id = SensorTypeEnum.Generic,
                    Name = "Genérico",
                };
                context.SensorType.Add(genericSensorType);
            }

            if (luminositySensorType == null)
            {
                luminositySensorType = new SensorType
                {
                    Id = SensorTypeEnum.Luminosity,
                    Name = "Luminosidade",
                };
                context.SensorType.Add(luminositySensorType);
            }

            if (motionSensorType == null)
            {
                motionSensorType = new SensorType
                {
                    Id = SensorTypeEnum.Motion,
                    Name = "Movimento",
                };
                context.SensorType.Add(motionSensorType);
            }

            if (pressureSensorType == null)
            {
                pressureSensorType = new SensorType
                {
                    Id = SensorTypeEnum.Pressure,
                    Name = "Pressão",
                };
                context.SensorType.Add(pressureSensorType);
            }

            if (proximityDistanceSensorType == null)
            {
                proximityDistanceSensorType = new SensorType
                {
                    Id = SensorTypeEnum.ProximityDistance,
                    Name = "Proximidade/Distância",
                };
                context.SensorType.Add(proximityDistanceSensorType);
            }

            if (temperatureSensorType == null)
            {
                temperatureSensorType = new SensorType
                {
                    Id = SensorTypeEnum.Temperature,
                    Name = "Temperatura",
                };
                context.SensorType.Add(temperatureSensorType);
            }

            #endregion

            #region SensorDatasheet

            var sensorDatasheetTemperatureDS18B20 = context.SensorDatasheet.FirstOrDefault(x => x.Id == SensorDatasheetEnum.Temperature_DS18B20);
            var sensorDatasheetUltrasonicHCSR04 = context.SensorDatasheet.FirstOrDefault(x => x.Id == SensorDatasheetEnum.Ultrasonic_HCSR04);

            if (sensorDatasheetTemperatureDS18B20 == null)
            {
                sensorDatasheetTemperatureDS18B20 = new SensorDatasheet
                {
                    Id = SensorDatasheetEnum.Temperature_DS18B20,
                    SensorTypeId = temperatureSensorType.Id,
                };
                context.SensorDatasheet.Add(sensorDatasheetTemperatureDS18B20);
            }

            if (sensorDatasheetUltrasonicHCSR04 == null)
            {
                sensorDatasheetUltrasonicHCSR04 = new SensorDatasheet
                {
                    Id = SensorDatasheetEnum.Ultrasonic_HCSR04,
                    SensorTypeId = proximityDistanceSensorType.Id,
                };
                context.SensorDatasheet.Add(sensorDatasheetUltrasonicHCSR04);
            }

            #endregion

            #region SensorUnitMeasurementDefault

            var sensorUnitMeasurementDefaultTemperatureDS18B20 = context.SensorUnitMeasurementDefault.FirstOrDefault(x => x.Id == SensorDatasheetEnum.Temperature_DS18B20);
            var sensorUnitMeasurementDefaultUltrasonicHCSR04 = context.SensorUnitMeasurementDefault.FirstOrDefault(x => x.Id == SensorDatasheetEnum.Ultrasonic_HCSR04);

            if (sensorUnitMeasurementDefaultTemperatureDS18B20 == null)
            {
                sensorUnitMeasurementDefaultTemperatureDS18B20 = new SensorUnitMeasurementDefault
                {
                    Id = SensorDatasheetEnum.Temperature_DS18B20,
                    SensorTypeId = temperatureSensorType.Id,
                    UnitMeasurementId = UnitMeasurementEnum.Celsius,
                    UnitMeasurementTypeId = UnitMeasurementTypeEnum.Temperature,
                    Max = 125M,
                    Min = -55M,
                };
                context.SensorUnitMeasurementDefault.Add(sensorUnitMeasurementDefaultTemperatureDS18B20);
            }

            if (sensorUnitMeasurementDefaultUltrasonicHCSR04 == null)
            {
                sensorUnitMeasurementDefaultUltrasonicHCSR04 = new SensorUnitMeasurementDefault
                {
                    Id = SensorDatasheetEnum.Ultrasonic_HCSR04,
                    SensorTypeId = proximityDistanceSensorType.Id,
                    UnitMeasurementId = UnitMeasurementEnum.Meter,
                    UnitMeasurementTypeId = UnitMeasurementTypeEnum.Length,
                    Max = 2.3M,
                    Min = 0.2M,
                };
                context.SensorUnitMeasurementDefault.Add(sensorUnitMeasurementDefaultUltrasonicHCSR04);
            }

            #endregion

            #region ActuatorType

            var genericActuatorType = context.ActuatorType.FirstOrDefault(x => x.Id == ActuatorTypeEnum.Generic);
            var lightActuatorType = context.ActuatorType.FirstOrDefault(x => x.Id == ActuatorTypeEnum.Light);
            var motorActuatorType = context.ActuatorType.FirstOrDefault(x => x.Id == ActuatorTypeEnum.Motor);
            var relayActuatorType = context.ActuatorType.FirstOrDefault(x => x.Id == ActuatorTypeEnum.Relay);
            var valveActuatorType = context.ActuatorType.FirstOrDefault(x => x.Id == ActuatorTypeEnum.Valve);

            if (genericActuatorType == null)
            {
                genericActuatorType = new ActuatorType
                {
                    Id = ActuatorTypeEnum.Generic,
                    Name = "Genérico",
                };
                context.ActuatorType.Add(genericActuatorType);
            }

            if (lightActuatorType == null)
            {
                lightActuatorType = new ActuatorType
                {
                    Id = ActuatorTypeEnum.Light,
                    Name = "Luz",
                };
                context.ActuatorType.Add(lightActuatorType);
            }

            if (motorActuatorType == null)
            {
                motorActuatorType = new ActuatorType
                {
                    Id = ActuatorTypeEnum.Motor,
                    Name = "Motor",
                };
                context.ActuatorType.Add(motorActuatorType);
            }

            if (relayActuatorType == null)
            {
                relayActuatorType = new ActuatorType
                {
                    Id = ActuatorTypeEnum.Relay,
                    Name = "Relê",
                };
                context.ActuatorType.Add(relayActuatorType);
            }

            if (valveActuatorType == null)
            {
                valveActuatorType = new ActuatorType
                {
                    Id = ActuatorTypeEnum.Valve,
                    Name = "Válvula",
                };
                context.ActuatorType.Add(valveActuatorType);
            }

            #endregion

            #region SensorRange

            var sensorRange1 = context.SensorRange.SingleOrDefault(x => x.Id == 1);

            if (sensorRange1 == null)
            {
                sensorRange1 = new SensorRange { Id = 1 };
                context.SensorRange.Add(sensorRange1);
            }

            sensorRange1.Min = -55;
            sensorRange1.Max = 125;

            context.SaveChanges();

            #endregion

            #region DSFamilyTempSensorResolutions

            var dsFamilyTempSensorResolution9 = context.DSFamilyTempSensorResolution.SingleOrDefault(x => x.Id == 9);
            var dsFamilyTempSensorResolution10 = context.DSFamilyTempSensorResolution.SingleOrDefault(x => x.Id == 10);
            var dsFamilyTempSensorResolution11 = context.DSFamilyTempSensorResolution.SingleOrDefault(x => x.Id == 11);
            var dsFamilyTempSensorResolution12 = context.DSFamilyTempSensorResolution.SingleOrDefault(x => x.Id == 12);

            if (dsFamilyTempSensorResolution9 == null)
            {
                dsFamilyTempSensorResolution9 = new DSFamilyTempSensorResolution { Id = 9 };
                context.DSFamilyTempSensorResolution.Add(dsFamilyTempSensorResolution9);
            }

            if (dsFamilyTempSensorResolution10 == null)
            {
                dsFamilyTempSensorResolution10 = new DSFamilyTempSensorResolution { Id = 10 };
                context.DSFamilyTempSensorResolution.Add(dsFamilyTempSensorResolution10);
            }

            if (dsFamilyTempSensorResolution11 == null)
            {
                dsFamilyTempSensorResolution11 = new DSFamilyTempSensorResolution { Id = 11 };
                context.DSFamilyTempSensorResolution.Add(dsFamilyTempSensorResolution11);
            }

            if (dsFamilyTempSensorResolution12 == null)
            {
                dsFamilyTempSensorResolution12 = new DSFamilyTempSensorResolution { Id = 12 };
                context.DSFamilyTempSensorResolution.Add(dsFamilyTempSensorResolution12);
            }

            dsFamilyTempSensorResolution9.Name = "9 bits";
            dsFamilyTempSensorResolution9.Bits = 9;
            dsFamilyTempSensorResolution9.Resolution = 0.5M;
            dsFamilyTempSensorResolution9.ConversionTime = 93.75M;
            dsFamilyTempSensorResolution9.DecimalPlaces = 1;
            dsFamilyTempSensorResolution9.Description = "Resolução de 9 bits";

            dsFamilyTempSensorResolution10.Name = "10 bits";
            dsFamilyTempSensorResolution10.Bits = 10;
            dsFamilyTempSensorResolution10.Resolution = 0.25M;
            dsFamilyTempSensorResolution10.ConversionTime = 187.5M;
            dsFamilyTempSensorResolution10.DecimalPlaces = 2;
            dsFamilyTempSensorResolution10.Description = "Resolução de 10 bits";

            dsFamilyTempSensorResolution11.Name = "11 bits";
            dsFamilyTempSensorResolution11.Bits = 11;
            dsFamilyTempSensorResolution11.Resolution = 0.125M;
            dsFamilyTempSensorResolution11.ConversionTime = 375;
            dsFamilyTempSensorResolution11.DecimalPlaces = 3;
            dsFamilyTempSensorResolution11.Description = "Resolução de 11 bits";

            dsFamilyTempSensorResolution12.Name = "12 bits";
            dsFamilyTempSensorResolution12.Bits = 12;
            dsFamilyTempSensorResolution12.Resolution = 0.0625M;
            dsFamilyTempSensorResolution12.ConversionTime = 750;
            dsFamilyTempSensorResolution12.DecimalPlaces = 4;
            dsFamilyTempSensorResolution12.Description = "Resolução de 12 bits";

            context.SaveChanges();

            #endregion

            #region DSFamilyTempSensor

            var sensor_1_Address = "28fff62293165b0";
            var sensor_2_1_Address = "40:255:231:109:162:22:3:211";
            var sensor_2_2_Address = "40:255:254:101:147:22:4:182";
            var sensor_3_1_Address = "40:255:192:95:147:22:4:195";
            var sensor_3_2_Address = "40:255:113:95:147:22:4:65";

            var sensor_1 = context.DSFamilyTempSensor.Include(x => x.SensorChartLimiter).Include(x => x.SensorTriggers).SingleOrDefault(x => x.DeviceAddress.ToLower() == sensor_1_Address.ToLower());
            var sensor_2_1 = context.DSFamilyTempSensor.Include(x => x.SensorChartLimiter).Include(x => x.SensorTriggers).SingleOrDefault(x => x.DeviceAddress.ToLower() == sensor_2_1_Address.ToLower());
            var sensor_2_2 = context.DSFamilyTempSensor.Include(x => x.SensorChartLimiter).Include(x => x.SensorTriggers).SingleOrDefault(x => x.DeviceAddress.ToLower() == sensor_2_2_Address.ToLower());
            var sensor_3_1 = context.DSFamilyTempSensor.Include(x => x.SensorChartLimiter).Include(x => x.SensorTriggers).SingleOrDefault(x => x.DeviceAddress.ToLower() == sensor_3_1_Address.ToLower());
            var sensor_3_2 = context.DSFamilyTempSensor.Include(x => x.SensorChartLimiter).Include(x => x.SensorTriggers).SingleOrDefault(x => x.DeviceAddress.ToLower() == sensor_3_2_Address.ToLower());

            if (sensor_1 == null)
            {
                sensor_1 = new DSFamilyTempSensor
                {
                    DeviceAddress = sensor_1_Address,
                    Family = "DS18B20",
                    SensorRangeId = sensorRange1.Id,
                    UnitMeasurementId = UnitMeasurementEnum.Celsius,
                    DSFamilyTempSensorResolutionId = dsFamilyTempSensorResolution9.Id,
                    DSFamilyTempSensorResolution = dsFamilyTempSensorResolution9,
                    Label = "Sensor 1",
                    SensorTriggers = new List<SensorTrigger>
                    {
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "-55",
                            BuzzerOn = true,
                        },
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "125",
                            BuzzerOn = true,
                        },
                    },
                    SensorChartLimiter = new SensorChartLimiter
                    {
                        Min = 20,
                        Max = 30,
                    },
                    CreateDate = DateTime.Now,
                };
                context.DSFamilyTempSensor.Add(sensor_1);
            }
            else
            {
                sensor_1.Family = "DS18B20";
                sensor_1.DeviceAddress = sensor_1_Address;

                if (!sensor_1.SensorTriggers.Any())
                {
                    sensor_1.SensorTriggers = new List<SensorTrigger>();

                    sensor_1.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "-55",
                        BuzzerOn = true,
                    });
                    sensor_1.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "125",
                        BuzzerOn = true,
                    });
                }

                if (sensor_1.SensorChartLimiter == null)
                {
                    sensor_1.SensorChartLimiter = new SensorChartLimiter
                    {
                        Min = 20,
                        Max = 30,
                    };
                }

            }

            if (sensor_2_1 == null)
            {
                sensor_2_1 = new DSFamilyTempSensor
                {
                    DeviceAddress = sensor_2_1_Address,
                    Family = "DS18B20",
                    SensorRangeId = sensorRange1.Id,
                    UnitMeasurementId = UnitMeasurementEnum.Fahrenheit,
                    DSFamilyTempSensorResolutionId = dsFamilyTempSensorResolution11.Id,
                    DSFamilyTempSensorResolution = dsFamilyTempSensorResolution11,
                    Label = "Sensor 1",
                    SensorTriggers = new List<SensorTrigger>
                    {
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "-55",
                            BuzzerOn = true,
                        },
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "125",
                            BuzzerOn = true,
                        },
                    },
                    SensorChartLimiter = new SensorChartLimiter
                    {
                        Min = 20,
                        Max = 30,
                    },
                    CreateDate = DateTime.Now,
                };
                context.DSFamilyTempSensor.Add(sensor_2_1);
            }
            else
            {
                sensor_2_1.Family = "DS18B20";
                sensor_2_1.DeviceAddress = sensor_2_1_Address;

                if (!sensor_2_1.SensorTriggers.Any())
                {
                    sensor_2_1.SensorTriggers = new List<SensorTrigger>();

                    sensor_2_1.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "-55",
                        BuzzerOn = true,
                    });
                    sensor_2_1.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "125",
                        BuzzerOn = true,
                    });
                }

                if (sensor_2_1.SensorChartLimiter == null)
                {
                    sensor_2_1.SensorChartLimiter = new SensorChartLimiter
                    {
                        Min = 20,
                        Max = 30,
                    };
                }
            }

            if (sensor_2_2 == null)
            {
                sensor_2_2 = new DSFamilyTempSensor
                {
                    DeviceAddress = sensor_2_2_Address,
                    Family = "DS18B20",
                    SensorRangeId = sensorRange1.Id,
                    UnitMeasurementId = UnitMeasurementEnum.Fahrenheit,
                    DSFamilyTempSensorResolutionId = dsFamilyTempSensorResolution11.Id,
                    DSFamilyTempSensorResolution = dsFamilyTempSensorResolution11,
                    Label = "Sensor 2",
                    SensorTriggers = new List<SensorTrigger>
                    {
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "-55",
                            BuzzerOn = true,
                        },
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "125",
                            BuzzerOn = true,
                        },
                    },
                    SensorChartLimiter = new SensorChartLimiter
                    {
                        Min = 20,
                        Max = 30,
                    },
                    CreateDate = DateTime.Now,
                };
                context.DSFamilyTempSensor.Add(sensor_2_2);
            }
            else
            {
                sensor_2_2.Family = "DS18B20";
                sensor_2_2.DeviceAddress = sensor_2_2_Address;

                if (!sensor_2_2.SensorTriggers.Any())
                {
                    sensor_2_2.SensorTriggers = new List<SensorTrigger>();

                    sensor_2_2.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "-55",
                        BuzzerOn = true,
                    });
                    sensor_2_2.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "125",
                        BuzzerOn = true,
                    });
                }

                if (sensor_2_2.SensorChartLimiter == null)
                {
                    sensor_2_2.SensorChartLimiter = new SensorChartLimiter
                    {
                        Min = 20,
                        Max = 30,
                    };
                }
            }

            if (sensor_3_1 == null)
            {
                sensor_3_1 = new DSFamilyTempSensor
                {
                    DeviceAddress = sensor_3_1_Address,
                    Family = "DS18B20",
                    SensorRangeId = sensorRange1.Id,
                    UnitMeasurementId = UnitMeasurementEnum.Fahrenheit,
                    DSFamilyTempSensorResolutionId = dsFamilyTempSensorResolution11.Id,
                    DSFamilyTempSensorResolution = dsFamilyTempSensorResolution11,
                    Label = "Sensor 3",
                    SensorTriggers = new List<SensorTrigger>
                    {
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "-55",
                            BuzzerOn = true,
                        },
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "125",
                            BuzzerOn = true,
                        },
                    },
                    SensorChartLimiter = new SensorChartLimiter
                    {
                        Min = 20,
                        Max = 30,
                    },
                    CreateDate = DateTime.Now,
                };
                context.DSFamilyTempSensor.Add(sensor_3_1);
            }
            else
            {
                sensor_3_1.Family = "DS18B20";
                sensor_3_1.DeviceAddress = sensor_3_1_Address;

                if (!sensor_3_1.SensorTriggers.Any())
                {
                    sensor_3_1.SensorTriggers = new List<SensorTrigger>();

                    sensor_3_1.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "-55",
                        BuzzerOn = true,
                    });
                    sensor_3_1.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "125",
                        BuzzerOn = true,
                    });
                }

                if (sensor_3_1.SensorChartLimiter == null)
                {
                    sensor_3_1.SensorChartLimiter = new SensorChartLimiter
                    {
                        Min = 20,
                        Max = 30,
                    };
                }
            }

            if (sensor_3_2 == null)
            {
                sensor_3_2 = new DSFamilyTempSensor
                {
                    DeviceAddress = sensor_3_2_Address,
                    Family = "DS18B20",
                    SensorRangeId = sensorRange1.Id,
                    UnitMeasurementId = UnitMeasurementEnum.Fahrenheit,
                    DSFamilyTempSensorResolutionId = dsFamilyTempSensorResolution11.Id,
                    DSFamilyTempSensorResolution = dsFamilyTempSensorResolution11,
                    Label = "Sensor 4",
                    SensorTriggers = new List<SensorTrigger>
                    {
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "-55",
                            BuzzerOn = true,
                        },
                        new SensorTrigger
                        {
                            TriggerOn = true,
                            TriggerValue = "125",
                            BuzzerOn = true,
                        },
                    },
                    SensorChartLimiter = new SensorChartLimiter
                    {
                        Min = 20,
                        Max = 30,
                    },
                    CreateDate = DateTime.Now,
                };
                context.DSFamilyTempSensor.Add(sensor_3_2);
            }
            else
            {
                sensor_3_2.Family = "DS18B20";
                sensor_3_2.DeviceAddress = sensor_3_2_Address;

                if (!sensor_3_2.SensorTriggers.Any())
                {
                    sensor_3_2.SensorTriggers = new List<SensorTrigger>();

                    sensor_3_2.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "-55",
                        BuzzerOn = true,
                    });
                    sensor_3_2.SensorTriggers.Add(new SensorTrigger
                    {
                        TriggerOn = true,
                        TriggerValue = "125",
                        BuzzerOn = true,
                    });
                }

                if (sensor_3_2.SensorChartLimiter == null)
                {
                    sensor_3_2.SensorChartLimiter = new SensorChartLimiter
                    {
                        Min = 20,
                        Max = 30,
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
            }

            context.SaveChanges();

            #endregion

            #region SensorsInDevice

            var sensorsInDevice_2_1 = new SensorsInDevice
            {
                SensorId = sensor_2_1.Id,
                Sensor = sensor_2_1,
                DeviceBaseId = espDevice1.Id,
                DeviceBase = espDevice1,
            };

            context.SensorsInDevice.AddOrUpdate(sensorsInDevice_2_1);

            var sensorsInDevice_2_2 = new SensorsInDevice
            {
                SensorId = sensor_2_2.Id,
                Sensor = sensor_2_2,
                DeviceBaseId = espDevice1.Id,
                DeviceBase = espDevice1,
            };

            context.SensorsInDevice.AddOrUpdate(sensorsInDevice_2_2);

            var sensorsInDevice_3_1 = new SensorsInDevice
            {
                SensorId = sensor_3_1.Id,
                Sensor = sensor_3_1,
                DeviceBaseId = espDevice1.Id,
                DeviceBase = espDevice1,
            };

            context.SensorsInDevice.AddOrUpdate(sensorsInDevice_3_1);

            var sensorsInDevice_3_2 = new SensorsInDevice
            {
                SensorId = sensor_3_2.Id,
                Sensor = sensor_3_2,
                DeviceBaseId = espDevice1.Id,
                DeviceBase = espDevice1,
            };

            context.SensorsInDevice.AddOrUpdate(sensorsInDevice_3_2);

            context.SaveChanges();

            #endregion

            ExecuteSettings();
        }

        private static void ExecuteContinent(ARTDbContext context)
        {
            var lines = GetMatrixFromFile("Continent.csv");

            foreach (var line in lines)
            {
                var continentId = (ContinentEnum)Enum.Parse(typeof(ContinentEnum), line[0]);
                var name = line[1];

                var entity = context.Continent.SingleOrDefault(x => x.Id == continentId);

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

                var entity = context.NumericalScalePrefix.SingleOrDefault(x => x.Id == numericalScalePrefixId);

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

                var entity = context.NumericalScaleType.SingleOrDefault(x => x.Id == numericalScaleTypeId);

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

                var entity = context.UnitMeasurement.SingleOrDefault(x => x.Id == unitMeasurementId);

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

        private static void ExecuteUnitMeasurementType(ARTDbContext context)
        {
            var lines = GetMatrixFromFile("UnitMeasurementType.csv");

            foreach (var line in lines)
            {
                var unitMeasurementTypeId = (UnitMeasurementTypeEnum)Enum.Parse(typeof(UnitMeasurementTypeEnum), line[0]);
                var name = line[1];

                var entity = context.UnitMeasurementType.SingleOrDefault(x => x.Id == unitMeasurementTypeId);

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