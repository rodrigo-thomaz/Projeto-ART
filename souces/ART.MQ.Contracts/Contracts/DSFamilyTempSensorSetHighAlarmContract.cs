using System;

namespace ART.MQ.Common.Contracts
{
    [Serializable]
    public class DSFamilyTempSensorSetHighAlarmContract
    {
        public string DeviceAddress { get; set; }
        public int Value { get; set; }
    }
}
