namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DeviceTypeProfile : Profile
    {
        #region Constructors

        public DeviceTypeProfile()
        {
            CreateMap<DeviceType, DeviceTypeGetModel>()
                .ForMember(vm => vm.DeviceTypeId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name));
        }

        #endregion Constructors
    }
}