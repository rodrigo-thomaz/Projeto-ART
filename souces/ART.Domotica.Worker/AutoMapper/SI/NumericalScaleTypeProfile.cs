namespace ART.Domotica.Worker.AutoMapper.SI
{
    using ART.Domotica.Model.SI;
    using ART.Domotica.Repository.Entities.SI;

    using global::AutoMapper;

    public class NumericalScaleTypeProfile : Profile
    {
        #region Constructors

        public NumericalScaleTypeProfile()
        {
            CreateMap<NumericalScaleType, NumericalScaleTypeDetailModel>()
                .ForMember(vm => vm.NumericalScaleTypeId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name));
        }

        #endregion Constructors
    }
}