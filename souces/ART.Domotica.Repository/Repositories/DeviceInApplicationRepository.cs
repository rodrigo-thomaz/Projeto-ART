namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceInApplicationRepository : RepositoryBase<ARTDbContext, DeviceInApplication, Guid>, IDeviceInApplicationRepository
    {
        #region Constructors

        public DeviceInApplicationRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}