using AutoMapper;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class LancamentoProgramadoProfile: Profile
    {
        public LancamentoProgramadoProfile()
        {
            CreateMap<LancamentoProgramadoDetailViewDTO, LancamentoProgramadoDetailViewModel>();

            CreateMap<LancamentoProgramadoDetailUpdateModel, LancamentoProgramadoInsertDTO>();
            CreateMap<LancamentoProgramadoDetailUpdateModel, LancamentoProgramadoEditDTO>();
        }
    }
}