namespace ART.MQ.DistributedServices.IProducers
{
    using System;
    using System.Threading.Tasks;

    using ART.MQ.DistributedServices.Models;

    public interface IDSFamilyTempSensorProducer
    {
        #region Methods

        Task Get(Guid dsFamilyTempSensorId, string session);

        Task GetResolutions(string session);

        Task SetHighAlarm(DSFamilyTempSensorSetHighAlarmModel request);

        Task SetLowAlarm(DSFamilyTempSensorSetLowAlarmModel request);

        Task SetResolution(DSFamilyTempSensorSetResolutionModel request);

        #endregion Methods
    }
}