namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceBaseRepository : RepositoryBase<ARTDbContext, DeviceBase, Guid>, IDeviceBaseRepository
    {
        #region Constructors

        public DeviceBaseRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}