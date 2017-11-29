namespace ART.Domotica.Worker.AutoMapper
{
    using System;
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
                .ForMember(vm => vm.LowChartLimiterCelsius, m => m.MapFrom(x => x.SensorChartLimiter.Min))
                .ForMember(vm => vm.HighChartLimiterCelsius, m => m.MapFrom(x => x.SensorChartLimiter.Max))
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.Id));

            CreateMap<DSFamilyTempSensorSetUnitOfMeasurementRequestContract, DSFamilyTempSensorSetUnitOfMeasurementRequestIoTContract>();
            CreateMap<DSFamilyTempSensorSetResolutionRequestContract, DSFamilyTempSensorSetResolutionRequestIoTContract>();

            CreateMap<DSFamilyTempSensorSetLabelRequestContract, DSFamilyTempSensorSetLabelCompletedModel>();

            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorSetResolutionCompletedModel>()
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.SensorsInDevice.Single().DeviceBaseId))
                .ForMember(vm => vm.DSFamilyTempSensorResolutionId, m => m.MapFrom(x => x.DSFamilyTempSensorResolutionId));

            CreateMap<DSFamilyTempSensor, DSFamilyTempSensorSetUnitOfMeasurementCompletedModel>()
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.SensorsInDevice.Single().DeviceBaseId))
                .ForMember(vm => vm.UnitOfMeasurementId, m => m.MapFrom(x => x.UnitOfMeasurementId));

            CreateMap<DSFamilyTempSensorResolution, DSFamilyTempSensorResolutionDetailModel>();

            CreateMap<SensorsInDevice, DSFamilyTempSensorDetailModel>()
                .ForMember(vm => vm.DSFamilyTempSensorId, m => m.MapFrom(x => x.SensorBaseId))
                .ForMember(vm => vm.DSFamilyTempSensorResolutionId, m => m.MapFrom(x => ((DSFamilyTempSensor)x.SensorBase).DSFamilyTempSensorResolutionId))
                .ForMember(vm => vm.SensorRangeId, m => m.MapFrom(x => x.SensorBase.SensorRangeId.Value))
                .ForMember(vm => vm.SensorChartLimiter, m => m.MapFrom(x => x.SensorBase.SensorChartLimiter))
                .ForMember(vm => vm.UnitOfMeasurementId, m => m.MapFrom(x => x.SensorBase.UnitOfMeasurementId))
                .ForMember(vm => vm.Label, m => m.MapFrom(x => x.SensorBase.Label))
                .ForMember(vm => vm.HighAlarm, m => m.ResolveUsing(src => {
                    if (src.SensorBase.SensorTriggers == null) return null;
                    var max = src.SensorBase.SensorTriggers.Max(x => Convert.ToDecimal(x.TriggerValue));
                    var sensorTrigger = src.SensorBase.SensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == max);
                    return sensorTrigger;
                }))
                .ForMember(vm => vm.LowAlarm, m => m.ResolveUsing(src => {
                    if (src.SensorBase.SensorTriggers == null) return null;
                    var min = src.SensorBase.SensorTriggers.Min(x => Convert.ToDecimal(x.TriggerValue));
                    var sensorTrigger = src.SensorBase.SensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == min);
                    return sensorTrigger;
                }));
        }

        #endregion Constructors
    }
}