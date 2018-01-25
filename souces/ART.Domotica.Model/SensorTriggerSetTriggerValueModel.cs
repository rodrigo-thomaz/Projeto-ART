namespace ART.Domotica.Model
{
    using System;

    using ART.Domotica.Enums;

    public class SensorTriggerSetTriggerValueModel
    {
        #region Properties

        public PositionEnum Position
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

        public decimal TriggerValue
        {
            get; set;
        }

        #endregion Properties
    }
}