namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class NumericalScaleType : IEntity<NumericalScaleTypeEnum>
    {
        #region Properties        

        public ICollection<UnitMeasurementScale> UnitMeasurementScales
        {
            get; set;
        }

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
        
        #endregion Properties
    }
}