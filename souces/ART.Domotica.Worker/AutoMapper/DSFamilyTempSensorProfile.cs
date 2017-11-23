namespace ART.Domotica.Worker.AutoMapper
{
    using System.Linq;

    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class DSFamilyTempSensorProfile : Profile
    {
        #region Constructors

        public DSFamilyTempSensorProfile()
        {
            CreateMap<TempSensorAlarm, TempSensorAlarmResponseIoTContract>();

            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorGetAllByDeviceInApplicationIdResponseIoTContract>()
                .ForMember(vm => vm.DeviceAddress, m => m.ResolveUsing(src => {
                    var split = src.DeviceAddress.Split(':');
                    var result = new short[8];
                    for (int i = 0; i < 8; i++)
                    {
                        result[i] = short.Parse(split[i]);
                    }
                    return result;
                }))
                .ForMember(vm => vm.ResolutionBits, m => m.MapFrom(x => x.DSFamilyTempSensorResolution.Bits))
                .ForMember(vm => vm.LowChartLimiterCelsius, m => m.MapFrom(x => x.LowChartLimiterCelsius))
                .ForMember(vm => vm.HighChartLimiterCelsius, m => m.MapFrom(x => x.HighChartLimiterCelsius))
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.Id));

            CreateMap<TempSensorAlarmPositionContract, TempSensorAlarmPositionIoTContract>();
            CreateMap<DSFamilyTempSensorSetScaleRequestContract, DSFamilyTempSensorSetScaleRequestIoTContract>();
            CreateMap<DSFamilyTempSensorSetResolutionRequestContract, DSFamilyTempSensorSetResolutionRequestIoTContract>();
            CreateMap<DSFamilyTempSensorSetAlarmOnRequestContract, DSFamilyTempSensorSetAlarmOnRequestIoTContract>();
            CreateMap<DSFamilyTempSensorSetAlarmCelsiusRequestContract, DSFamilyTempSensorSetAlarmCelsiusRequestIoTContract>();
            CreateMap<DSFamilyTempSensorSetAlarmBuzzerOnRequestContract, DSFamilyTempSensorSetAlarmBuzzerOnRequestIoTContract>();
            CreateMap<DSFamilyTempSensorSetChartLimiterCelsiusRequestContract, DSFamilyTempSensorSetChartLimiterCelsiusRequestIoTContract>();

            CreateMap<TempSensorAlarmPositionContract, TempSensorAlarmPositionModel>();
            CreateMap<DSFamilyTempSensorSetAlarmOnRequestContract, DSFamilyTempSensorSetAlarmOnCompletedModel>();
            CreateMap<DSFamilyTempSensorSetLabelRequestContract, DSFamilyTempSensorSetLabelCompletedModel>();
            CreateMap<DSFamilyTempSensorSetAlarmCelsiusRequestContract, DSFamilyTempSensorSetAlarmCelsiusCompletedModel>();
            CreateMap<DSFamilyTempSensorSetAlarmBuzzerOnRequestContract, DSFamilyTempSensorSetAlarmBuzzerOnCompletedModel>();
            CreateMap<DSFamilyTempSensorSetChartLimiterCelsiusRequestContract, DSFamilyTempSensorSetChartLimiterCelsiusCompletedModel>();

            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorSetResolutionCompletedModel>()
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.SensorsInDevice.Single().DeviceBaseId))
                .ForMember(vm => vm.DSFamilyTempSensorResolutionId, m => m.MapFrom(x => x.DSFamilyTempSensorResolutionId));

            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorSetScaleCompletedModel>()
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.SensorsInDevice.Single().DeviceBaseId))
                .ForMember(vm => vm.TemperatureScaleId, m => m.MapFrom(x => x.TemperatureScaleId));

            CreateMap<TempSensorAlarm, TempSensorAlarmGetDetailModel>();

            CreateMap<SensorsInDevice, DSFamilyTempSensorDetailModel>()
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.SensorBaseId))
                .ForMember(vm => vm.DSFamilyTempSensorResolutionId, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).DSFamilyTempSensorResolutionId))
                .ForMember(vm => vm.TempSensorRange, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).TempSensorRange))
                .ForMember(vm => vm.HighAlarm, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).HighAlarm))
                .ForMember(vm => vm.LowAlarm, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).LowAlarm))
                .ForMember(vm => vm.LowChartLimiterCelsius, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).LowChartLimiterCelsius))
                .ForMember(vm => vm.HighChartLimiterCelsius, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).HighChartLimiterCelsius))
                .ForMember(vm => vm.TemperatureScaleId, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).TemperatureScaleId))
                .ForMember(vm => vm.Label, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).Label));

            CreateMap<DSFamilyTempSensorResolution, DSFamilyTempSensorResolutionDetailModel>();
        }

        #endregion Constructors
    }
}