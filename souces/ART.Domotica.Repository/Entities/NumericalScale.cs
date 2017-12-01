namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

    public class NumericalScale : IEntity<NumericalScaleEnum>
    {
        #region Properties        

        public NumericalScaleEnum Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public ICollection<UnitMeasurementScale> UnitMeasurementScales
        {
            get; set;
        }

        public ICollection<NumericalScaleCountry> NumericalScaleCountries
        {
            get; set;
        }
        
        #endregion Properties
    }
}