namespace ART.Domotica.Worker.AutoMapper
{
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
                .ForMember(vm => vm.Max, m => m.MapFrom(x => x.Max))
                .ForMember(vm => vm.Min, m => m.MapFrom(x => x.Min));

            CreateMap<SensorTrigger, SensorTriggerGetResponseIoTContract>()
                .ForMember(vm => vm.SensorTriggerId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.TriggerOn, m => m.MapFrom(x => x.TriggerOn))
                .ForMember(vm => vm.BuzzerOn, m => m.MapFrom(x => x.BuzzerOn))
                .ForMember(vm => vm.Max, m => m.MapFrom(x => x.Max))
                .ForMember(vm => vm.Min, m => m.MapFrom(x => x.Min));

            CreateMap<SensorTriggerSetTriggerOnRequestContract, SensorTriggerSetTriggerOnRequestIoTContract>()
                .ForMember(vm => vm.SensorTriggerId, m => m.MapFrom(x => x.SensorTriggerId))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.TriggerOn, m => m.MapFrom(x => x.TriggerOn));

            CreateMap<SensorTriggerSetBuzzerOnRequestContract, SensorTriggerSetBuzzerOnRequestIoTContract>()
                .ForMember(vm => vm.SensorTriggerId, m => m.MapFrom(x => x.SensorTriggerId))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.BuzzerOn, m => m.MapFrom(x => x.BuzzerOn));

            CreateMap<SensorTriggerSetTriggerValueRequestContract, SensorTriggerSetTriggerValueRequestIoTContract>()
                .ForMember(vm => vm.SensorTriggerId, m => m.MapFrom(x => x.SensorTriggerId))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.TriggerValue, m => m.MapFrom(x => x.TriggerValue))
                .ForMember(vm => vm.Position, m => m.MapFrom(x => x.Position));

            CreateMap<SensorTriggerSetTriggerOnRequestContract, SensorTriggerSetTriggerOnModel>()
                .ForMember(vm => vm.SensorTriggerId, m => m.MapFrom(x => x.SensorTriggerId))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.TriggerOn, m => m.MapFrom(x => x.TriggerOn));

            CreateMap<SensorTriggerSetTriggerValueRequestContract, SensorTriggerSetTriggerValueModel>()
                .ForMember(vm => vm.SensorTriggerId, m => m.MapFrom(x => x.SensorTriggerId))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.TriggerValue, m => m.MapFrom(x => x.TriggerValue))
                .ForMember(vm => vm.Position, m => m.MapFrom(x => x.Position));

            CreateMap<SensorTriggerSetBuzzerOnRequestContract, SensorTriggerSetBuzzerOnModel>()
                .ForMember(vm => vm.SensorTriggerId, m => m.MapFrom(x => x.SensorTriggerId))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
                .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId))
                .ForMember(vm => vm.BuzzerOn, m => m.MapFrom(x => x.BuzzerOn));

            CreateMap<SensorTrigger, SensorTriggerInsertResponseIoTContract>()
                .ForMember(vm => vm.SensorTriggerId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
                .ForMember(vm => vm.TriggerOn, m => m.MapFrom(x => x.TriggerOn))
                .ForMember(vm => vm.BuzzerOn, m => m.MapFrom(x => x.BuzzerOn))
                .ForMember(vm => vm.Max, m => m.MapFrom(x => x.Max))
                .ForMember(vm => vm.Min, m => m.MapFrom(x => x.Min));

            CreateMap<SensorTriggerDeleteRequestContract, SensorTriggerDeleteModel>()
               .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId))
               .ForMember(vm => vm.SensorDatasheetId, m => m.MapFrom(x => x.SensorDatasheetId))
               .ForMember(vm => vm.SensorTypeId, m => m.MapFrom(x => x.SensorTypeId));

            CreateMap<SensorTrigger, SensorTriggerDeleteResponseIoTContract>()
                .ForMember(vm => vm.SensorTriggerId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.SensorId, m => m.MapFrom(x => x.SensorId));
        }

        #endregion Constructors
    }
}