namespace ART.Domotica.Worker.AutoMapper.SI
{
    using ART.Domotica.Model.SI;
    using ART.Domotica.Repository.Entities.SI;

    using global::AutoMapper;

    public class NumericalScaleProfile : Profile
    {
        #region Constructors

        public NumericalScaleProfile()
        {
            CreateMap<NumericalScale, NumericalScaleDetailModel>()
                .ForMember(vm => vm.NumericalScalePrefixId, m => m.MapFrom(x => x.NumericalScalePrefixId))
                .ForMember(vm => vm.NumericalScaleTypeId, m => m.MapFrom(x => x.NumericalScaleTypeId))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name))
                .ForMember(vm => vm.ScientificNotationBase, m => m.MapFrom(x => x.ScientificNotationBase))
                .ForMember(vm => vm.ScientificNotationExponent, m => m.MapFrom(x => x.ScientificNotationExponent));
        }

        #endregion Constructors
    }
}