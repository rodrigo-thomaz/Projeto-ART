namespace ART.Domotica.Repository.Entities
{
    using ART.Domotica.Enums;

    public class RaspberryDevice : DeviceBase
    {
        #region Properties

        public DeviceDatasheetEnum DeviceDatasheetId
        {
            get; set;
        }

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