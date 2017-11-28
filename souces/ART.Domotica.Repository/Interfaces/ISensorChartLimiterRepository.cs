using ART.Domotica.Repository.Entities;
using ART.Infra.CrossCutting.Repository;
using System;

namespace ART.Domotica.Repository.Interfaces
{
    public interface ISensorChartLimiterRepository : IRepository<ARTDbContext, SensorChartLimiter, Guid>
    {
        #region Methods


        #endregion Methods
    }
}