namespace ART.Domotica.Model
{
    using System;

    public class DSFamilyTempSensorGetDetailModel
    {
        #region Properties

        public Guid DSFamilyTempSensorId
        {
            get; set;
        }

        public byte DSFamilyTempSensorResolutionId
        {
            get; set;
        }

        public TempSensorRangeGetDetailModel TempSensorRange { get; set; }

        public TempSensorAlarmGetDetailModel HighAlarm
        {
            get; set;
        }

        public TempSensorAlarmGetDetailModel LowAlarm
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