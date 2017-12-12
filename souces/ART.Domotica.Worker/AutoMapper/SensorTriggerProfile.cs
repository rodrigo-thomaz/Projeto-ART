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
                .ForMember(vm => vm.SensorTriggerId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.BuzzerOn, m => m.MapFrom(x => x.BuzzerOn))
                .ForMember(vm => vm.TriggerOn, m => m.MapFrom(x => x.TriggerOn))
                .ForMember(vm => vm.TriggerValue, m => m.MapFrom(x => x.TriggerValue));

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