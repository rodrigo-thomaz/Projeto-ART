namespace ART.Domotica.Repository.Interfaces
{
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorInDeviceRepository : IRepository<ARTDbContext, SensorInDevice>
    {
    }
}