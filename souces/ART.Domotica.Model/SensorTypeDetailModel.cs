namespace ART.Domotica.Model
{
    using ART.Domotica.Enums;

    public class SensorTypeDetailModel
    {
        #region Properties

        public string Name
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