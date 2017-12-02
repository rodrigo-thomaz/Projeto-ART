namespace ART.Domotica.Model.Locale
{
    using ART.Domotica.Enums.Locale;

    public class CountryDetailModel
    {
        #region Properties

        public ContinentEnum ContinentId
        {
            get; set;
        }

        public short CountryId
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }
}