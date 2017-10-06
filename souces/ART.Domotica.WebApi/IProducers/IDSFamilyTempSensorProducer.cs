namespace ART.Domotica.WebApi.IProducers
{
    using System.Threading.Tasks;

    using ART.Domotica.Contract;

    public interface IDSFamilyTempSensorProducer
    {
        #region Methods

        Task GetAll(DSFamilyTempSensorGetAllContract contract);

        Task GetResolutions(DSFamilyTempSensorGetResolutionsContract contract);

        Task SetHighAlarm(DSFamilyTempSensorSetHighAlarmContract contract);

        Task SetLowAlarm(DSFamilyTempSensorSetLowAlarmContract contract);

        Task SetResolution(DSFamilyTempSensorSetResolutionContract contract);

        #endregion Methods
    }
}