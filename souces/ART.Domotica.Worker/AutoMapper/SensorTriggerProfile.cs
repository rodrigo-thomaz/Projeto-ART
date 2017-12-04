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
            CreateMap<SensorTrigger, SensorTriggerGetModel>()
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

            CreateMap<SensorTriggerSetAlarmOnRequestContract, SensorTriggerSetAlarmOnModel>();
            CreateMap<SensorTriggerSetAlarmCelsiusRequestContract, SensorTriggerSetAlarmCelsiusModel>();
            CreateMap<SensorTriggerSetAlarmBuzzerOnRequestContract, SensorTriggerSetAlarmBuzzerOnModel>();
        }

        #endregion Constructors
    }
}