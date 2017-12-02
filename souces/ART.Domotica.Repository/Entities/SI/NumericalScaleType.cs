namespace ART.Domotica.Repository.Entities.SI
{
    using System.Collections.Generic;

    using ART.Domotica.Enums.SI;
    using ART.Infra.CrossCutting.Repository;

    public class NumericalScaleType : IEntity<NumericalScaleTypeEnum>
    {
        #region Properties

        public NumericalScaleTypeEnum Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public ICollection<NumericalScale> NumericalScales
        {
            get; set;
        }

        public ICollection<NumericalScaleTypeCountry> NumericalScaleTypeCountries
        {
            get; set;
        }

        public ICollection<SensorUnitMeasurementDefault> SensorUnitMeasurementDefaults
        {
            get; set;
        }

        #endregion Properties
    }
}