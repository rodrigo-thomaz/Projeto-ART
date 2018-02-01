namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceBaseRepository : RepositoryBase<ARTDbContext, DeviceBase>, IDeviceBaseRepository
    {
        #region Constructors

        public DeviceBaseRepository(ARTDbContext context)
            : base(context)
        {
        }

        public async Task<DeviceBase> GetByKey(Guid deviceId, Guid deviceDatasheetId)
        {
            return await _context.Set<DeviceBase>().FindAsync(deviceId, deviceDatasheetId);
        }

        #endregion Constructors
    }
}