namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;

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