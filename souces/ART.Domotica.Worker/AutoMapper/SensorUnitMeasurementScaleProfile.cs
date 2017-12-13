namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorUnitMeasurementScaleProfile : Profile
    {
        #region Constructors

        public SensorUnitMeasurementScaleProfile()
        {
            CreateMap<SensorUnitMeasurementScale, SensorUnitMeasurementScaleGetModel>()
                .ForMember(vm => vm.SensorUnitMeasurementScaleId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.UnitMeasurementId, m => m.MapFrom(x => x.UnitMeasurementId))
                .ForMember(vm => vm.UnitMeasurementTypeId, m => m.MapFrom(x => x.UnitMeasurementTypeId))
                .ForMember(vm => vm.NumericalScalePrefixId, m => m.MapFrom(x => x.NumericalScalePrefixId))
                .ForMember(vm => vm.NumericalScaleTypeId, m => m.MapFrom(x => x.NumericalScaleTypeId))
                .ForMember(vm => vm.CountryId, m => m.MapFrom(x => x.CountryId))
                .ForMember(vm => vm.RangeMax, m => m.MapFrom(x => x.RangeMax))
                .ForMember(vm => vm.RangeMin, m => m.MapFrom(x => x.RangeMin))
                .ForMember(vm => vm.ChartLimiterMax, m => m.MapFrom(x => x.ChartLimiterMax))
                .ForMember(vm => vm.ChartLimiterMin, m => m.MapFrom(x => x.ChartLimiterMin));

            CreateMap<SensorUnitMeasurementScaleSetValueRequestContract, SensorUnitMeasurementScaleSetValueRequestIoTContract>();
            CreateMap<SensorUnitMeasurementScaleSetValueRequestContract, SensorUnitMeasurementScaleSetValueModel>();
        }

        #endregion Constructors
    }
}