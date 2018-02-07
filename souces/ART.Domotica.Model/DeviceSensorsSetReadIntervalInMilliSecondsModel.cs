namespace ART.Domotica.Model
{
    using System;

    public class DeviceSensorsSetReadIntervalInMilliSecondsModel
    {
        #region Properties

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceSensorsId
        {
            get; set;
        }

        public long ReadIntervalInMilliSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}