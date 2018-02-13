namespace ART.Domotica.Worker.AutoMapper
{
    using System.Linq;

    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Utils;

    using global::AutoMapper;

    public class ESPDeviceProfile : Profile
    {
        #region Constructors

        public ESPDeviceProfile()
        {
            CreateMap<ESPDevice, ESPDeviceGetModel>()
                .ForMember(vm => vm.DeviceTypeId, m => m.MapFrom(x => x.DeviceTypeId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.DevicesInApplication.Single().DeviceId))
                .ForMember(vm => vm.Label, m => m.MapFrom(x => x.Label))
                .ForMember(vm => vm.DeviceNTP, m => m.MapFrom(x => x.DeviceNTP))
                .ForMember(vm => vm.DeviceSerial, m => m.MapFrom(x => x.DeviceSerial))
                .ForMember(vm => vm.DeviceWiFi, m => m.MapFrom(x => x.DeviceWiFi))
                .ForMember(vm => vm.DeviceMQ, m => m.MapFrom(x => x.DeviceMQ))
                .ForMember(vm => vm.DeviceBinary, m => m.MapFrom(x => x.DeviceBinary))
                .ForMember(vm => vm.DeviceDebug, m => m.MapFrom(x => x.DeviceDebug))
                .ForMember(vm => vm.DeviceDisplay, m => m.MapFrom(x => x.DeviceDisplay))
                .ForMember(vm => vm.DeviceSensor, m => m.MapFrom(x => x.DeviceSensor));

            CreateMap<ESPDevice, ESPDeviceGetByPinModel>()
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId));

            CreateMap<ESPDevice, ESPDeviceGetConfigurationsRPCResponseContract>()
                .ForMember(vm => vm.DeviceTypeId, m => m.MapFrom(x => x.DeviceTypeId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceInApplication, m => m.MapFrom(x => x.DevicesInApplication.FirstOrDefault()))
                .ForMember(vm => vm.DeviceDebug, m => m.MapFrom(x => x.DeviceDebug))
                .ForMember(vm => vm.DeviceWiFi, m => m.MapFrom(x => x.DeviceWiFi))
                .ForMember(vm => vm.DeviceMQ, m => m.MapFrom(x => x.DeviceMQ))
                .ForMember(vm => vm.DeviceNTP, m => m.MapFrom(x => x.DeviceNTP))
                .ForMember(vm => vm.Label, m => m.MapFrom(x => x.Label))
                .ForMember(vm => vm.HasDeviceSerial, m => m.MapFrom(x => x.DeviceDatasheet.HasDeviceSerial))
                .ForMember(vm => vm.HasDeviceSensor, m => m.MapFrom(x => x.DeviceDatasheet.HasDeviceSensor));

            CreateMap<ESPDevice, ESPDeviceUpdatePinsResponseIoTContract>()
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId));

            CreateMap<ESPDevice, ESPDeviceAdminGetModel>()
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => DateTimeConverter.ToUniversalTimestamp(x.CreateDate)))
                .ForMember(vm => vm.InApplication, m => m.MapFrom(x => x.DevicesInApplication.Any()));
        }

        #endregion Constructors
    }
}