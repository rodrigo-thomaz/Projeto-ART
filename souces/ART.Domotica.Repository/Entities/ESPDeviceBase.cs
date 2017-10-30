namespace ART.Domotica.Repository.Entities
{
    public abstract class ESPDeviceBase : DeviceBase
    {
        #region Properties

        public string ChipId
        {
            get; set;
        }

        public string FlashChipId
        {
            get; set;
        }

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