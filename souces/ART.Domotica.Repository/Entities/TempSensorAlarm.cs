namespace ART.Domotica.Repository.Entities
{
    public class TempSensorAlarm
    {
        #region Properties

        public bool AlarmBuzzerOn
        {
            get; set;
        }

        public bool AlarmOn
        {
            get; set;
        }

        public decimal AlarmValue
        {
            get; set;
        }

        #endregion Properties
    }
}