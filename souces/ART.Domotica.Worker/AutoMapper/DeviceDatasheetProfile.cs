namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceDatasheetProfile : Profile
    {
        #region Constructors

        public DeviceDatasheetProfile()
        {
            CreateMap<DeviceDatasheet, DeviceDatasheetGetModel>()
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name))
                .ForMember(vm => vm.HasDeviceSerial, m => m.MapFrom(x => x.HasDeviceSerial))
                .ForMember(vm => vm.HasDeviceSensors, m => m.MapFrom(x => x.HasDeviceSensors));
        }

        #endregion Constructors
    }
}