using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Models;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class TipoTelefoneProfile : Profile
    {
        public TipoTelefoneProfile()
        {
            CreateMap<TipoTelefoneMasterDTO, TipoTelefoneMasterModel>();

            CreateMap<TipoTelefoneDetailViewDTO, TipoTelefoneDetailViewModel>();
            CreateMap<TipoTelefoneSelectViewDTO, TipoTelefoneSelectViewModel>();

            CreateMap<TipoTelefoneDetailInsertModel, TipoTelefoneDetailInsertDTO>();
            CreateMap<TipoTelefoneDetailEditModel, TipoTelefoneDetailEditDTO>();
        }        
    }
}