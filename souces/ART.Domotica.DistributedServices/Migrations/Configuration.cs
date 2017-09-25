namespace ART.Domotica.DistributedServices.Migrations
{
    using ART.Domotica.DistributedServices.Entities;
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<ARTDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = false;
            ContextKey = "ARTDomoticaDbContext";
        }

        protected override void Seed(ARTDbContext context)
        {
            #region sensors

            var sensor1 = new DSFamilyTempSensor
            {
                Id = Guid.Parse("018CF459-4451-4EB8-B843-F329D07B2181"),
                DeviceAddress = "28ffe76da2163d3",
                Family = "DS18B20",
            };

            var sensor2 = new DSFamilyTempSensor
            {
                Id = Guid.Parse("972632D0-B9F4-46A5-A39F-11EAA7ED9514"),
                DeviceAddress = "28fffe6593164b6",
                Family = "DS18B20",
            };

            context.DSFamilyTempSensor.AddOrUpdate(sensor1);

            context.DSFamilyTempSensor.AddOrUpdate(sensor2);

            #endregion

            #region spaces

            var space1 = new Space
            {
                Id = Guid.Parse("51C174A1-266E-4245-BA8C-F6F2AA4B2652"),
                Name = "Aquário Sala",
                Description = "Aquário Quarto",
            };

            var space2 = new Space
            {
                Id = Guid.Parse("66A84A85-9D2C-4EBC-91D4-F891C3BD8A22"),
                Name = "Fonte com carpas",
                Description = "Pequena fonte com carpas no quintal",
            };

            context.Space.AddOrUpdate(space1);

            context.Space.AddOrUpdate(space2);

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
                Id = Guid.Parse("00F67477-EE29-4679-882A-6BEB6907381C"),
                Name = "9 bits",
                Bits = 9,
                Resolution= 0.5M,
                ConversionTime = 93.75M,
                Description = "Resolução de 9 bits",
            };

            var dsFamilyTempSensorResolution2 = new DSFamilyTempSensorResolution
            {
                Id = Guid.Parse("89835D9C-A817-4A8A-BDC2-B41DBD5FF281"),
                Name = "10 bits",
                Bits = 10,
                Resolution = 0.25M,
                ConversionTime = 187.5M,
                Description = "Resolução de 10 bits",
            };

            var dsFamilyTempSensorResolution3 = new DSFamilyTempSensorResolution
            {
                Id = Guid.Parse("E89644D5-6B95-4411-BB57-0F4E039E814B"),
                Name = "11 bits",
                Bits = 11,
                Resolution = 0.125M,
                ConversionTime = 375,
                Description = "Resolução de 11 bits",
            };

            var dsFamilyTempSensorResolution4 = new DSFamilyTempSensorResolution
            {
                Id = Guid.Parse("EFA51275-6A7D-4CDC-99A3-9C943B99D2C8"),
                Name = "12 bits",
                Bits = 12,
                Resolution = 0.0625M,
                ConversionTime = 750,
                Description = "Resolução de 12 bits",
            };

            context.DSFamilyTempSensorResolution.AddOrUpdate(dsFamilyTempSensorResolution1);
            context.DSFamilyTempSensorResolution.AddOrUpdate(dsFamilyTempSensorResolution2);
            context.DSFamilyTempSensorResolution.AddOrUpdate(dsFamilyTempSensorResolution3);
            context.DSFamilyTempSensorResolution.AddOrUpdate(dsFamilyTempSensorResolution4);

            #endregion
        }
    }
}
