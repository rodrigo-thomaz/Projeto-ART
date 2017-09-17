using AutoMapper;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class ConciliacaoProfile : Profile
    {
        public ConciliacaoProfile()
        {
            CreateMap<ConciliacaoDetailUpdateModel, ConciliacaoDetailUpdateDTO>();

            CreateMap<ConciliacaoDetailViewDTO, ConciliacaoDetailViewModel>();
        }
    }
}