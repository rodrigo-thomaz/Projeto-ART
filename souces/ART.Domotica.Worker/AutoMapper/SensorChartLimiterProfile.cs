namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorChartLimiterProfile : Profile
    {
        #region Constructors

        public SensorChartLimiterProfile()
        {
            CreateMap<SensorChartLimiter, SensorChartLimiterDetailModel>();
            CreateMap<SensorChartLimiterSetValueRequestContract, SensorChartLimiterSetValueRequestIoTContract>();
            CreateMap<SensorChartLimiterSetValueRequestContract, SensorChartLimiterSetValueCompletedModel>();
        }

        #endregion Constructors
    }
}