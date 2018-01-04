namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
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
                .ForMember(vm => vm.Active, m => m.MapFrom(x => x.Active));

            CreateMap<DeviceDebugSetActiveRequestContract, DeviceDebugSetActiveModel>()
                .ForMember(vm => vm.DeviceDebugId, m => m.MapFrom(x => x.DeviceDebugId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.Active, m => m.MapFrom(x => x.Active));

            CreateMap<DeviceDebug, DeviceDebugDetailResponseContract>()
                .ForMember(vm => vm.Active, m => m.MapFrom(x => x.Active));
        }

        #endregion Constructors
    }
}