namespace ART.Domotica.Worker.AutoMapper
{
    using System;

    using ART.Domotica.Contract;
    using ART.Domotica.IoTContract;
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class SensorTriggerProfile : Profile
    {
        #region Constructors

        public SensorTriggerProfile()
        {
            CreateMap<SensorTrigger, SensorTriggerDetailModel>()
                .ForMember(vm => vm.AlarmOn, m => m.MapFrom(x => x.TriggerOn))
                .ForMember(vm => vm.AlarmCelsius, m => m.MapFrom(x => Convert.ToDecimal(x.TriggerValue)))
                .ForMember(vm => vm.AlarmBuzzerOn, m => m.MapFrom(x => x.BuzzerOn));

            CreateMap<SensorTrigger, SensorTriggerGetResponseIoTContract>()
                .ForMember(vm => vm.AlarmOn, m => m.MapFrom(x => x.TriggerOn))
                .ForMember(vm => vm.AlarmCelsius, m => m.MapFrom(x => Convert.ToDecimal(x.TriggerValue)))
                .ForMember(vm => vm.AlarmBuzzerOn, m => m.MapFrom(x => x.BuzzerOn));

            CreateMap<SensorTriggerSetAlarmOnRequestContract, SensorTriggerSetAlarmOnRequestIoTContract>();
            CreateMap<SensorTriggerSetAlarmCelsiusRequestContract, SensorTriggerSetAlarmCelsiusRequestIoTContract>();
            CreateMap<SensorTriggerSetAlarmBuzzerOnRequestContract, SensorTriggerSetAlarmBuzzerOnRequestIoTContract>();

            CreateMap<SensorTriggerSetAlarmOnRequestContract, SensorTriggerSetAlarmOnCompletedModel>();
            CreateMap<SensorTriggerSetAlarmCelsiusRequestContract, SensorTriggerSetAlarmCelsiusCompletedModel>();
            CreateMap<SensorTriggerSetAlarmBuzzerOnRequestContract, SensorTriggerSetAlarmBuzzerOnCompletedModel>();
        }

        #endregion Constructors
    }
}