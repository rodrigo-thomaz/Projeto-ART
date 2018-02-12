namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Enums;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceInApplicationRepository : RepositoryBase<ARTDbContext, DeviceInApplication>, IDeviceInApplicationRepository
    {
        #region Constructors

        public DeviceInApplicationRepository(ARTDbContext context)
            : base(context)
        {
        }

        public async Task<DeviceInApplication> GetByKey(Guid applicationId, DeviceTypeEnum deviceTypeId, Guid deviceDatasheetId, Guid deviceId)
        {
            return await _context.DeviceInApplication.FindAsync(applicationId, deviceTypeId, deviceDatasheetId, deviceId);
        }

        #endregion Constructors
    }
}