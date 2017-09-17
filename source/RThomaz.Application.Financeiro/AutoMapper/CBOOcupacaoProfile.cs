using AutoMapper;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class CBOOcupacaoProfile : Profile
    {
        public CBOOcupacaoProfile()
        {
            CreateMap<CBOOcupacaoSelectViewDTO, CBOOcupacaoSelectViewModel>();
            CreateMap<CBOSinonimoSelectViewDTO, CBOSinonimoSelectViewModel>();
        }
    }
}