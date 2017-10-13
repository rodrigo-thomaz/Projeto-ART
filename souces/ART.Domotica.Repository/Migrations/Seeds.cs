namespace ART.Domotica.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Text;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Utils;

    public class Seeds
    {
        #region Methods

        public static void Execute(ARTDbContext context)
        {
            #region TemperatureScale

            var celsiusDescription = new StringBuilder();

            celsiusDescription.AppendLine("A escala de grau Celsius(símbolo: °C) é uma escala termométrica, do sistema métrico[1], usada na maioria dos países do mundo.Teve origem a partir do modelo proposto pelo astrônomo sueco Anders Celsius(1701 - 1744), inicialmente denominado escala centígrada(Grau centígrado).");
            celsiusDescription.AppendLine("Esta escala é baseada nos pontos de fusão e ebulição da água, em condição atmosférica padrão, aos quais são atribuídos os valores de 0 °C e 100 °C, respectivamente[2].Devido a esta divisão centesimal, se deu a antiga nomenclatura grau centígrado(cem partes/ graus) que, em 1948, durante a 9ª Conferência Geral de Pesos e Medidas(CR 64), teve seu nome oficialmente modificado para grau Celsius, em reconhecimento ao trabalho de Anders Celsius e para fim de desambiguação com o prefixo centi do SI.");
            celsiusDescription.AppendLine("Enquanto que os valores de congelação e evaporação da água estão aproximadamente corretos, a definição original não é apropriada como um padrão formal: ela depende da definição de pressão atmosférica padrão, que por sua vez depende da própria definição de temperatura.A definição oficial atual de grau Celsius define 0,01 °C como o ponto triplo da água, e 1 grau Celsius como sendo 1 / 273,16 da diferença de temperatura entre o ponto triplo da água e o zero absoluto. Esta definição garante que 1 grau Celsius apresenta a mesma variação de temperatura que 1 kelvin.");

            var celsiusTemperatureScale = context.TemperatureScale.SingleOrDefault(x => x.Id == 1);

            if (celsiusTemperatureScale == null)
            {
                celsiusTemperatureScale = new TemperatureScale { Id = 1 };
                context.TemperatureScale.Add(celsiusTemperatureScale);
            }

            celsiusTemperatureScale.Name = "Celsius";
            celsiusTemperatureScale.Symbol = "°C";
            celsiusTemperatureScale.Description = celsiusDescription.ToString();

            var fahrenheitDescription = new StringBuilder();

            fahrenheitDescription.AppendLine("O grau fahrenheit(símbolo: °F) é uma escala de temperatura proposta por Daniel Gabriel Fahrenheit em 1724.Nesta escala:");
            fahrenheitDescription.AppendLine("- o ponto de fusão da água(0 °C) é de 32 °F.");
            fahrenheitDescription.AppendLine("- o ponto de ebulição da água(100 °C) é de 212 °F.");
            fahrenheitDescription.AppendLine("Uma diferença de 1,8 °F é igual a uma diferença de 1 °C.");
            fahrenheitDescription.AppendLine("Esta escala foi utilizada principalmente pelos países que foram colonizados pelos britânicos, mas seu uso atualmente se restringe a poucos países de língua inglesa, como os Estados Unidos e Belize. E também, muito utilizada com o povo grego, para medir a temperatura de um corpo.Jakelinneh Devocerg, mulher francesa que criou a teoria 'Fahrenheit Devocerg' que para passar de celsius para fahrenheit se usa sempre 1,8.Ex: f = 137 * e c = 20 * f + 137 - 20 + c.1,8 fc = 117.1,8 = 1,20202020");
            fahrenheitDescription.AppendLine("Para uso científico, há uma escala de temperatura, chamada de Rankine, que leva o marco zero de sua escala ao zero absoluto e possui a mesma variação da escala fahrenheit, existindo, portanto, correlação entre a escala de Rankine e grau fahrenheit do mesmo modo que existe correlação das escalas kelvin e grau Celsius.");

            var fahrenheitTemperatureScale = context.TemperatureScale.SingleOrDefault(x => x.Id == 2);

            if (fahrenheitTemperatureScale == null)
            {
                fahrenheitTemperatureScale = new TemperatureScale { Id = 2 };
                context.TemperatureScale.Add(fahrenheitTemperatureScale);
            }

            fahrenheitTemperatureScale.Name = "Fahrenheit";
            fahrenheitTemperatureScale.Symbol = "°F";
            fahrenheitTemperatureScale.Description = fahrenheitDescription.ToString();

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
            dsFamilyTempSensorResolution9.Description = "Resolução de 9 bits";

            dsFamilyTempSensorResolution10.Name = "10 bits";
            dsFamilyTempSensorResolution10.Bits = 10;
            dsFamilyTempSensorResolution10.Resolution = 0.25M;
            dsFamilyTempSensorResolution10.ConversionTime = 187.5M;
            dsFamilyTempSensorResolution10.Description = "Resolução de 10 bits";

            dsFamilyTempSensorResolution11.Name = "11 bits";
            dsFamilyTempSensorResolution11.Bits = 11;
            dsFamilyTempSensorResolution11.Resolution = 0.125M;
            dsFamilyTempSensorResolution11.ConversionTime = 375;
            dsFamilyTempSensorResolution11.Description = "Resolução de 11 bits";

            dsFamilyTempSensorResolution12.Name = "12 bits";
            dsFamilyTempSensorResolution12.Bits = 12;
            dsFamilyTempSensorResolution12.Resolution = 0.0625M;
            dsFamilyTempSensorResolution12.ConversionTime = 750;
            dsFamilyTempSensorResolution12.Description = "Resolução de 12 bits";

            context.SaveChanges();

            #endregion

            #region DSFamilyTempSensor

            var sensor1Address = "28ffe76da2163d3";
            var sensor2Address = "28fffe6593164b6";

            var sensor1 = context.DSFamilyTempSensor.SingleOrDefault(x => x.DeviceAddress.ToLower() == sensor1Address.ToLower());
            var sensor2 = context.DSFamilyTempSensor.SingleOrDefault(x => x.DeviceAddress.ToLower() == sensor2Address.ToLower());

            if(sensor1 == null)
            {
                sensor1 = new DSFamilyTempSensor
                {
                    DeviceAddress = sensor1Address,
                    Family = "DS18B20",
                    TemperatureScaleId = celsiusTemperatureScale.Id,
                    TemperatureScale = celsiusTemperatureScale,
                    DSFamilyTempSensorResolutionId = dsFamilyTempSensorResolution9.Id,
                    DSFamilyTempSensorResolution = dsFamilyTempSensorResolution9,
                    Pin = RandonHelper.RandomString(4),
                    CreateDate = DateTime.Now,
                };
                context.DSFamilyTempSensor.Add(sensor1);
            }
            else
            {
                sensor1.Family = "DS18B20";
            }

            if (sensor2 == null)
            {
                sensor2 = new DSFamilyTempSensor
                {
                    DeviceAddress = sensor2Address,
                    Family = "DS18B20",
                    TemperatureScaleId = fahrenheitTemperatureScale.Id,
                    TemperatureScale = fahrenheitTemperatureScale,
                    DSFamilyTempSensorResolutionId = dsFamilyTempSensorResolution11.Id,
                    DSFamilyTempSensorResolution = dsFamilyTempSensorResolution11,
                    Pin = RandonHelper.RandomString(4),
                    CreateDate = DateTime.Now,
                };
                context.DSFamilyTempSensor.Add(sensor2);
            }
            else
            {
                sensor2.Family = "DS18B20";
            }

            context.SaveChanges();

            #endregion

            #region ThermometerDevice

            var thermometerDevice1MacAddress = "A0:20:A6:17:83:25";

            var thermometerDevice1 = context.ThermometerDevice.SingleOrDefault(x => x.MacAddress.ToLower() == thermometerDevice1MacAddress.ToLower());

            if (thermometerDevice1 == null)
            {
                thermometerDevice1 = new ThermometerDevice
                {
                    MacAddress = thermometerDevice1MacAddress,
                    Pin = RandonHelper.RandomString(4),
                    CreateDate = DateTime.Now,
                };
                context.ThermometerDevice.Add(thermometerDevice1);
            }
            else
            {
                //code here
            }

            context.SaveChanges();

            #endregion

            #region SensorsInDevice

            var sensorsInDevice1 = new SensorsInDevice
            {
                SensorBaseId = sensor1.Id,
                SensorBase = sensor1,
                DeviceBaseId = thermometerDevice1.Id,
                DeviceBase = thermometerDevice1,
            };

            context.SensorsInDevice.AddOrUpdate(sensorsInDevice1);

            var sensorsInDevice2 = new SensorsInDevice
            {
                SensorBaseId = sensor2.Id,
                SensorBase = sensor2,
                DeviceBaseId = thermometerDevice1.Id,
                DeviceBase = thermometerDevice1,
            };

            context.SensorsInDevice.AddOrUpdate(sensorsInDevice2);

            context.SaveChanges();

            #endregion
        }

        #endregion Methods
    }
}