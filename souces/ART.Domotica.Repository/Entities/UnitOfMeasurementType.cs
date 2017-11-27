namespace ART.Domotica.Repository.Entities
{
    using System.Collections.Generic;

    using ART.Domotica.Enums;
    using ART.Infra.CrossCutting.Repository;

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

        public ICollection<UnitOfMeasurement> UnitOfMeasurements
        {
            get; set;
        }

        #endregion Properties
    }
}