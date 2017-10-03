using ART.MQ.DistributedServices.Models;
using System.Threading.Tasks;

namespace ART.MQ.DistributedServices.IProducers
{
    public interface IDSFamilyTempSensorProducer
    {
        Task GetResolutions(string session);
        Task SetHighAlarm(DSFamilyTempSensorSetHighAlarmModel request);
        Task SetLowAlarm(DSFamilyTempSensorSetLowAlarmModel request);
        Task SetResolution(DSFamilyTempSensorSetResolutionModel request);
    }
}