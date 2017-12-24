namespace ART.Domotica.Worker.AutoMapper.SI
{
    using ART.Domotica.Model.SI;
    using ART.Domotica.Repository.Entities.SI;

    using global::AutoMapper;

    public class UnitMeasurementScaleProfile : Profile
    {
        #region Constructors

        public UnitMeasurementScaleProfile()
        {
            CreateMap<UnitMeasurementScale, UnitMeasurementScaleGetModel>()
                .ForMember(vm => vm.UnitMeasurementId, m => m.MapFrom(x => x.UnitMeasurementId))
                .ForMember(vm => vm.UnitMeasurementTypeId, m => m.MapFrom(x => x.UnitMeasurementTypeId))
                .ForMember(vm => vm.NumericalScalePrefixId, m => m.MapFrom(x => x.NumericalScalePrefixId))
                .ForMember(vm => vm.NumericalScaleTypeId, m => m.MapFrom(x => x.NumericalScaleTypeId))
                .ForMember(vm => vm.Name, m => m.MapFrom(x => x.Name));
        }

        #endregion Constructors
    }
}