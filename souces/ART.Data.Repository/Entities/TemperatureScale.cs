namespace ART.Data.Repository.Entities
{
    using System.Collections.Generic;

    public class TemperatureScale : IEntity<byte>
    {
        #region Properties

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

        public string Symbol
        {
            get; set;
        }

        #endregion Properties
    }
}