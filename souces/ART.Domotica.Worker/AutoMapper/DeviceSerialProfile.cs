namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceSerialProfile : Profile
    {
        #region Constructors

        public DeviceSerialProfile()
        {
            CreateMap<DeviceSerial, DeviceSerialSetEnabledModel>()
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.DeviceId))
                .ForMember(vm => vm.DeviceSerialId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.Enabled, m => m.MapFrom(x => x.Enabled));
        }

        #endregion Constructors
    }
}