namespace ART.Data.Repository.Entities
{
    using System;
    using System.Collections.Generic;

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

        public byte ResolutionDecimalPlaces
        {
            get
            {
                return BitConverter.GetBytes(decimal.GetBits(Resolution)[3])[2];
            }
        }

        #endregion Properties
    }
}