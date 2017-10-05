namespace ART.MQ.Worker.AutoMapper
{
    using ART.Data.Repository.Entities;
    using ART.Seguranca.Contracts;

    using global::AutoMapper;

    public class ApplicationUserProfile : Profile
    {
        #region Constructors

        public ApplicationUserProfile()
        {
            CreateMap<ApplicationUserContract, ApplicationUser>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ApplicationUserId));
        }

        #endregion Constructors
    }
}