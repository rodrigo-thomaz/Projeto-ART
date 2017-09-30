using ART.MQ.Consumer.Entities;
using System;
using System.Threading.Tasks;

namespace ART.MQ.Consumer.IRepositories
{
    public interface IDSFamilyTempSensorRepository : IRepository<DSFamilyTempSensor, Guid>
    {
        Task<DSFamilyTempSensor> GetByDeviceAddress(string deviceAddress);
    }
}
