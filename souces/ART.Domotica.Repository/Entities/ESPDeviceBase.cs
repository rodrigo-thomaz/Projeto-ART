namespace ART.Domotica.Repository.Entities
{
    public abstract class ESPDeviceBase : DeviceBase
    {
        #region Properties

        public int ChipId
        {
            get; set;
        }

        public int FlashChipId
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