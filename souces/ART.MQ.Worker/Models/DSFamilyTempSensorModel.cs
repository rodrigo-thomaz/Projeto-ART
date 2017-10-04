namespace ART.MQ.Worker.Models
{
    public class DSFamilyTempSensorModel
    {
        public string DeviceAddress { get; set; }
        public string Family { get; set; }
        public byte TemperatureScaleId { get; set; }
        public byte DSFamilyTempSensorResolutionId { get; set; }
        public decimal HighAlarm { get; set; }
        public decimal LowAlarm { get; set; }
    }
}
