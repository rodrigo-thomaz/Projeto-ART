﻿namespace ART.Domotica.Repository.Configurations
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.ModelConfiguration;

    using ART.Domotica.Repository.Entities;

    public class DeviceNTPSettingConfiguration : EntityTypeConfiguration<DeviceNTPSetting>
    {
        #region Constructors

        public DeviceNTPSettingConfiguration()
        {
            //Primary Keys
            HasKey(x => x.Id);

            //Id
            Property(x => x.Id)
                .HasColumnOrder(0)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

            //DeviceBase
            HasRequired(x => x.DeviceBase)
               .WithRequiredDependent(x => x.DeviceNTPSetting);

            //TimeOffsetInSecond
            Property(x => x.TimeOffsetInSecond)
                .HasColumnOrder(1)
                .IsRequired();

            //UpdateIntervalInMilliSecond
            Property(x => x.UpdateIntervalInMilliSecond)
                .HasColumnOrder(2)
                .IsRequired();
        }

        #endregion Constructors
    }
}