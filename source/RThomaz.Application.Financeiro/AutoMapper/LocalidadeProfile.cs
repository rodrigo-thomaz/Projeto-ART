using AutoMapper;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class LocalidadeProfile : Profile
    {
        public LocalidadeProfile()
        {
            CreateMap<LocalidadeDetailUpdateModel, LocalidadeDetailUpdateDTO>();

            CreateMap<PaisSelectViewModel, PaisSelectViewDTO>();
            CreateMap<EstadoSelectViewModel, EstadoSelectViewDTO>();
            CreateMap<CidadeSelectViewModel, CidadeSelectViewDTO>();
            CreateMap<BairroSelectViewModel, BairroSelectViewDTO>();

            CreateMap<PaisSelectViewDTO, PaisSelectViewModel>();
            CreateMap<EstadoSelectViewDTO, EstadoSelectViewModel>();
            CreateMap<CidadeSelectViewDTO, CidadeSelectViewModel>();
            CreateMap<BairroSelectViewDTO, BairroSelectViewModel>();
        }
    }
}