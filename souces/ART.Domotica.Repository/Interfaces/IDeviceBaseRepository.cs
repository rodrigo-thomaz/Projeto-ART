namespace ART.Domotica.Repository.Interfaces
{
    using System;

    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface IDeviceBaseRepository : IRepository<ARTDbContext, DeviceBase, Guid>
    {
    }
}