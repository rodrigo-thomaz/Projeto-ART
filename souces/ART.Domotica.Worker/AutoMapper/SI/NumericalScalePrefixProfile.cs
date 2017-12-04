namespace ART.Domotica.Worker.AutoMapper.SI
{
    using ART.Domotica.Model.SI;
    using ART.Domotica.Repository.Entities.SI;

    using global::AutoMapper;

    public class NumericalScalePrefixProfile : Profile
    {
        #region Constructors

        public NumericalScalePrefixProfile()
        {
            CreateMap<NumericalScalePrefix, NumericalScalePrefixGetModel>()
                .ForMember(vm => vm.NumericalScalePrefixId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name))
                .ForMember(vm => vm.Symbol, m => m.MapFrom(x => x.Symbol));
        }

        #endregion Constructors
    }
}