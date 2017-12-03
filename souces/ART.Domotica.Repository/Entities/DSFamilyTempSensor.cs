namespace ART.Domotica.Repository.Entities
{
    using ART.Domotica.Enums.SI;
    using ART.Infra.CrossCutting.Repository;
    using System;

    public class DSFamilyTempSensor : IEntity<Guid>
    {
        #region Properties

        public Guid Id
        {
            get; set;
        }

        public Sensor Sensor
        {
            get; set;
        }

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