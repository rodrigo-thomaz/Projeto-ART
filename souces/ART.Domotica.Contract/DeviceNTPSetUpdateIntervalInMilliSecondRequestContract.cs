namespace ART.Domotica.Contract
{
    using System;

    using ART.Domotica.Enums;

    public class DeviceNTPSetUpdateIntervalInMilliSecondRequestContract
    {
        #region Properties

        public DeviceDatasheetEnum DeviceDatasheetId
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