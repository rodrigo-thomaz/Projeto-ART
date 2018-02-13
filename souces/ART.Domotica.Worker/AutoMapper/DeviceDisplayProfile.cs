namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceDisplayProfile : Profile
    {
        #region Constructors

        public DeviceDisplayProfile()
        {
            CreateMap<DeviceDisplay, DeviceDisplayGetModel>()
                .ForMember(vm => vm.DeviceTypeId, m => m.MapFrom(x => x.DeviceTypeId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Enabled, m => m.MapFrom(x => x.Enabled));

            CreateMap<DeviceDisplaySetValueRequestContract, DeviceDisplaySetValueModel>()
                .ForMember(vm => vm.DeviceTypeId, m => m.MapFrom(x => x.DeviceTypeId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.DeviceId))
                .ForMember(vm => vm.Value, m => m.MapFrom(x => x.Value));

            CreateMap<DeviceDisplay, DeviceDisplayDetailResponseContract>()
                .ForMember(vm => vm.Enabled, m => m.MapFrom(x => x.Enabled));
        }

        #endregion Constructors
    }
}