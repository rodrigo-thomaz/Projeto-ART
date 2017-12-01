namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorUnitMeasurementDefaultProfile : Profile
    {
        #region Constructors

        public SensorUnitMeasurementDefaultProfile()
        {
            CreateMap<SensorUnitMeasurementDefault, SensorUnitMeasurementDefaultDetailModel>()
                .ForMember(vm => vm.SensorUnitMeasurementDefaultId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.UnitMeasurementId, m => m.MapFrom(x => x.UnitMeasurementId))
                .ForMember(vm => vm.UnitMeasurementTypeId, m => m.MapFrom(x => x.UnitMeasurementTypeId))
                .ForMember(vm => vm.Max, m => m.MapFrom(x => x.Max))
                .ForMember(vm => vm.Min, m => m.MapFrom(x => x.Min));
        }

        #endregion Constructors
    }
}