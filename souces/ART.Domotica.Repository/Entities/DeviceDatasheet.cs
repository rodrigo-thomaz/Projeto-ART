namespace ART.Domotica.Repository.Entities
{
    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceDatasheet : IEntity<DeviceDatasheetEnum>
    {
        #region Properties

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