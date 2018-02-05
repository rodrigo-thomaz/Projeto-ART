namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;
    using System.Linq;

    public class DeviceInApplicationProfile : Profile
    {
        #region Constructors

        public DeviceInApplicationProfile()
        {
            CreateMap<DeviceInApplication, DeviceInApplicationDetailResponseContract>()
                .ForMember(vm => vm.ApplicationId, m => m.MapFrom(x => x.ApplicationId));

            CreateMap<ESPDevice, DeviceInApplicationInsertResponseIoTContract>()
                .ForMember(vm => vm.ApplicationId, m => m.MapFrom(x => x.DevicesInApplication.Single().ApplicationId));

            CreateMap<DeviceBase, DeviceInApplicationRemoveModel>()
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Id));
        }

        #endregion Constructors
    }
}