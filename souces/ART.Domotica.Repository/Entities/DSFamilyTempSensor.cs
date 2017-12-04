namespace ART.Domotica.Repository.Entities
{
    using System;
    using ART.Infra.CrossCutting.Repository;

    public class DSFamilyTempSensor : IEntity<Guid>
    {
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

        public Guid Id
        {
            get; set;
        }

        public Sensor Sensor
        {
            get; set;
        }

        #endregion Properties
    }
}