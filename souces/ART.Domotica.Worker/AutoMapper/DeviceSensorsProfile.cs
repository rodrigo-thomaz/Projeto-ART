namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceSensorsProfile : Profile
    {
        #region Constructors

        public DeviceSensorsProfile()
        {
            CreateMap<DeviceSensors, DeviceSensorsGetModel>()
                .ForMember(vm => vm.DeviceSensorsId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.PublishIntervalInSeconds, m => m.MapFrom(x => x.PublishIntervalInSeconds));
        }

        #endregion Constructors
    }
}