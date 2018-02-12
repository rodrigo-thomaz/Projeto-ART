namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceDebugRepository : RepositoryBase<ARTDbContext, DeviceDebug, Guid>, IDeviceDebugRepository
    {
        #region Constructors

        public DeviceDebugRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<DeviceDebug> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            return await _context.DeviceDebug.FindAsync(deviceTypeId, deviceDatasheetId, deviceId);
        }
    }
}