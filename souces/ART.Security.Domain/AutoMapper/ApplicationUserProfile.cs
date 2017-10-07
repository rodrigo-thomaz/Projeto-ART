namespace ART.Security.Domain.AutoMapper
{
    using ART.Security.Common.Contracts;
    using ART.Security.Repository.Entities;

    using global::AutoMapper;

    public class ApplicationUserProfile : Profile
    {
        #region Constructors

        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUser, RegisterUserContract>()
                .ForMember(dest => dest.ApplicationUserId, opt => opt.MapFrom(src => src.Id));
        }

        #endregion Constructors
    }
}