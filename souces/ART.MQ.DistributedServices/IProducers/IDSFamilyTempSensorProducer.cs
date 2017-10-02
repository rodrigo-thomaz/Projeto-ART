using ART.MQ.DistributedServices.Models;

namespace ART.MQ.DistributedServices.IProducers
{
    public interface IDSFamilyTempSensorProducer
    {
        void SetHighAlarm(DSFamilyTempSensorSetHighAlarmModel request);
        void SetLowAlarm(DSFamilyTempSensorSetLowAlarmModel request);
        void SetResolution(DSFamilyTempSensorSetResolutionModel request);
    }
}