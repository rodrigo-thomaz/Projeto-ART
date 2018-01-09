namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceMQProfile : Profile
    {
        #region Constructors

        public DeviceMQProfile()
        {
            CreateMap<DeviceMQ, DeviceMQDetailResponseContract>()
                .ForMember(vm => vm.User, m => m.MapFrom(x => x.User))
                .ForMember(vm => vm.Password, m => m.MapFrom(x => x.Password))
                .ForMember(vm => vm.ClientId, m => m.MapFrom(x => x.ClientId))
                .ForMember(vm => vm.DeviceTopic, m => m.MapFrom(x => x.Topic));

            CreateMap<DeviceMQ, DeviceMQGetModel>()
                .ForMember(vm => vm.DeviceMQId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.User, m => m.MapFrom(x => x.User))
                .ForMember(vm => vm.ClientId, m => m.MapFrom(x => x.ClientId))
                .ForMember(vm => vm.Topic, m => m.MapFrom(x => x.Topic));
        }

        #endregion Constructors
    }
}