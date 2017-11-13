namespace ART.Domotica.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.MQ.Contract;

    public interface IDSFamilyTempSensorDomain
    {
        #region Methods

        Task<List<DSFamilyTempSensorGetAllByHardwareInApplicationIdResponseContract>> GetAllByHardwareInApplicationId(Guid hardwareApplicationId);

        Task<List<DSFamilyTempSensorResolutionGetAllModel>> GetAllResolutions();

        Task<SensorsInDevice> GetDeviceFromSensor(Guid dsFamilyTempSensorId);

        Task<List<DSFamilyTempSensorGetListModel>> GetList(AuthenticatedMessageContract message);

        Task<List<DSFamilyTempSensorGetListInApplicationModel>> GetListInApplication(Guid applicationUserId);

        Task SetHighAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetHighAlarmContract> message);

        Task SetLowAlarm(AuthenticatedMessageContract<DSFamilyTempSensorSetLowAlarmContract> message);

        Task SetResolution(AuthenticatedMessageContract<DSFamilyTempSensorSetResolutionContract> message);

        #endregion Methods
    }
}