namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorProfile : Profile
    {
        #region Constructors

        public SensorProfile()
        {
            CreateMap<Sensor, SensorGetModel>()
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.Label, m => m.MapFrom(x => x.Label))
                .ForMember(vm => vm.SensorTriggers, m => m.MapFrom(x => x.SensorTriggers))
                .ForMember(vm => vm.SensorTempDSFamily, m => m.MapFrom(x => x.SensorTempDSFamily))
                .ForMember(vm => vm.SensorUnitMeasurementScale, m => m.MapFrom(x => x.SensorUnitMeasurementScale));

            CreateMap<Sensor, SensorGetAllByHardwareInApplicationIdResponseIoTContract>()
                .ForMember(vm => vm.DeviceAddress, m => m.ResolveUsing(src => {
                    var split = src.SensorTempDSFamily.DeviceAddress.Split(':');
                    var result = new short[8];
                    for (int i = 0; i < 8; i++)
                    {
                        result[i] = short.Parse(split[i]);
                    }
                    return result;
                }))
                .ForMember(vm => vm.ResolutionBits, m => m.MapFrom(x => x.SensorTempDSFamily.SensorTempDSFamilyResolution.Bits))
                .ForMember(vm => vm.LowChartLimiterCelsius, m => m.MapFrom(x => x.SensorUnitMeasurementScale.ChartLimiterMin))
                .ForMember(vm => vm.HighChartLimiterCelsius, m => m.MapFrom(x => x.SensorUnitMeasurementScale.ChartLimiterMax))
                .ForMember(vm => vm.SensorTempDSFamilyId, m => m.MapFrom(x => x.Id));

            CreateMap<Sensor, SensorSetLabelModel>()
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.Label, m => m.MapFrom(x => x.Label));
        }

        #endregion Constructors
    }
}