namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class SensorTriggerInsertRequestContract
    {
        #region Properties

        public bool BuzzerOn
        {
            get; set;
        }

        public decimal Max
        {
            get; set;
        }

        public decimal Min
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

        public bool TriggerOn
        {
            get; set;
        }

        #endregion Properties
    }
}