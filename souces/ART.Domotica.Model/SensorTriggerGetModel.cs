using System;

namespace ART.Domotica.Model
{
    public class SensorTriggerGetModel
    {
        #region Properties

        public Guid SensorTriggerId { get; set; }

        public bool BuzzerOn
        {
            get; set;
        }

        public Guid SensorId
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