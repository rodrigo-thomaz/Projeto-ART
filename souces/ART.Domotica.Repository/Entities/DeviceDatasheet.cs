namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceDatasheet : IEntity<DeviceDatasheetEnum>
    {
        #region Properties

        public ICollection<DeviceBase> DevicesBase
        {
            get; set;
        }

        public DeviceDatasheetEnum Id
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