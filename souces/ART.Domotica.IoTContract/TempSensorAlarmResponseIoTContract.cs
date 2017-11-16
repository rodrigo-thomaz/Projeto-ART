namespace ART.Domotica.IoTContract
{
    public class TempSensorAlarmResponseIoTContract
    {
        public bool AlarmOn { get; set; }
        public decimal AlarmValue { get; set; }
        public bool AlarmBuzzerOn { get; set; }
    }
}
