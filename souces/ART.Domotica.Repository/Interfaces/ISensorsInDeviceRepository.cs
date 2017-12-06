namespace ART.Domotica.Repository.Interfaces
{
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Repository;

    public interface ISensorsInDeviceRepository : IRepository<ARTDbContext, SensorsInDevice>
    {
    }
}