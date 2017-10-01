using ART.MQ.Worker.Entities;
using System;
using System.Threading.Tasks;

namespace ART.MQ.Worker.IRepositories
{
    public interface IDSFamilyTempSensorRepository : IRepository<DSFamilyTempSensor, Guid>
    {
        Task<DSFamilyTempSensor> GetByDeviceAddress(string deviceAddress);
    }
}
