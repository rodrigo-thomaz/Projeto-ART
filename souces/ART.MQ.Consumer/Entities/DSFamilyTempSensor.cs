namespace ART.MQ.Consumer.Entities
{
    public class DSFamilyTempSensor : SensorBase
    {
        #region Primitive Properties

        public string DeviceAddress { get; set; }
        public string Family { get; set; }
        public byte TemperatureScaleId { get; set; }
        public byte DSFamilyTempSensorResolutionId { get; set; }

        #endregion

        #region Navigation Properties

        public TemperatureScale TemperatureScale { get; set; }
        public DSFamilyTempSensorResolution DSFamilyTempSensorResolution { get; set; }

        #endregion        
    }
}