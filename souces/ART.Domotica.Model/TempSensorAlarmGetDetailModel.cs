namespace ART.Domotica.Model
{
    public class TempSensorAlarmGetDetailModel
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