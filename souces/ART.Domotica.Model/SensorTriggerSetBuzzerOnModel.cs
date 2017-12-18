namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class SensorTriggerSetBuzzerOnModel
    {
        #region Properties

        public bool BuzzerOn
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

        public Guid SensorTriggerId
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