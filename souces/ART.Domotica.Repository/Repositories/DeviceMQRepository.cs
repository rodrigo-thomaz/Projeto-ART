namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceMQRepository : RepositoryBase<ARTDbContext, DeviceMQ, Guid>, IDeviceMQRepository
    {
        #region Constructors

        public DeviceMQRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors

        public async Task<DeviceMQ> GetByKey(DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            return await _context.DeviceMQ.FindAsync(deviceTypeId, deviceDatasheetId, deviceId);
        }

    }
}