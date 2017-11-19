namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Infra.CrossCutting.Repository;

    public class TempSensorRange : IEntity<byte>
    {
        #region Properties

        public ICollection<DSFamilyTempSensor> DSFamilyTempSensors
        {
            get; set;
        }

        public byte Id
        {
            get; set;
        }

        public short Max
        {
            get; set;
        }

        public short Min
        {
            get; set;
        }

        #endregion Properties
    }
}