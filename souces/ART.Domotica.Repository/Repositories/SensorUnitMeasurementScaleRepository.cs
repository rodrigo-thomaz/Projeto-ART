namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class SensorUnitMeasurementScaleRepository : RepositoryBase<ARTDbContext, SensorUnitMeasurementScale, Guid>, ISensorUnitMeasurementScaleRepository
    {
        #region Constructors

        public SensorUnitMeasurementScaleRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}