namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorDatasheetUnitMeasurementDefaultProfile : Profile
    {
        #region Constructors

        public SensorDatasheetUnitMeasurementDefaultProfile()
        {
            CreateMap<SensorDatasheetUnitMeasurementDefault, SensorDatasheetUnitMeasurementDefaultGetModel>()
                .ForMember(vm => vm.SensorDatasheetUnitMeasurementDefaultId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.UnitMeasurementId, m => m.MapFrom(x => x.UnitMeasurementId))
                .ForMember(vm => vm.UnitMeasurementTypeId, m => m.MapFrom(x => x.UnitMeasurementTypeId))
                .ForMember(vm => vm.NumericalScalePrefixId, m => m.MapFrom(x => x.NumericalScalePrefixId))
                .ForMember(vm => vm.NumericalScaleTypeId, m => m.MapFrom(x => x.NumericalScaleTypeId))
                .ForMember(vm => vm.Max, m => m.MapFrom(x => x.Max))
                .ForMember(vm => vm.Min, m => m.MapFrom(x => x.Min));

            CreateMap<SensorDatasheetUnitMeasurementDefault, SensorDatasheetUnitMeasurementDefaultGetResponseIoTContract>()
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId));
        }

        #endregion Constructors
    }
}