namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using System;
    using ART.Infra.CrossCutting.Repository;

    public class Sensor : IEntity<Guid>
    {
        #region Properties

        public DateTime CreateDate
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


        public SensorDatasheet SensorDatasheet
        {
            get; set;
        }

        public SensorDatasheetEnum SensorDatasheetId
        {
            get; set;
        }

        public ICollection<SensorInDevice> SensorInDevice
        {
            get; set;
        }

        public ICollection<SensorInApplication> SensorInApplication
        {
            get; set;
        }

        public SensorTempDSFamily SensorTempDSFamily
        {
            get; set;
        }

        public ICollection<SensorTrigger> SensorTriggers
        {
            get; set;
        }

        public SensorTypeEnum SensorTypeId
        {
            get; set;
        }

        public SensorUnitMeasurementScale SensorUnitMeasurementScale
        {
            get; set;
        }

        #endregion Properties
    }
}