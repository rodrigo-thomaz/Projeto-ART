using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Models;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class TipoEmailProfile : Profile
    {
        public TipoEmailProfile()
        {
            CreateMap<TipoEmailMasterDTO, TipoEmailMasterModel>();

            CreateMap<TipoEmailDetailViewDTO, TipoEmailDetailViewModel>();
            CreateMap<TipoEmailSelectViewDTO, TipoEmailSelectViewModel>();

            CreateMap<TipoEmailDetailInsertModel, TipoEmailDetailInsertDTO>();
            CreateMap<TipoEmailDetailEditModel, TipoEmailDetailEditDTO>();
        }        
    }
}