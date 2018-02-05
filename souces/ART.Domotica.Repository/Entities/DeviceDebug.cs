namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;

    public class DeviceDebug : IEntity<Guid>
    {
        #region Properties

        public DeviceBase DeviceBase
        {
            get; set;
        }

        public Guid DeviceDatasheetId
        {
            get; set;
        }

        public Guid Id
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