namespace ART.Domotica.Repository.Entities
{
    public abstract class ESPDeviceBase : DeviceBase
    {
        #region Properties

        public string MacAddress
        {
            get; set;
        }

        #endregion Properties
    }
}