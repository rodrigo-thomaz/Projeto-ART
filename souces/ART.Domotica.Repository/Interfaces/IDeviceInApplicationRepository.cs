namespace ART.Domotica.Repository.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceInApplicationRepository : IRepository<ARTDbContext, DeviceInApplication>
    {
        Task<DeviceInApplication> GetByKey(Guid applicationId, Guid deviceBaseId);
    }
}