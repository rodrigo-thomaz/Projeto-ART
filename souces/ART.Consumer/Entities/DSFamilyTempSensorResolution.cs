using System;
using System.Collections.Generic;

namespace ART.Consumer.Entities
{
    public class DSFamilyTempSensorResolution
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

        public byte Id { get; set; }
        public string Name { get; set; }
        public decimal Resolution { get; set; }
        public decimal ConversionTime { get; set; }
        public byte Bits { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navigation Properties

        public ICollection<DSFamilyTempSensor> DSFamilyTempSensors { get; set; }

        #endregion
    }
}