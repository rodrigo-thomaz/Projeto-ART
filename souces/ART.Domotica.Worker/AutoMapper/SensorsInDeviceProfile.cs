namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorsInDeviceProfile : Profile
    {
        #region Constructors

        public SensorsInDeviceProfile()
        {
            CreateMap<SensorsInDevice, SensorsInDeviceGetModel>()
                .ForMember(vm => vm.DeviceSensorsId, m => m.MapFrom(x => x.DeviceSensorsId))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.Ordination, m => m.MapFrom(x => x.Ordination));
        }

        #endregion Constructors
    }
}