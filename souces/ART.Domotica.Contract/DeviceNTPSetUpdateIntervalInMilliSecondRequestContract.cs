namespace ART.Domotica.Contract
{
    using System;

    public class DeviceNTPSetUpdateIntervalInMilliSecondRequestContract
    {
        #region Properties

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceNTPId
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