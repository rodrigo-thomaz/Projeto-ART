namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class ApplicationBrokerSettingProfile : Profile
    {
        #region Constructors

        public ApplicationBrokerSettingProfile()
        {
            CreateMap<ApplicationBrokerSetting, ApplicationBrokerSettingDetailModel>()
                .ForMember(vm => vm.BrokerApplicationTopic, m => m.MapFrom(x => x.Topic));
        }

        #endregion Constructors
    }
}