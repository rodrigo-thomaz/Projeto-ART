namespace ART.Domotica.Contract
{
    public class ESPDeviceGetConfigurationsRPCRequestContract
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

        public string SoftAPMacAddress
        {
            get; set;
        }

        public string StationMacAddress
        {
            get; set;
        }

        #endregion Properties
    }
}