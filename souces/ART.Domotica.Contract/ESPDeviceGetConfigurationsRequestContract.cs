namespace ART.Domotica.Contract
{
    public class ESPDeviceGetConfigurationsRequestContract
    {
        public int ChipId { get; set; }
        public int FlashChipId { get; set; }
        public string MacAddress { get; set; }
    }
}
