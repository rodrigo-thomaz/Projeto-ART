namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorProfile : Profile
    {
        #region Constructors

        public SensorProfile()
        {
            CreateMap<Sensor, SensorDetailModel>()
                .ForMember(vm => vm.SensorChartLimiter, m => m.MapFrom(x => x.SensorChartLimiter));
        }

        #endregion Constructors
    }
}