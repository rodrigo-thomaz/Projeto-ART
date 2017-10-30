namespace ART.Domotica.Repository.Entities
{
    public abstract class ESPDeviceBase : DeviceBase
    {
        #region Properties

        public string MacAddress
        {
            get; set;
        }

        public string Pin
        {
            get; set;
        }

        #endregion Properties
    }
}