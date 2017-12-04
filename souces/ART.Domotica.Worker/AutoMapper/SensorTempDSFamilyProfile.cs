namespace ART.Domotica.Worker.AutoMapper
{
    using System;
    using System.Linq;

    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorTempDSFamilyProfile : Profile
    {
        #region Constructors

        public SensorTempDSFamilyProfile()
        {
            CreateMap<SensorSetUnitMeasurementRequestContract, SensorSetUnitMeasurementRequestIoTContract>();
            CreateMap<SensorTempDSFamilySetResolutionRequestContract, SensorTempDSFamilySetResolutionRequestIoTContract>();

            CreateMap<SensorSetLabelRequestContract, SensorSetLabelCompletedModel>();

            CreateMap<SensorTempDSFamily, SensorTempDSFamilySetResolutionCompletedModel>()
                .ForMember(vm => vm.SensorTempDSFamilyId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.DeviceId, m => m.MapFrom(x => x.Sensor.SensorsInDevice.Single().DeviceBaseId))
                .ForMember(vm => vm.SensorTempDSFamilyResolutionId, m => m.MapFrom(x => x.SensorTempDSFamilyResolutionId));

            CreateMap<SensorTempDSFamilyResolution, SensorTempDSFamilyResolutionDetailModel>();

            CreateMap<SensorsInDevice, SensorTempDSFamilyDetailModel>()
                .ForMember(vm => vm.SensorTempDSFamilyId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorTempDSFamilyResolutionId, m => m.MapFrom(x => x.Sensor.SensorTempDSFamily.SensorTempDSFamilyResolutionId))
                .ForMember(vm => vm.SensorUnitMeasurementScale, m => m.MapFrom(x => x.Sensor.SensorUnitMeasurementScale))
                //.ForMember(vm => vm.UnitMeasurementId, m => m.MapFrom(x => x.Sensor.UnitMeasurementId))
                .ForMember(vm => vm.Label, m => m.MapFrom(x => x.Sensor.Label))
                .ForMember(vm => vm.HighAlarm, m => m.ResolveUsing(src => {
                    if (src.Sensor.SensorTriggers == null) return null;
                    var max = src.Sensor.SensorTriggers.Max(x => Convert.ToDecimal(x.TriggerValue));
                    var sensorTrigger = src.Sensor.SensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == max);
                    return sensorTrigger;
                }))
                .ForMember(vm => vm.LowAlarm, m => m.ResolveUsing(src => {
                    if (src.Sensor.SensorTriggers == null) return null;
                    var min = src.Sensor.SensorTriggers.Min(x => Convert.ToDecimal(x.TriggerValue));
                    var sensorTrigger = src.Sensor.SensorTriggers.First(x => Convert.ToDecimal(x.TriggerValue) == min);
                    return sensorTrigger;
                }));
        }

        #endregion Constructors
    }
}