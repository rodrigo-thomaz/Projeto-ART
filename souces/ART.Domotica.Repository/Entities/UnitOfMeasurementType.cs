using ART.Infra.CrossCutting.Repository;
using System.Collections.Generic;

namespace ART.Domotica.Repository.Entities
{
    // https://pt.wikipedia.org/wiki/Unidade_de_medida

    public class UnitOfMeasurementType : IEntity<UnitOfMeasurementTypeEnum>
    {
        #region Properties        

        public string Name
        {
            get; set;
        }        

        public ICollection<TemperatureScale> TemperatureScales
        {
            get; set;
        }

        public UnitOfMeasurementTypeEnum Id
        {
            get; set;
        }

        #endregion Properties
    }

    
}

