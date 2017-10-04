namespace ART.MQ.DistributedServices.AutoMapper
{
    using ART.MQ.Common.Contracts;
    using ART.MQ.DistributedServices.Models;

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