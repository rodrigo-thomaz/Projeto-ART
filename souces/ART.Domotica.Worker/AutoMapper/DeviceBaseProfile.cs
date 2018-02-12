namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceBaseProfile : Profile
    {
        #region Constructors

        public DeviceBaseProfile()
        {
            CreateMap<DeviceBase, DeviceSetLabelModel>()
                .ForMember(vm => vm.DeviceTypeId, m => m.MapFrom(x => x.DeviceTypeId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Label, m => m.MapFrom(x => x.Label));
        }

        #endregion Constructors
    }
}