namespace ART.Domotica.Repository.Interfaces
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceBrokerSettingRepository : IRepository<ARTDbContext, DeviceBrokerSetting, Guid>
    {
    }
}