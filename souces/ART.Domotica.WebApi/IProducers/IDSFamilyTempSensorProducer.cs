namespace ART.Domotica.WebApi.IProducers
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.WebApi.Models;

    public interface IDSFamilyTempSensorProducer
    {
        #region Methods

        Task GetAll(Guid applicationId, string session);

        Task GetResolutions(string session);

        Task SetHighAlarm(DSFamilyTempSensorSetHighAlarmModel request);

        Task SetLowAlarm(DSFamilyTempSensorSetLowAlarmModel request);

        Task SetResolution(DSFamilyTempSensorSetResolutionModel request);

        #endregion Methods
    }
}