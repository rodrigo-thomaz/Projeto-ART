namespace ART.Domotica.WebApi.IProducers
{
    using System;
    using System.Threading.Tasks;

    using ART.Domotica.WebApi.Models;

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