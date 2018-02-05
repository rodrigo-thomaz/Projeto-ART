namespace ART.Domotica.Contract
{
    using System;

    public class DeviceSetIntervalInMilliSecondsRequestContract
    {
        #region Properties

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceId
        {
            get; set;
        }

        public int IntervalInMilliSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}