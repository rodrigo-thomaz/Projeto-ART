namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
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
            #region TimeZone

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

            #endregion

            #region TempSensorRange

            var tempSensorRange1 = context.TempSensorRange.SingleOrDefault(x => x.Id == 1);

            if (tempSensorRange1 == null)
            {
                tempSensorRange1 = new TempSensorRange { Id = 1 };
                context.TempSensorRange.Add(tempSensorRange1);
            }

            tempSensorRange1.Min = -55;
            tempSensorRange1.Max = 125;

            context.SaveChanges();

            #endregion

            #region UnitOfMeasurementType

            // https://pt.wikipedia.org/wiki/Unidade_de_medida

            //Area = 1,               - Unidades de área
            //Capacity = 2,           - Unidades de capacidade
            //Length = 3,             - Unidades de comprimento
            //Density = 4,            - Unidades de densidade
            //Energy = 5,             - Unidades de energia
            //Force = 6,              - Unidades de força
            //Mass = 7,               - Unidades de massa
            //SpecificWeight = 8,     - Unidades de peso específico
            //Potency = 9,            - Unidades de potência
            //Pressure = 10,          - Unidades de pressão
            //Temperature = 11,       - Unidades de temperatura
            //Time = 12,              - Unidades de tempo
            //Velocity = 13,          - Unidades de velocidade
            //Viscosity = 14,         - Unidades de viscosidade
            //Volume = 15,            - Unidades de volume
            //Electrical = 16,        - Unidades elétrica

            var areaUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Area);
            var capacityUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Capacity);
            var lengthUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Length);
            var densityUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Density);
            var energyUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Energy);
            var forceUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Force);
            var massUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Mass);
            var specificWeightUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.SpecificWeight);
            var potencyUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Potency);
            var pressureUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Pressure);
            var temperatureUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Temperature);
            var timeUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Time);
            var velocityUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Velocity);
            var viscosityUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Viscosity);
            var volumeUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Volume);
            var electricalUnitOfMeasurementType = context.UnitOfMeasurementType.FirstOrDefault(x => x.Id == UnitOfMeasurementTypeEnum.Electrical);

            if (areaUnitOfMeasurementType == null)
            {
                areaUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Area,
                    Name = "Área",
                };
                context.UnitOfMeasurementType.Add(areaUnitOfMeasurementType);
            }

            if (capacityUnitOfMeasurementType == null)
            {
                capacityUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Capacity,
                    Name = "Capacidade",
                };
                context.UnitOfMeasurementType.Add(capacityUnitOfMeasurementType);
            }

            if (lengthUnitOfMeasurementType == null)
            {
                lengthUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Length,
                    Name = "Comprimento",
                };
                context.UnitOfMeasurementType.Add(lengthUnitOfMeasurementType);
            }

            if (densityUnitOfMeasurementType == null)
            {
                densityUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Density,
                    Name = "Densidade",
                };
                context.UnitOfMeasurementType.Add(densityUnitOfMeasurementType);
            }

            if (energyUnitOfMeasurementType == null)
            {
                energyUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Energy,
                    Name = "Energia",
                };
                context.UnitOfMeasurementType.Add(energyUnitOfMeasurementType);
            }

            if (forceUnitOfMeasurementType == null)
            {
                forceUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Force,
                    Name = "Força",
                };
                context.UnitOfMeasurementType.Add(forceUnitOfMeasurementType);
            }

            if (massUnitOfMeasurementType == null)
            {
                massUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Mass,
                    Name = "Massa",
                };
                context.UnitOfMeasurementType.Add(massUnitOfMeasurementType);
            }

            if (specificWeightUnitOfMeasurementType == null)
            {
                specificWeightUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.SpecificWeight,
                    Name = "Peso específico",
                };
                context.UnitOfMeasurementType.Add(specificWeightUnitOfMeasurementType);
            }

            if (potencyUnitOfMeasurementType == null)
            {
                potencyUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Potency,
                    Name = "Potência",
                };
                context.UnitOfMeasurementType.Add(potencyUnitOfMeasurementType);
            }

            if (pressureUnitOfMeasurementType == null)
            {
                pressureUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Pressure,
                    Name = "Pressão",
                };
                context.UnitOfMeasurementType.Add(pressureUnitOfMeasurementType);
            }

            if (temperatureUnitOfMeasurementType == null)
            {
                temperatureUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Temperature,
                    Name = "Temperatura",
                };
                context.UnitOfMeasurementType.Add(temperatureUnitOfMeasurementType);
            }

            if (timeUnitOfMeasurementType == null)
            {
                timeUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Time,
                    Name = "Tempo",
                };
                context.UnitOfMeasurementType.Add(timeUnitOfMeasurementType);
            }

            if (velocityUnitOfMeasurementType == null)
            {
                velocityUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Velocity,
                    Name = "Velocidade",
                };
                context.UnitOfMeasurementType.Add(velocityUnitOfMeasurementType);
            }

            if (viscosityUnitOfMeasurementType == null)
            {
                viscosityUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Viscosity,
                    Name = "Viscosidade",
                };
                context.UnitOfMeasurementType.Add(viscosityUnitOfMeasurementType);
            }

            if (volumeUnitOfMeasurementType == null)
            {
                volumeUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Volume,
                    Name = "Volume",
                };
                context.UnitOfMeasurementType.Add(volumeUnitOfMeasurementType);
            }

            if (electricalUnitOfMeasurementType == null)
            {
                electricalUnitOfMeasurementType = new UnitOfMeasurementType
                {
                    Id = UnitOfMeasurementTypeEnum.Electrical,
                    Name = "Elétrica",
                };
                context.UnitOfMeasurementType.Add(electricalUnitOfMeasurementType);
            }

            context.SaveChanges();

            #endregion

            #region UnitOfMeasurement

            var celsiusDescription = new StringBuilder();

            celsiusDescription.AppendLine("A escala de grau Celsius(símbolo: °C) é uma escala termométrica, do sistema métrico[1], usada na maioria dos países do mundo.Teve origem a partir do modelo proposto pelo astrônomo sueco Anders Celsius(1701 - 1744), inicialmente denominado escala centígrada(Grau centígrado).");
            celsiusDescription.AppendLine("Esta escala é baseada nos pontos de fusão e ebulição da água, em condição atmosférica padrão, aos quais são atribuídos os valores de 0 °C e 100 °C, respectivamente[2].Devido a esta divisão centesimal, se deu a antiga nomenclatura grau centígrado(cem partes/ graus) que, em 1948, durante a 9ª Conferência Geral de Pesos e Medidas(CR 64), teve seu nome oficialmente modificado para grau Celsius, em reconhecimento ao trabalho de Anders Celsius e para fim de desambiguação com o prefixo centi do SI.");
            celsiusDescription.AppendLine("Enquanto que os valores de congelação e evaporação da água estão aproximadamente corretos, a definição original não é apropriada como um padrão formal: ela depende da definição de pressão atmosférica padrão, que por sua vez depende da própria definição de temperatura.A definição oficial atual de grau Celsius define 0,01 °C como o ponto triplo da água, e 1 grau Celsius como sendo 1 / 273,16 da diferença de temperatura entre o ponto triplo da água e o zero absoluto. Esta definição garante que 1 grau Celsius apresenta a mesma variação de temperatura que 1 kelvin.");

            var celsiusUnitOfMeasurement = context.UnitOfMeasurement.FirstOrDefault(x => x.Id == UnitOfMeasurementEnum.Celsius);

            if (celsiusUnitOfMeasurement == null)
            {
                celsiusUnitOfMeasurement = new UnitOfMeasurement
                {
                    Id = UnitOfMeasurementEnum.Celsius,
                    UnitOfMeasurementTypeId = temperatureUnitOfMeasurementType.Id,
                };
                context.UnitOfMeasurement.Add(celsiusUnitOfMeasurement);
            }

            celsiusUnitOfMeasurement.Name = "Celsius";
            celsiusUnitOfMeasurement.Symbol = "C";
            celsiusUnitOfMeasurement.Description = celsiusDescription.ToString();

            var fahrenheitDescription = new StringBuilder();

            fahrenheitDescription.AppendLine("O grau fahrenheit(símbolo: °F) é uma escala de temperatura proposta por Daniel Gabriel Fahrenheit em 1724.Nesta escala:");
            fahrenheitDescription.AppendLine("- o ponto de fusão da água(0 °C) é de 32 °F.");
            fahrenheitDescription.AppendLine("- o ponto de ebulição da água(100 °C) é de 212 °F.");
            fahrenheitDescription.AppendLine("Uma diferença de 1,8 °F é igual a uma diferença de 1 °C.");
            fahrenheitDescription.AppendLine("Esta escala foi utilizada principalmente pelos países que foram colonizados pelos britânicos, mas seu uso atualmente se restringe a poucos países de língua inglesa, como os Estados Unidos e Belize. E também, muito utilizada com o povo grego, para medir a temperatura de um corpo.Jakelinneh Devocerg, mulher francesa que criou a teoria 'Fahrenheit Devocerg' que para passar de celsius para fahrenheit se usa sempre 1,8.Ex: f = 137 * e c = 20 * f + 137 - 20 + c.1,8 fc = 117.1,8 = 1,20202020");
            fahrenheitDescription.AppendLine("Para uso científico, há uma escala de temperatura, chamada de Rankine, que leva o marco zero de sua escala ao zero absoluto e possui a mesma variação da escala fahrenheit, existindo, portanto, correlação entre a escala de Rankine e grau fahrenheit do mesmo modo que existe correlação das escalas kelvin e grau Celsius.");

            var fahrenheitUnitOfMeasurement = context.UnitOfMeasurement.SingleOrDefault(x => x.Id == UnitOfMeasurementEnum.Fahrenheit);

            if (fahrenheitUnitOfMeasurement == null)
            {
                fahrenheitUnitOfMeasurement = new UnitOfMeasurement
                {
                    Id = UnitOfMeasurementEnum.Fahrenheit,
                    UnitOfMeasurementTypeId = temperatureUnitOfMeasurementType.Id,
                };
                context.UnitOfMeasurement.Add(fahrenheitUnitOfMeasurement);
            }

            fahrenheitUnitOfMeasurement.Name = "Fahrenheit";
            fahrenheitUnitOfMeasurement.Symbol = "F";
            fahrenheitUnitOfMeasurement.Description = fahrenheitDescription.ToString();

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

            var sensor_1 = context.DSFamilyTempSensor.SingleOrDefault(x => x.DeviceAddress.ToLower() == sensor_1_Address.ToLower());
            var sensor_2_1 = context.DSFamilyTempSensor.SingleOrDefault(x => x.DeviceAddress.ToLower() == sensor_2_1_Address.ToLower());
            var sensor_2_2 = context.DSFamilyTempSensor.SingleOrDefault(x => x.DeviceAddress.ToLower() == sensor_2_2_Address.ToLower());
            var sensor_3_1 = context.DSFamilyTempSensor.SingleOrDefault(x => x.DeviceAddress.ToLower() == sensor_3_1_Address.ToLower());
            var sensor_3_2 = context.DSFamilyTempSensor.SingleOrDefault(x => x.DeviceAddress.ToLower() == sensor_3_2_Address.ToLower());

            if (sensor_1 == null)
            {
                sensor_1 = new DSFamilyTempSensor
                {
                    DeviceAddress = sensor_1_Address,
                    Family = "DS18B20",
                    TempSensorRangeId = tempSensorRange1.Id,
                    UnitOfMeasurementId = celsiusUnitOfMeasurement.Id,
                    UnitOfMeasurement = celsiusUnitOfMeasurement,
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
                    LowAlarm = new TempSensorAlarm
                    {
                        AlarmOn = false,
                        AlarmCelsius = -55,
                        AlarmBuzzerOn = false,
                    },
                    HighAlarm = new TempSensorAlarm
                    {
                        AlarmOn = false,
                        AlarmCelsius = 125,
                        AlarmBuzzerOn = false,
                    },
                    LowChartLimiterCelsius = 20,
                    HighChartLimiterCelsius = 30,
                    CreateDate = DateTime.Now,
                };
                context.DSFamilyTempSensor.Add(sensor_1);
            }
            else
            {
                sensor_1.Family = "DS18B20";
                sensor_1.DeviceAddress = sensor_1_Address;

                if(sensor_1.SensorTriggers == null)
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
            }

            if (sensor_2_1 == null)
            {
                sensor_2_1 = new DSFamilyTempSensor
                {
                    DeviceAddress = sensor_2_1_Address,
                    Family = "DS18B20",
                    TempSensorRangeId = tempSensorRange1.Id,
                    UnitOfMeasurementId = fahrenheitUnitOfMeasurement.Id,
                    UnitOfMeasurement = fahrenheitUnitOfMeasurement,
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
                    LowAlarm = new TempSensorAlarm
                    {
                        AlarmOn = false,
                        AlarmCelsius = -55,
                        AlarmBuzzerOn = false,
                    },
                    HighAlarm = new TempSensorAlarm
                    {
                        AlarmOn = false,
                        AlarmCelsius = 125,
                        AlarmBuzzerOn = false,
                    },
                    LowChartLimiterCelsius = 20,
                    HighChartLimiterCelsius = 30,
                    CreateDate = DateTime.Now,
                };
                context.DSFamilyTempSensor.Add(sensor_2_1);
            }
            else
            {
                sensor_2_1.Family = "DS18B20";
                sensor_2_1.DeviceAddress = sensor_2_1_Address;

                if (sensor_2_1.SensorTriggers == null)
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
            }

            if (sensor_2_2 == null)
            {
                sensor_2_2 = new DSFamilyTempSensor
                {
                    DeviceAddress = sensor_2_2_Address,
                    Family = "DS18B20",
                    TempSensorRangeId = tempSensorRange1.Id,
                    UnitOfMeasurementId = fahrenheitUnitOfMeasurement.Id,
                    UnitOfMeasurement = fahrenheitUnitOfMeasurement,
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
                    LowAlarm = new TempSensorAlarm
                    {
                        AlarmOn = false,
                        AlarmCelsius = -55,
                        AlarmBuzzerOn = false,
                    },
                    HighAlarm = new TempSensorAlarm
                    {
                        AlarmOn = false,
                        AlarmCelsius = 125,
                        AlarmBuzzerOn = false,
                    },
                    LowChartLimiterCelsius = 20,
                    HighChartLimiterCelsius = 30,
                    CreateDate = DateTime.Now,
                };
                context.DSFamilyTempSensor.Add(sensor_2_2);
            }
            else
            {
                sensor_2_2.Family = "DS18B20";
                sensor_2_2.DeviceAddress = sensor_2_2_Address;

                if (sensor_2_2.SensorTriggers == null)
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
            }

            if (sensor_3_1 == null)
            {
                sensor_3_1 = new DSFamilyTempSensor
                {
                    DeviceAddress = sensor_3_1_Address,
                    Family = "DS18B20",
                    TempSensorRangeId = tempSensorRange1.Id,
                    UnitOfMeasurementId = fahrenheitUnitOfMeasurement.Id,
                    UnitOfMeasurement = fahrenheitUnitOfMeasurement,
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
                    LowAlarm = new TempSensorAlarm
                    {
                        AlarmOn = false,
                        AlarmCelsius = -55,
                        AlarmBuzzerOn = false,
                    },
                    HighAlarm = new TempSensorAlarm
                    {
                        AlarmOn = false,
                        AlarmCelsius = 125,
                        AlarmBuzzerOn = false,
                    },
                    LowChartLimiterCelsius = 20,
                    HighChartLimiterCelsius = 30,
                    CreateDate = DateTime.Now,
                };
                context.DSFamilyTempSensor.Add(sensor_3_1);
            }
            else
            {
                sensor_3_1.Family = "DS18B20";
                sensor_3_1.DeviceAddress = sensor_3_1_Address;

                if (sensor_3_1.SensorTriggers == null)
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
            }

            if (sensor_3_2 == null)
            {
                sensor_3_2 = new DSFamilyTempSensor
                {
                    DeviceAddress = sensor_3_2_Address,
                    Family = "DS18B20",
                    TempSensorRangeId = tempSensorRange1.Id,
                    UnitOfMeasurementId = fahrenheitUnitOfMeasurement.Id,
                    UnitOfMeasurement = fahrenheitUnitOfMeasurement,
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
                    LowAlarm = new TempSensorAlarm
                    {
                        AlarmOn = false,
                        AlarmCelsius = -55,
                        AlarmBuzzerOn = false,
                    },
                    HighAlarm = new TempSensorAlarm
                    {
                        AlarmOn = false,
                        AlarmCelsius = 125,
                        AlarmBuzzerOn = false,
                    },
                    LowChartLimiterCelsius = 20,
                    HighChartLimiterCelsius = 30,
                    CreateDate = DateTime.Now,
                };
                context.DSFamilyTempSensor.Add(sensor_3_2);
            }
            else
            {
                sensor_3_2.Family = "DS18B20";
                sensor_3_2.DeviceAddress = sensor_3_2_Address;

                if (sensor_3_2.SensorTriggers == null)
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
                if(espDevice1.DeviceMQ == null)
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
                SensorBaseId = sensor_2_1.Id,
                SensorBase = sensor_2_1,
                DeviceBaseId = espDevice1.Id,
                DeviceBase = espDevice1,
            };

            context.SensorsInDevice.AddOrUpdate(sensorsInDevice_2_1);

            var sensorsInDevice_2_2 = new SensorsInDevice
            {
                SensorBaseId = sensor_2_2.Id,
                SensorBase = sensor_2_2,
                DeviceBaseId = espDevice1.Id,
                DeviceBase = espDevice1,
            };

            context.SensorsInDevice.AddOrUpdate(sensorsInDevice_2_2);

            var sensorsInDevice_3_1 = new SensorsInDevice
            {
                SensorBaseId = sensor_3_1.Id,
                SensorBase = sensor_3_1,
                DeviceBaseId = espDevice1.Id,
                DeviceBase = espDevice1,
            };

            context.SensorsInDevice.AddOrUpdate(sensorsInDevice_3_1);

            var sensorsInDevice_3_2 = new SensorsInDevice
            {
                SensorBaseId = sensor_3_2.Id,
                SensorBase = sensor_3_2,
                DeviceBaseId = espDevice1.Id,
                DeviceBase = espDevice1,
            };

            context.SensorsInDevice.AddOrUpdate(sensorsInDevice_3_2);

            context.SaveChanges();

            #endregion

            ExecuteSettings();
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

        #endregion Methods
    }
}