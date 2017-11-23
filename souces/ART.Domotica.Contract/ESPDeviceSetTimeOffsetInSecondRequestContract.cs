namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceSetTimeOffsetInSecondRequestContract
    {
        #region Properties

        public Guid DeviceId
        {
            get; set;
        }

        public int TimeOffsetInSecond
        {
            get; set;
        }

        #endregion Properties
    }
}