namespace ART.Seguranca.Domain.AutoMapper
{
    using ART.Seguranca.Contracts;
    using ART.Seguranca.Repository.Entities;

    using global::AutoMapper;

    public class ApplicationUserProfile : Profile
    {
        #region Constructors

        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserContract>()
                .ForMember(dest => dest.ApplicationUserId, opt => opt.MapFrom(src => src.Id));
        }

        #endregion Constructors
    }
}