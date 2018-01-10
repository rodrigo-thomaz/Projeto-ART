namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorInDeviceProfile : Profile
    {
        #region Constructors

        public SensorInDeviceProfile()
        {
            CreateMap<SensorInDevice, SensorInDeviceGetModel>()
                .ForMember(vm => vm.DeviceSensorsId, m => m.MapFrom(x => x.DeviceSensorsId))
                .ForMember(vm => vm.DeviceDatasheetId, m => m.MapFrom(x => x.DeviceDatasheetId))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId));

            CreateMap<SensorInDevice, SensorInDeviceSetOrdinationModel>()
                .ForMember(vm => vm.DeviceSensorsId, m => m.MapFrom(x => x.DeviceSensorsId))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.Ordination, m => m.MapFrom(x => x.Ordination));

            CreateMap<SensorInDevice, SensorInDeviceGetResponseIoTContract>()
                .ForMember(vm => vm.DeviceAddress, m => m.ResolveUsing(src => {
                    if (src.Sensor.SensorTempDSFamily == null)
                    {
                        return new short[0];
                    }

                    var split = src.Sensor.SensorTempDSFamily.DeviceAddress.Split(':');
                    var result = new short[8];
                    for (int i = 0; i < 8; i++)
                    {
                        result[i] = short.Parse(split[i]);
                    }
                    return result;
                }))
                .ForMember(vm => vm.ResolutionBits, m => m.MapFrom(x => x.Sensor.SensorTempDSFamily.SensorTempDSFamilyResolution.Bits))
                .ForMember(vm => vm.Label, m => m.MapFrom(x => x.Sensor.Label))
                .ForMember(vm => vm.LowChartLimiterCelsius, m => m.MapFrom(x => x.Sensor.SensorUnitMeasurementScale.ChartLimiterMin))
                .ForMember(vm => vm.HighChartLimiterCelsius, m => m.MapFrom(x => x.Sensor.SensorUnitMeasurementScale.ChartLimiterMax))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.Sensor.Id));
        }

        #endregion Constructors
    }
}