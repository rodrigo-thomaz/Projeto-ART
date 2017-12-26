namespace ART.Domotica.Repository.Entities
{
    public class RaspberryDevice : DeviceBase
    {
        #region Properties

        public string LanMacAddress
        {
            get; set;
        }

        public string WLanMacAddress
        {
            get; set;
        }

        #endregion Properties
    }
}