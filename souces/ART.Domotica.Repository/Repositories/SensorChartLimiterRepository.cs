namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class SensorChartLimiterRepository : RepositoryBase<ARTDbContext, SensorChartLimiter, Guid>, ISensorChartLimiterRepository
    {
        #region Constructors

        public SensorChartLimiterRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}