namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceSerialProfile : Profile
    {
        #region Constructors

        public DeviceSerialProfile()
        {
            CreateMap<DeviceSerial, DeviceSerialSetEnabledModel>()
                .ForMember(vm => vm.DeviceTypeId, m => m.MapFrom(x => x.DeviceTypeId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.DeviceId))
                .ForMember(vm => vm.DeviceSerialId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Enabled, m => m.MapFrom(x => x.Enabled));

            CreateMap<DeviceSerialSetPinRequestContract, DeviceSerialSetPinModel>()
                .ForMember(vm => vm.DeviceTypeId, m => m.MapFrom(x => x.DeviceTypeId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.DeviceId))
                .ForMember(vm => vm.DeviceSerialId, m => m.MapFrom(x => x.DeviceSerialId))
                .ForMember(vm => vm.Value, m => m.MapFrom(x => x.Value))
                .ForMember(vm => vm.Direction, m => m.MapFrom(x => x.Direction));

            CreateMap<DeviceSerialSetPinRequestContract, DeviceSerialSetPinRequestIoTContract>()
                .ForMember(vm => vm.DeviceSerialId, m => m.MapFrom(x => x.DeviceSerialId))
                .ForMember(vm => vm.Value, m => m.MapFrom(x => x.Value))
                .ForMember(vm => vm.Direction, m => m.MapFrom(x => x.Direction));

            CreateMap<DeviceSerialSetEnabledRequestContract, DeviceSerialSetEnabledRequestIoTContract>()
                .ForMember(vm => vm.DeviceSerialId, m => m.MapFrom(x => x.DeviceSerialId))
                .ForMember(vm => vm.Value, m => m.MapFrom(x => x.Enabled));

            CreateMap<DeviceSerial, DeviceSerialGetModel>()
                .ForMember(vm => vm.DeviceTypeId, m => m.MapFrom(x => x.DeviceTypeId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.DeviceId))
                .ForMember(vm => vm.DeviceSerialId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Index, m => m.MapFrom(x => x.Index))
                .ForMember(vm => vm.Enabled, m => m.MapFrom(x => x.Enabled))
                .ForMember(vm => vm.SerialMode, m => m.MapFrom(x => x.SerialMode))
                .ForMember(vm => vm.PinRX, m => m.MapFrom(x => x.PinRX))
                .ForMember(vm => vm.PinTX, m => m.MapFrom(x => x.PinTX))
                .ForMember(vm => vm.AllowPinSwapRX, m => m.MapFrom(x => x.AllowPinSwapRX))
                .ForMember(vm => vm.AllowPinSwapTX, m => m.MapFrom(x => x.AllowPinSwapTX));

            CreateMap<DeviceSerial, DeviceSerialGetResponseIoTContract>()
                .ForMember(vm => vm.DeviceSerialId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Index, m => m.MapFrom(x => x.Index))
                .ForMember(vm => vm.Enabled, m => m.MapFrom(x => x.Enabled))
                .ForMember(vm => vm.SerialMode, m => m.MapFrom(x => x.SerialMode))
                .ForMember(vm => vm.PinRX, m => m.MapFrom(x => x.PinRX))
                .ForMember(vm => vm.PinTX, m => m.MapFrom(x => x.PinTX))
                .ForMember(vm => vm.AllowPinSwapRX, m => m.MapFrom(x => x.AllowPinSwapRX))
                .ForMember(vm => vm.AllowPinSwapTX, m => m.MapFrom(x => x.AllowPinSwapTX));
        }

        #endregion Constructors
    }
}