using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Models;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class CentroCustoProfile : Profile
    {
        public CentroCustoProfile()
        {
            CreateMap<CentroCustoSelectViewDTO, CentroCustoSelectViewModel>();
            CreateMap<CentroCustoDetailViewDTO, CentroCustoDetailViewModel>();

            CreateMap<CentroCustoMasterMoveModel, CentroCustoMasterMoveDTO>();
            CreateMap<CentroCustoDetailInsertModel, CentroCustoDetailInsertDTO>();
            CreateMap<CentroCustoDetailEditModel, CentroCustoDetailEditDTO>();
        }
    }
}