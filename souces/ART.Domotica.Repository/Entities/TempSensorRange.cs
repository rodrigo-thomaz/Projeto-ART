namespace ART.Domotica.Repository.Entities
{
    using ART.Infra.CrossCutting.Repository;
    using System.Collections.Generic;

    public class TempSensorRange : IEntity<byte>
    {
        #region Properties

        public byte Id
        {
            get; set;
        }

        public short Min { get; set; }

        public short Max { get; set; }

        public ICollection<DSFamilyTempSensor> DSFamilyTempSensors
        {
            get; set;
        }

        #endregion Properties
    }
}