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

        public async Task<DeviceMQ> GetByKey(Guid deviceId, Guid deviceDatasheetId)
        {
            return await _context.DeviceMQ.FindAsync(deviceId, deviceDatasheetId);
        }

    }
}