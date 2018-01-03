namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceDebug : IEntity<Guid>
    {
        #region Properties

        public bool Active
        {
            get; set;
        }

        public DeviceBase DeviceBase
        {
            get; set;
        }

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        #endregion Properties
    }
}