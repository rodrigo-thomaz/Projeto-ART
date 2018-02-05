namespace ART.Domotica.Model
{
    using System;

    public class DeviceDebugGetModel
    {
        #region Properties

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public Guid DeviceDebugId
        {
            get; set;
        }

        public bool RemoteEnabled
        {
            get; set;
        }

        public bool ResetCmdEnabled
        {
            get; set;
        }

        public bool SerialEnabled
        {
            get; set;
        }

        public bool ShowColors
        {
            get; set;
        }

        public bool ShowDebugLevel
        {
            get; set;
        }

        public bool ShowProfiler
        {
            get; set;
        }

        public bool ShowTime
        {
            get; set;
        }

        #endregion Properties
    }
}