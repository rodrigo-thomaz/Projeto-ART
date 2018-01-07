namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceDebugProfile : Profile
    {
        #region Constructors

        public DeviceDebugProfile()
        {
            CreateMap<DeviceDebug, DeviceDebugGetModel>()
                .ForMember(vm => vm.DeviceDebugId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.TelnetTCPPort, m => m.MapFrom(x => x.TelnetTCPPort))
                .ForMember(vm => vm.RemoteEnabled, m => m.MapFrom(x => x.RemoteEnabled))
                .ForMember(vm => vm.SerialEnabled, m => m.MapFrom(x => x.SerialEnabled))
                .ForMember(vm => vm.ResetCmdEnabled, m => m.MapFrom(x => x.ResetCmdEnabled))
                .ForMember(vm => vm.ShowDebugLevel, m => m.MapFrom(x => x.ShowDebugLevel))
                .ForMember(vm => vm.ShowTime, m => m.MapFrom(x => x.ShowTime))
                .ForMember(vm => vm.ShowProfiler, m => m.MapFrom(x => x.ShowProfiler))
                .ForMember(vm => vm.ShowColors, m => m.MapFrom(x => x.ShowColors));

            CreateMap<DeviceDebugSetTelnetTCPPortRequestContract, DeviceDebugSetTelnetTCPPortRequestIoTContract>()
                .ForMember(vm => vm.Value, m => m.MapFrom(x => x.Value));

            CreateMap<DeviceDebugSetTelnetTCPPortRequestContract, DeviceDebugSetTelnetTCPPortModel>()
                .ForMember(vm => vm.DeviceDebugId, m => m.MapFrom(x => x.DeviceDebugId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.Value, m => m.MapFrom(x => x.Value));

            CreateMap<DeviceDebugSetValueRequestContract, DeviceDebugSetValueRequestIoTContract>()
                .ForMember(vm => vm.Value, m => m.MapFrom(x => x.Value));

            CreateMap<DeviceDebugSetValueRequestContract, DeviceDebugSetValueModel>()
                .ForMember(vm => vm.DeviceDebugId, m => m.MapFrom(x => x.DeviceDebugId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.Value, m => m.MapFrom(x => x.Value));

            CreateMap<DeviceDebug, DeviceDebugDetailResponseContract>()
                .ForMember(vm => vm.RemoteEnabled, m => m.MapFrom(x => x.RemoteEnabled))
                .ForMember(vm => vm.SerialEnabled, m => m.MapFrom(x => x.SerialEnabled))
                .ForMember(vm => vm.ResetCmdEnabled, m => m.MapFrom(x => x.ResetCmdEnabled))
                .ForMember(vm => vm.ShowDebugLevel, m => m.MapFrom(x => x.ShowDebugLevel))
                .ForMember(vm => vm.ShowTime, m => m.MapFrom(x => x.ShowTime))
                .ForMember(vm => vm.ShowProfiler, m => m.MapFrom(x => x.ShowProfiler))
                .ForMember(vm => vm.ShowColors, m => m.MapFrom(x => x.ShowColors));
        }

        #endregion Constructors
    }
}