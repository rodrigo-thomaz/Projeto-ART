using AutoMapper;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Services.DTOs.Helpers;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class MesAnoProfile : Profile
    {
        public MesAnoProfile()
        {
            CreateMap<MesAnoDTO, MesAnoModel>();

            CreateMap<MesAnoModel, MesAnoDTO>();
        }
    }
}