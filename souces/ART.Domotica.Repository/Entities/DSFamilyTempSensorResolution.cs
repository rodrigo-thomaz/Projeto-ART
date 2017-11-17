namespace ART.Domotica.Repository.Entities
{
    using System;
    using System.Collections.Generic;

    using ART.Infra.CrossCutting.Repository;

    public class DSFamilyTempSensorResolution : IEntity<byte>
    {
        #region Properties

        public byte Bits
        {
            get; set;
        }

        public decimal ConversionTime
        {
            get; set;
        }

        public string Description
        {
            get; set;
        }

        public ICollection<DSFamilyTempSensor> DSFamilyTempSensors
        {
            get; set;
        }

        public byte Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public decimal Resolution
        {
            get; set;
        }

        public byte DecimalPlaces
        {
            get; set;
        }

        #endregion Properties
    }
}