namespace ART.Domotica.Domain.AutoMapper
{
    using ART.Domotica.Repository.Entities;
    using ART.Security.Contract;

    using global::AutoMapper;

    public class ApplicationUserProfile : Profile
    {
        #region Constructors

        public ApplicationUserProfile()
        {
            CreateMap<RegisterUserContract, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ApplicationUserId));
        }

        #endregion Constructors
    }
}