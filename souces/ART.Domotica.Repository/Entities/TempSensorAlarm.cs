namespace ART.Domotica.Repository.Entities
{
    public class TempSensorAlarm
    {
        #region Properties

        public bool AlarmBuzzerOn
        {
            get; set;
        }

        public decimal AlarmCelsius
        {
            get; set;
        }

        public bool AlarmOn
        {
            get; set;
        }

        #endregion Properties
    }
}