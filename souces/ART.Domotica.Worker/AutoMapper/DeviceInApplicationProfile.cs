namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceInApplicationProfile : Profile
    {
        #region Constructors

        public DeviceInApplicationProfile()
        {
            CreateMap<DeviceInApplication, DeviceInApplicationDetailResponseContract>()
                .ForMember(vm => vm.ApplicationId, m => m.MapFrom(x => x.ApplicationId));
        }

        #endregion Constructors
    }
}