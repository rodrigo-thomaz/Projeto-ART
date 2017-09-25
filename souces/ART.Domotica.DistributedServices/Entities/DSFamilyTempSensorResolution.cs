using System;

namespace ART.Domotica.DistributedServices.Entities
{
    public class DSFamilyTempSensorResolution : IEntity
    {
        #region Ignore Properties       

        public byte ResolutionDecimalPlaces
        {            
            get
            {
                return BitConverter.GetBytes(decimal.GetBits(Resolution)[3])[2];
            }
        }

        #endregion

        #region Primitive Properties

        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Resolution { get; set; }
        public decimal ConversionTime { get; set; }
        public byte Bits { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation Properties



        #endregion
    }
}