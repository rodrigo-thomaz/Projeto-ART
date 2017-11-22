namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorSetLabelCompletedModel
    {
        #region Properties

        public string Label
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        #endregion Properties
    }
}