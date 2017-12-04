namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class SensorTempDSFamilyRepository : RepositoryBase<ARTDbContext, SensorTempDSFamily, Guid>, ISensorTempDSFamilyRepository
    {
        #region Constructors

        public SensorTempDSFamilyRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}