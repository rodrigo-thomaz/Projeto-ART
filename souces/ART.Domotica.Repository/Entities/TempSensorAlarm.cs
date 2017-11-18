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

        public decimal AlarmCelsius
        {
            get; set;
        }

        #endregion Properties
    }
}