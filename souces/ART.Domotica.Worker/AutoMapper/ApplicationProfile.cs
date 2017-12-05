namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Contract;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class ApplicationProfile : Profile
    {
        #region Constructors

        public ApplicationProfile()
        {
            CreateMap<Application, ApplicationGetRPCResponseContract>()
                .ForMember(vm => vm.ApplicationId, m => m.MapFrom(x => x.Id))
                .ForMember(vm => vm.ApplicationMQ, m => m.MapFrom(x => x.ApplicationMQ));
        }

        #endregion Constructors
    }
}