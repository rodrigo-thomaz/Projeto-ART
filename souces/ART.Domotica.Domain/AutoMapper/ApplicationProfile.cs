using ART.Domotica.Model;
using ART.Domotica.Repository.Entities;
using AutoMapper;

namespace ART.Domotica.Domain.AutoMapper
{
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