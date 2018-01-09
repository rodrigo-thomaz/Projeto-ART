namespace ART.Domotica.Worker.AutoMapper
{
    using System.Linq;

    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
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
                .ForMember(vm => vm.PublishIntervalInMilliSeconds, m => m.MapFrom(x => x.PublishIntervalInMilliSeconds))
                .ForMember(vm => vm.SensorInDevice, m => m.MapFrom(x => x.SensorInDevice.OrderBy(y => y.Ordination)));

            CreateMap<DeviceSensors, DeviceSensorsSetPublishIntervalInMilliSecondsModel>()
                .ForMember(vm => vm.DeviceSensorsId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.PublishIntervalInMilliSeconds, m => m.MapFrom(x => x.PublishIntervalInMilliSeconds));

            CreateMap<DeviceSensors, DeviceSensorsDetailResponseContract>()
                .ForMember(vm => vm.PublishIntervalInMilliSeconds, m => m.MapFrom(x => x.PublishIntervalInMilliSeconds));

            CreateMap<DeviceSensors, SetValueRequestIoTContract<int>>()
                .ForMember(vm => vm.Value, m => m.MapFrom(x => x.PublishIntervalInMilliSeconds));
        }

        #endregion Constructors
    }
}