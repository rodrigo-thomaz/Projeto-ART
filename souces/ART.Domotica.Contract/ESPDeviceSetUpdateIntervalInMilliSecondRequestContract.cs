namespace ART.Domotica.Contract
{
    using System;

    public class ESPDeviceSetUpdateIntervalInMilliSecondRequestContract
    {
        #region Properties

        public Guid DeviceId
        {
            get; set;
        }

        public int UpdateIntervalInMilliSecond
        {
            get; set;
        }

        #endregion Properties
    }
}