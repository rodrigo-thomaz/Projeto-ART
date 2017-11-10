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

        public string MacAddress
        {
            get; set;
        }

        #endregion Properties
    }
}