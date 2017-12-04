namespace ART.Domotica.Repository.Entities
{
    using System;
    using System.Collections.Generic;
    using ART.Infra.CrossCutting.Repository;
    using ART.Domotica.Enums;

    public class Sensor : IEntity<Guid>
    {
        #region Properties

        public SensorDatasheetEnum SensorDatasheetId
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        public SensorDatasheet SensorDatasheet
        {
            get; set;
        }

        public DateTime CreateDate
        {
            get; set;
        }

        public DSFamilyTempSensor DSFamilyTempSensor
        {
            get; set;
        }

        public Guid Id
        {
            get; set;
        }

        public string Label
        {
            get; set;
        }

        public SensorUnitMeasurementScale SensorUnitMeasurementScale
        {
            get; set;
        }

        public ICollection<SensorsInDevice> SensorsInDevice
        {
            get; set;
        }

        public ICollection<SensorTrigger> SensorTriggers
        {
            get; set;
        }      

        #endregion Properties
    }
}