namespace ART.Domotica.Worker.AutoMapper
{
    using System.Linq;
    using ART.Domotica.Contract;
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
                .ForMember(vm => vm.PublishIntervalInSeconds, m => m.MapFrom(x => x.PublishIntervalInSeconds))
                .ForMember(vm => vm.SensorInDevice, m => m.MapFrom(x => x.SensorInDevice.OrderBy(y => y.Ordination)));

            CreateMap<DeviceSensors, DeviceSensorsSetPublishIntervalInSecondsModel>()
                .ForMember(vm => vm.DeviceSensorsId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.PublishIntervalInSeconds, m => m.MapFrom(x => x.PublishIntervalInSeconds));

            CreateMap<DeviceSensors, DeviceSensorsDetailResponseContract>()
                .ForMember(vm => vm.PublishIntervalInSeconds, m => m.MapFrom(x => x.PublishIntervalInSeconds));
        }

        #endregion Constructors
    }
}