namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Infra.CrossCutting.Repository;
    using ART.Domotica.Enums;

    // https://pt.wikipedia.org/wiki/Unidade_de_medida
    public class UnitOfMeasurementType : IEntity<UnitOfMeasurementTypeEnum>
    {
        #region Properties

        public UnitOfMeasurementTypeEnum Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public ICollection<TemperatureScale> TemperatureScales
        {
            get; set;
        }

        #endregion Properties
    }
}