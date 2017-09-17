using AutoMapper;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class LancamentoPagoRecebidoProfile : Profile
    {
        public LancamentoPagoRecebidoProfile()
        {
            CreateMap<LancamentoPagoRecebidoDetailEditModel, LancamentoPagoRecebidoDetailEditDTO>();

            CreateMap<LancamentoPagoRecebidoDetailViewDTO, LancamentoPagoRecebidoDetailViewModel>();
        }
    }
}