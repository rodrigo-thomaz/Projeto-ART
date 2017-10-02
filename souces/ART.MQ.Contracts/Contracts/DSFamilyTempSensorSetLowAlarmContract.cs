using System;

namespace ART.MQ.Common.Contracts
{
    [Serializable]
    public class DSFamilyTempSensorSetLowAlarmContract
    {
        public string DeviceAddress { get; set; }
        public int Value { get; set; }
    }
}
