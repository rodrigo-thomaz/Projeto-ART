using ART.Domotica.DistributedServices.Entities;
using System;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;

namespace ART.Domotica.DistributedServices.Migrations
{
    public class Seeds
    {
        public static void Execute(ARTDbContext context)
        {
            #region TemperatureScale

            var celsiusDescription = new StringBuilder();
            
            celsiusDescription.AppendLine("A escala de grau Celsius(símbolo: °C) é uma escala termométrica, do sistema métrico[1], usada na maioria dos países do mundo.Teve origem a partir do modelo proposto pelo astrônomo sueco Anders Celsius(1701 - 1744), inicialmente denominado escala centígrada(Grau centígrado).");
            celsiusDescription.AppendLine("Esta escala é baseada nos pontos de fusão e ebulição da água, em condição atmosférica padrão, aos quais são atribuídos os valores de 0 °C e 100 °C, respectivamente[2].Devido a esta divisão centesimal, se deu a antiga nomenclatura grau centígrado(cem partes/ graus) que, em 1948, durante a 9ª Conferência Geral de Pesos e Medidas(CR 64), teve seu nome oficialmente modificado para grau Celsius, em reconhecimento ao trabalho de Anders Celsius e para fim de desambiguação com o prefixo centi do SI.");
            celsiusDescription.AppendLine("Enquanto que os valores de congelação e evaporação da água estão aproximadamente corretos, a definição original não é apropriada como um padrão formal: ela depende da definição de pressão atmosférica padrão, que por sua vez depende da própria definição de temperatura.A definição oficial atual de grau Celsius define 0,01 °C como o ponto triplo da água, e 1 grau Celsius como sendo 1 / 273,16 da diferença de temperatura entre o ponto triplo da água e o zero absoluto. Esta definição garante que 1 grau Celsius apresenta a mesma variação de temperatura que 1 kelvin.");
                        
            var celsiusTemperatureScale = new TemperatureScale
            {
                Name = "Celsius",
                Description = celsiusDescription.ToString(),
            };

            var fahrenheitDescription = new StringBuilder();

            fahrenheitDescription.AppendLine("O grau fahrenheit(símbolo: °F) é uma escala de temperatura proposta por Daniel Gabriel Fahrenheit em 1724.Nesta escala:");
            fahrenheitDescription.AppendLine("- o ponto de fusão da água(0 °C) é de 32 °F.");
            fahrenheitDescription.AppendLine("- o ponto de ebulição da água(100 °C) é de 212 °F.");
            fahrenheitDescription.AppendLine("Uma diferença de 1,8 °F é igual a uma diferença de 1 °C.");
            fahrenheitDescription.AppendLine("Esta escala foi utilizada principalmente pelos países que foram colonizados pelos britânicos, mas seu uso atualmente se restringe a poucos países de língua inglesa, como os Estados Unidos e Belize. E também, muito utilizada com o povo grego, para medir a temperatura de um corpo.Jakelinneh Devocerg, mulher francesa que criou a teoria 'Fahrenheit Devocerg' que para passar de celsius para fahrenheit se usa sempre 1,8.Ex: f = 137 * e c = 20 * f + 137 - 20 + c.1,8 fc = 117.1,8 = 1,20202020");
            fahrenheitDescription.AppendLine("Para uso científico, há uma escala de temperatura, chamada de Rankine, que leva o marco zero de sua escala ao zero absoluto e possui a mesma variação da escala fahrenheit, existindo, portanto, correlação entre a escala de Rankine e grau fahrenheit do mesmo modo que existe correlação das escalas kelvin e grau Celsius.");
            
            var fahrenheitTemperatureScale = new TemperatureScale
            {
                Name = "Fahrenheit",
                Description = fahrenheitDescription.ToString(),
            };
            
            context.TemperatureScale.AddOrUpdate(x => x.Name, celsiusTemperatureScale);
            context.TemperatureScale.AddOrUpdate(x => x.Name, fahrenheitTemperatureScale);

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
                };
                context.DSFamilyTempSensor.Add(sensor2);
            }   
            else
            {
                sensor2.Family = "DS18B20";
            }

            #endregion

            #region Space

            var space1 = new Space
            {
                Name = "Aquário Sala",
                Description = "Aquário Quarto",
            };

            var space2 = new Space
            {
                Name = "Fonte com carpas",
                Description = "Pequena fonte com carpas no quintal",
            };

            context.Space.AddOrUpdate(x => x.Name, space1);

            context.Space.AddOrUpdate(x => x.Name, space2);

            #endregion

            #region HardwareInSpace

            var hardwareInSpace1 = new HardwareInSpace
            {
                HardwareBase = sensor1,
                HardwareBaseId = sensor1.Id,
                Space = space1,
                SpaceId = space1.Id,
            };

            context.HardwareInSpace.AddOrUpdate(hardwareInSpace1);

            var hardwareInSpace2 = new HardwareInSpace
            {
                HardwareBase = sensor2,
                HardwareBaseId = sensor2.Id,
                Space = space2,
                SpaceId = space2.Id,
            };

            context.HardwareInSpace.AddOrUpdate(hardwareInSpace2);

            #endregion

            #region DSFamilyTempSensorResolutions

            var dsFamilyTempSensorResolution1 = new DSFamilyTempSensorResolution
            {
                Name = "9 bits",
                Bits = 9,
                Resolution = 0.5M,
                ConversionTime = 93.75M,
                Description = "Resolução de 9 bits",
            };

            var dsFamilyTempSensorResolution2 = new DSFamilyTempSensorResolution
            {
                Name = "10 bits",
                Bits = 10,
                Resolution = 0.25M,
                ConversionTime = 187.5M,
                Description = "Resolução de 10 bits",
            };

            var dsFamilyTempSensorResolution3 = new DSFamilyTempSensorResolution
            {
                Name = "11 bits",
                Bits = 11,
                Resolution = 0.125M,
                ConversionTime = 375,
                Description = "Resolução de 11 bits",
            };

            var dsFamilyTempSensorResolution4 = new DSFamilyTempSensorResolution
            {
                Name = "12 bits",
                Bits = 12,
                Resolution = 0.0625M,
                ConversionTime = 750,
                Description = "Resolução de 12 bits",
            };

            context.DSFamilyTempSensorResolution.AddOrUpdate(x => x.Bits, dsFamilyTempSensorResolution1);
            context.DSFamilyTempSensorResolution.AddOrUpdate(x => x.Bits, dsFamilyTempSensorResolution2);
            context.DSFamilyTempSensorResolution.AddOrUpdate(x => x.Bits, dsFamilyTempSensorResolution3);
            context.DSFamilyTempSensorResolution.AddOrUpdate(x => x.Bits, dsFamilyTempSensorResolution4);

            #endregion
        }
    }
}