namespace ART.Domotica.Worker.AutoMapper
{
    using System.Linq;

    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorProfile : Profile
    {
        #region Constructors

        public SensorProfile()
        {
            CreateMap<Sensor, SensorDetailModel>()
                .ForMember(vm => vm.SensorChartLimiter, m => m.MapFrom(x => x.SensorChartLimiter));

            CreateMap<Sensor, SensorSetUnitMeasurementCompletedModel>()
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.SensorsInDevice.Single().DeviceBaseId))
                .ForMember(vm => vm.UnitMeasurementId, m => m.MapFrom(x => x.UnitMeasurementId));

            CreateMap<Sensor, SensorGetAllByDeviceInApplicationIdResponseIoTContract>()
                .ForMember(vm => vm.DeviceAddress, m => m.ResolveUsing(src => {
                    var split = src.DSFamilyTempSensor.DeviceAddress.Split(':');
                    var result = new short[8];
                    for (int i = 0; i < 8; i++)
                    {
                        result[i] = short.Parse(split[i]);
                    }
                    return result;
                }))
                .ForMember(vm => vm.ResolutionBits, m => m.MapFrom(x => x.DSFamilyTempSensor.DSFamilyTempSensorResolution.Bits))
                .ForMember(vm => vm.LowChartLimiterCelsius, m => m.MapFrom(x => x.SensorChartLimiter.Min))
                .ForMember(vm => vm.HighChartLimiterCelsius, m => m.MapFrom(x => x.SensorChartLimiter.Max))
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.Id));
        }

        #endregion Constructors
    }
}