namespace ART.Domotica.Repository.Repositories
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Domotica.Repository.Interfaces;
    using ART.Infra.CrossCutting.Repository;

    public class DeviceBrokerSettingRepository : RepositoryBase<ARTDbContext, DeviceBrokerSetting, Guid>, IDeviceBrokerSettingRepository
    {
        #region Constructors

        public DeviceBrokerSettingRepository(ARTDbContext context)
            : base(context)
        {
        }

        #endregion Constructors
    }
}