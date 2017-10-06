namespace ART.Domotica.Domain.AutoMapper
{
    using ART.Domotica.Model;
    using ART.Domotica.Repository.Entities;

    using AutoMapper;
    using global::AutoMapper;

    public class ApplicationProfile : Profile
    {
        #region Constructors

        public ApplicationProfile()
        {
            CreateMap<Application, ApplicationGetAllModel>();
        }

        #endregion Constructors
    }
}