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

        public int ReadIntervalInMilliSeconds
        {
            get; set;
        }

        #endregion Properties
    }
}