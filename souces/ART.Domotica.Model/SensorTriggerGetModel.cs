namespace ART.Domotica.Model
{
    using System;

    public class SensorTriggerGetModel
    {
        #region Properties

        public bool BuzzerOn
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

        public bool TriggerOn
        {
            get; set;
        }

        public string TriggerValue
        {
            get; set;
        }

        #endregion Properties
    }
}