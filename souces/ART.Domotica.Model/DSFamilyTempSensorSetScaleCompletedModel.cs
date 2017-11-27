namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorSetScaleCompletedModel
    {
        #region Properties

        public Guid DeviceId
        {
            get; set;
        }

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public byte UnitOfMeasurementId
        {
            get; set;
        }

        #endregion Properties
    }
}