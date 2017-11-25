namespace ART.Domotica.Model
{
    public class TimeZoneDetailModel
    {
        #region Properties

        public byte Id { get; set; }
        public string DisplayName { get; set; }
        public bool SupportsDaylightSavingTime { get; set; }
        public int UtcTimeOffsetInSecond { get; set; }

        #endregion Properties
    }
}