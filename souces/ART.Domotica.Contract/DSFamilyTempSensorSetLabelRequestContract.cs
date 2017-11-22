namespace ART.Domotica.Contract
{
    using System;

    public class DSFamilyTempSensorSetLabelRequestContract
    {
        #region Properties

        public string Label
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