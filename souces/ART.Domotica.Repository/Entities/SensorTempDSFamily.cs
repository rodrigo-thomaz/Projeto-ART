namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Infra.CrossCutting.Repository;
    using ART.Domotica.Enums;

    public class SensorTempDSFamily : IEntity<Guid>
    {
        #region Properties

        public string DeviceAddress
        {
            get; set;
        }

        public string Family
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public Sensor Sensor
        {
            get; set;
        }

        public SensorTempDSFamilyResolution SensorTempDSFamilyResolution
        {
            get; set;
        }

        public byte SensorTempDSFamilyResolutionId
        {
            get; set;
        }

        public SensorDatasheetEnum SensorDatasheetId
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