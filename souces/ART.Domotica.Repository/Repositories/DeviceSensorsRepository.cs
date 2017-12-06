namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceSensorsRepository : RepositoryBase<ARTDbContext, DeviceSensors, Guid>, IDeviceSensorsRepository
    {
        #region Constructors

        public DeviceSensorsRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}