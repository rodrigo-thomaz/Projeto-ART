namespace ART.Domotica.Repository.Entities
{
    using System;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

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

        public SensorDatasheetEnum SensorDatasheetId
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

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        #endregion Properties
    }
}