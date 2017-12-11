namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class HardwareProfile : Profile
    {
        #region Constructors

        public HardwareProfile()
        {
            CreateMap<HardwareBase, HardwareSetLabelModel>()
                .ForMember(vm => vm.HardwareId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Label, m => m.MapFrom(x => x.Label));
        }

        #endregion Constructors
    }
}