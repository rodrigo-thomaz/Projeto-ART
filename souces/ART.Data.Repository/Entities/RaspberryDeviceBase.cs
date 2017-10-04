namespace ART.Data.Repository.Entities
{
    public class RaspberryDeviceBase : DeviceBase
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