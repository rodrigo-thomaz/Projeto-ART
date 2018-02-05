namespace ART.Domotica.Model
{
    using System;

    public class DeviceNTPSetUpdateIntervalInMilliSecondModel
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