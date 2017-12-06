namespace ART.Domotica.Repository.Entities.Globalization
{
    using System.Collections.Generic;

    using ART.Infra.CrossCutting.Repository;

    public class TimeZone : IEntity<byte>
    {
        #region Properties

        public ICollection<DeviceNTP> DevicesNTP
        {
            get; set;
        }

        public string DisplayName
        {
            get; set;
        }

        public byte Id
        {
            get; set;
        }

        public bool SupportsDaylightSavingTime
        {
            get; set;
        }

        public int UtcTimeOffsetInSecond
        {
            get; set;
        }

        #endregion Properties
    }
}