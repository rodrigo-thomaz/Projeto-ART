using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Models;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class TipoHomePageProfile : Profile
    {
        public TipoHomePageProfile()
        {
            CreateMap<TipoHomePageMasterDTO, TipoHomePageMasterModel>();

            CreateMap<TipoHomePageDetailViewDTO, TipoHomePageDetailViewModel>();
            CreateMap<TipoHomePageSelectViewDTO, TipoHomePageSelectViewModel>();

            CreateMap<TipoHomePageDetailInsertModel, TipoHomePageDetailInsertDTO>();
            CreateMap<TipoHomePageDetailEditModel, TipoHomePageDetailEditDTO>();
        }        
    }
}