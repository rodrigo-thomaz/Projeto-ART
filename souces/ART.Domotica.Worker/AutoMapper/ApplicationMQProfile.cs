namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class ApplicationMQProfile : Profile
    {
        #region Constructors

        public ApplicationMQProfile()
        {
            CreateMap<ApplicationMQ, ApplicationMQDetailModel>()
                .ForMember(vm => vm.BrokerApplicationTopic, m => m.MapFrom(x => x.Topic));
        }

        #endregion Constructors
    }
}