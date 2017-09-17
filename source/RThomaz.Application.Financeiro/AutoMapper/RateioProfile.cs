using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Models;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class RateioProfile : Profile
    {
        public RateioProfile()
        {
            CreateMap<RateioDetailUpdateModel, RateioDetailUpdateDTO>();

            CreateMap<RateioDetailViewDTO, RateioDetailViewModel>();
        }
    }
}