namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class SensorSetLabelRequestContract
    {
        #region Properties

        public string Label
        {
            get; set;
        }

        public SensorDatasheetEnum SensorDatasheetId
        {
            get; set;
        }

        public Guid SensorId
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        #endregion Properties
    }
}