namespace ART.Domotica.Contract
{
    using System;

    public class SensorSetLabelRequestContract
    {
        #region Properties

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public string Label
        {
            get; set;
        }

        #endregion Properties
    }
}