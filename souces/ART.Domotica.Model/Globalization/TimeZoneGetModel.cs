namespace ART.Domotica.Model.Globalization
{
    public class TimeZoneGetModel
    {
        #region Properties

        public string DisplayName
        {
            get; set;
        }

        public byte TimeZoneId
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