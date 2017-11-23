namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceNTPSettingRepository : RepositoryBase<ARTDbContext, DeviceNTPSetting, Guid>, IDeviceNTPSettingRepository
    {
        #region Constructors

        public DeviceNTPSettingRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}