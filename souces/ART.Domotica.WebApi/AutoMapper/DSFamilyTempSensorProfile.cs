namespace ART.Domotica.WebApi.AutoMapper
{
    using ART.Domotica.Common.Contracts;
    using ART.Domotica.WebApi.Models;

    using global::AutoMapper;

    public class DSFamilyTempSensorProfile : Profile
    {
        #region Constructors

        public DSFamilyTempSensorProfile()
        {
            CreateMap<DSFamilyTempSensorSetResolutionModel, DSFamilyTempSensorSetResolutionContract>();
            CreateMap<DSFamilyTempSensorSetHighAlarmModel, DSFamilyTempSensorSetHighAlarmContract>();
            CreateMap<DSFamilyTempSensorSetLowAlarmModel, DSFamilyTempSensorSetLowAlarmContract>();
        }

        #endregion Constructors
    }
}