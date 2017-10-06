namespace ART.Domotica.WebApi.IProducers
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;
    using ART.Infra.CrossCutting.MQ;

    public interface IDSFamilyTempSensorProducer
    {
        #region Methods

        Task GetAll(AuthenticatedContract<DSFamilyTempSensorGetAllContract> contract);

        Task GetResolutions(AuthenticatedContract<DSFamilyTempSensorGetResolutionsContract> contract);

        Task SetHighAlarm(AuthenticatedContract<DSFamilyTempSensorSetHighAlarmContract> contract);

        Task SetLowAlarm(AuthenticatedContract<DSFamilyTempSensorSetLowAlarmContract> contract);

        Task SetResolution(AuthenticatedContract<DSFamilyTempSensorSetResolutionContract> contract);

        #endregion Methods
    }
}