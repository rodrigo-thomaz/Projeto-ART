namespace ART.Domotica.Model
{
    using System;

    public class ESPDeviceSetTimeOffsetInSecondCompletedModel
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