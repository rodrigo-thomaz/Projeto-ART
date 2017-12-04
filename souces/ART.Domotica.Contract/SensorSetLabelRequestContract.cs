namespace ART.Domotica.Contract
{
    using System;

    public class SensorSetLabelRequestContract
    {
        #region Properties

        public string Label
        {
            get; set;
        }

        public Guid SensorTempDSFamilyId
        {
            get; set;
        }

        #endregion Properties
    }
}