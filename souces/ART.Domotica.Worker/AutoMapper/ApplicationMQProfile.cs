namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class ApplicationMQProfile : Profile
    {
        #region Constructors

        public ApplicationMQProfile()
        {
            CreateMap<ApplicationMQ, ApplicationMQGetRPCResponseContract>()
                .ForMember(vm => vm.Topic, m => m.MapFrom(x => x.Topic));
        }

        #endregion Constructors
    }
}