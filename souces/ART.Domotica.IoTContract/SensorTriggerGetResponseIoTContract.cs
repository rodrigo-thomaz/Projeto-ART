namespace ART.Domotica.IoTContract
{
    public class SensorTriggerGetResponseIoTContract
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