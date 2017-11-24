namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.Repository.Entities;
    using ART.Infra.CrossCutting.Utils;

    using global::AutoMapper;

    public class ApplicationMQProfile : Profile
    {
        #region Constructors

        public ApplicationMQProfile()
        {
            CreateMap<ApplicationMQ, ApplicationMQGetRPCResponseContract>()
                .ForMember(vm => vm.User, m => m.MapFrom(x => x.User))
                .ForMember(vm => vm.Password, m => m.MapFrom(x => x.Password))
                .ForMember(vm => vm.ApplicationTopic, m => m.MapFrom(x => x.Topic));
        }

        #endregion Constructors
    }
}