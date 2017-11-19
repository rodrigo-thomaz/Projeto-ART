namespace ART.Domotica.IoTContract
{
    public class TempSensorAlarmResponseIoTContract
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