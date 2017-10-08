namespace ART.Domotica.Model
{
    public class DSFamilyTempSensorGetAllModel
    {
        #region Properties

        public string DeviceAddress
        {
            get; set;
        }

        public byte DSFamilyTempSensorResolutionId
        {
            get; set;
        }

        public string Family
        {
            get; set;
        }

        public decimal HighAlarm
        {
            get; set;
        }

        public decimal LowAlarm
        {
            get; set;
        }

        public byte TemperatureScaleId
        {
            get; set;
        }

        #endregion Properties
    }
}