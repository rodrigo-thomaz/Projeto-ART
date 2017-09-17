using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Models;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class PlanoContaProfile : Profile
    {
        public PlanoContaProfile()
        {
            CreateMap<PlanoContaSelectViewDTO, PlanoContaSelectViewModel>();
            CreateMap<PlanoContaDetailViewDTO, PlanoContaDetailViewModel>();

            CreateMap<PlanoContaMasterMoveModel, PlanoContaMasterMoveDTO>();
            CreateMap<PlanoContaDetailInsertModel, PlanoContaDetailInsertDTO>();
            CreateMap<PlanoContaDetailEditModel, PlanoContaDetailEditDTO>();
        }
    }
}