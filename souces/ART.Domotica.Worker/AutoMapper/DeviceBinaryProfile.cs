namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceBinaryProfile : Profile
    {
        #region Constructors

        public DeviceBinaryProfile()
        {
            CreateMap<DeviceBinary, DeviceBinaryGetModel>()
                .ForMember(vm => vm.DeviceBinaryId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.CreateDate, m => m.MapFrom(x => x.DeviceDatasheetBinary.CreateDate))
                .ForMember(vm => vm.Version, m => m.MapFrom(x => x.DeviceDatasheetBinary.Version))
                .ForMember(vm => vm.UpdateDate, m => m.MapFrom(x => x.UpdateDate));
        }

        #endregion Constructors
    }
}