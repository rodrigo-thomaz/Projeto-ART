using ART.Domotica.Repository.Entities;
using ART.Domotica.Repository.Interfaces;
using ART.Infra.CrossCutting.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace ART.Domotica.Repository.Repositories
{
    public class DeviceSensorsRepository : RepositoryBase<ARTDbContext, DeviceSensors, Guid>, IDeviceSensorsRepository
    {
        #region Constructors

        public DeviceSensorsRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors    

        public async Task<List<DeviceSensors>> GetAllByApplicationId(Guid applicationId)
        {
            IQueryable<DeviceSensors> query = from ds in _context.DeviceSensors
                                                join dia in _context.DeviceInApplication on ds.Id equals dia.HardwareId
                                                where dia.ApplicationId == applicationId
                                                select ds;

            return await query.ToListAsync();
        }
    }
}