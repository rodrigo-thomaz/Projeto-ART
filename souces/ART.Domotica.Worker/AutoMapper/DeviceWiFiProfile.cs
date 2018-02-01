namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceWiFiProfile : Profile
    {
        #region Constructors

        public DeviceWiFiProfile()
        {
            CreateMap<DeviceWiFi, DeviceWiFiGetModel>()
                .ForMember(vm => vm.DeviceWiFiId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.HostName, m => m.MapFrom(x => x.HostName))
                .ForMember(vm => vm.PublishIntervalInMilliSeconds, m => m.MapFrom(x => x.PublishIntervalInMilliSeconds));

            CreateMap<DeviceWiFiSetHostNameRequestContract, DeviceWiFiSetHostNameModel>()
                .ForMember(vm => vm.DeviceWiFiId, m => m.MapFrom(x => x.DeviceWiFiId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.HostName, m => m.MapFrom(x => x.HostName));

            CreateMap<DeviceWiFi, DeviceWiFiDetailResponseContract>()
                .ForMember(vm => vm.HostName, m => m.MapFrom(x => x.HostName))
                .ForMember(vm => vm.PublishIntervalInMilliSeconds, m => m.MapFrom(x => x.PublishIntervalInMilliSeconds));

            CreateMap<DeviceWiFi, DeviceSetPublishIntervalInMilliSecondsModel>()
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.PublishIntervalInMilliSeconds, m => m.MapFrom(x => x.PublishIntervalInMilliSeconds));
        }

        #endregion Constructors
    }
}