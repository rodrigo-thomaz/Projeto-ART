namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class ThermometerDeviceRepository : RepositoryBase<ARTDbContext, ThermometerDevice, Guid>, IThermometerDeviceRepository
    {
        #region Constructors

        public ThermometerDeviceRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}