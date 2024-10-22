﻿namespace ART.Domotica.Repository.Entities.SI
{
    using System.Collections.Generic;

    using ART.Domotica.Enums.SI;
    using ART.Infra.CrossCutting.Repository;

    // https://pt.wikipedia.org/wiki/Unidade_de_medida
    public class UnitMeasurementType : IEntity<UnitMeasurementTypeEnum>
    {
        #region Properties

        public UnitMeasurementTypeEnum Id
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public ICollection<UnitMeasurement> UnitMeasurements
        {
            get; set;
        }

        #endregion Properties
    }
}