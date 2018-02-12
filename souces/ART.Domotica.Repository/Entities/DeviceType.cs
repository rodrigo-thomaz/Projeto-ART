namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceType : IEntity<DeviceTypeEnum>
    {
        #region Properties

        public ICollection<DeviceDatasheet> DeviceDatasheets
        {
            get; set;
        }

        public DeviceTypeEnum Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }
}