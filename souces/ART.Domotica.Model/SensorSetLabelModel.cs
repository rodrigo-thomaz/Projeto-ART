namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class SensorSetLabelModel
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