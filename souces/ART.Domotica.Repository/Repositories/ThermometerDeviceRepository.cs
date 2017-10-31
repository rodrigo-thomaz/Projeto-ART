namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System.Data.Entity;

    public class ThermometerDeviceRepository : RepositoryBase<ARTDbContext, ThermometerDevice, Guid>, IThermometerDeviceRepository
    {
        #region Constructors

        public ThermometerDeviceRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        #region Methods

        public async Task<List<ThermometerDevice>> GetList()
        {
            var data = await _context.ThermometerDevice
                .ToListAsync();
            return data;
        }

        #endregion Methods
    }
}