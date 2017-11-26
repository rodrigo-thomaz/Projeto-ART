namespace ART.Domotica.Model
{
    public class TimeZoneDetailModel
    {
        #region Properties

        public string DisplayName
        {
            get; set;
        }

        public byte Id
        {
            get; set;
        }

        public bool SupportsDaylightSavingTime
        {
            get; set;
        }

        public int UtcTimeOffsetInSecond
        {
            get; set;
        }

        #endregion Properties
    }
}