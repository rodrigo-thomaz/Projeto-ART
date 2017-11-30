namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;

    public class SensorDatasheetDetailModel
    {
        #region Properties

        public SensorDatasheetEnum Id
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        #endregion Properties
    }
}