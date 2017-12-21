namespace ART.Domotica.Repository.Repositories
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;
    using ART.Domotica.Enums;

    public class DeviceInApplicationRepository : RepositoryBase<ARTDbContext, DeviceInApplication>, IDeviceInApplicationRepository
    {
        #region Constructors

        public DeviceInApplicationRepository(ARTDbContext context)
            : base(context)
        {
        }

        public async Task<DeviceInApplication> GetByKey(Guid applicationId, Guid deviceId, DeviceDatasheetEnum deviceDatasheetId)
        {
            return await _context.DeviceInApplication.FindAsync(applicationId, deviceId, deviceDatasheetId);
        }

        #endregion Constructors
    }
}