using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Models;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class TipoEnderecoProfile : Profile
    {
        public TipoEnderecoProfile()
        {
            CreateMap<TipoEnderecoMasterDTO, TipoEnderecoMasterModel>();

            CreateMap<TipoEnderecoDetailViewDTO, TipoEnderecoDetailViewModel>();
            CreateMap<TipoEnderecoSelectViewDTO, TipoEnderecoSelectViewModel>();

            CreateMap<TipoEnderecoDetailInsertModel, TipoEnderecoDetailInsertDTO>();
            CreateMap<TipoEnderecoDetailEditModel, TipoEnderecoDetailEditDTO>();
        }        
    }
}