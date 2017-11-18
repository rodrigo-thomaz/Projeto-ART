namespace ART.Domotica.IoTContract
{
    public class TempSensorAlarmResponseIoTContract
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