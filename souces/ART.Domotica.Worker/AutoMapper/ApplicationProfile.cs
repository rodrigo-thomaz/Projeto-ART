namespace ART.Domotica.Worker.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using global::AutoMapper;

    public class ApplicationProfile : Profile
    {
        #region Constructors

        public ApplicationProfile()
        {
            CreateMap<Application, ApplicationGetModel>()
                .ForMember(vm => vm.ApplicationId, m => m.MapFrom(x => x.Id));
        }

        #endregion Constructors
    }
}