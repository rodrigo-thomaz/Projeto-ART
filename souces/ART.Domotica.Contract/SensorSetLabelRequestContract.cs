namespace ART.Domotica.Contract
{
    using System;

    public class SensorSetLabelRequestContract
    {
        #region Properties

        public Guid SensorId
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