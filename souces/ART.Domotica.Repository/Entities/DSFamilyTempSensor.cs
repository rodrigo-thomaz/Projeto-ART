namespace ART.Domotica.Repository.Entities
{
    using ART.Domotica.Enums.SI;

    public class DSFamilyTempSensor : Sensor
    {
        #region Constructors

        public DSFamilyTempSensor()
        {
            UnitMeasurementTypeId = UnitMeasurementTypeEnum.Temperature;
        }

        #endregion Constructors

        #region Properties

        public string DeviceAddress
        {
            get; set;
        }

        public DSFamilyTempSensorResolution DSFamilyTempSensorResolution
        {
            get; set;
        }

        public byte DSFamilyTempSensorResolutionId
        {
            get; set;
        }

        public string Family
        {
            get; set;
        }

        #endregion Properties
    }
}