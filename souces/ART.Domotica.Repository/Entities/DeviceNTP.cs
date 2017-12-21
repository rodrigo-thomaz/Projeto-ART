namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceNTP : IEntity<Guid>
    {
        #region Properties

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

        public Entities.Globalization.TimeZone TimeZone
        {
            get; set;
        }

        public byte TimeZoneId
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