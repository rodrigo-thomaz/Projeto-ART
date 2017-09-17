using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Models;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class ContaProfile : Profile
    {
        public ContaProfile()
        {
            CreateMap<ContaMasterDTO, ContaMasterModel>();

            CreateMap<ContaSelectViewDTO, ContaSelectViewModel>()
                .Include<ContaCartaoCreditoSelectViewDTO, ContaCartaoCreditoSelectViewModel>()
                .Include<ContaCorrenteSelectViewDTO, ContaCorrenteSelectViewModel>()
                .Include<ContaEspecieSelectViewDTO, ContaEspecieSelectViewModel>()
                .Include<ContaPoupancaSelectViewDTO, ContaPoupancaSelectViewModel>();

            CreateMap<ContaCartaoCreditoSelectViewDTO, ContaCartaoCreditoSelectViewModel>();
            CreateMap<ContaCorrenteSelectViewDTO, ContaCorrenteSelectViewModel>();
            CreateMap<ContaEspecieSelectViewDTO, ContaEspecieSelectViewModel>();
            CreateMap<ContaPoupancaSelectViewDTO, ContaPoupancaSelectViewModel>();
            
            CreateMap<ContaSummaryViewDTO, ContaSummaryViewModel>();

            CreateMap<SaldoInicialDTO, SaldoInicialModel>();
            CreateMap<DadoBancarioDTO, DadoBancarioModel>();

            CreateMap<ContaCartaoCreditoDetailViewDTO, ContaCartaoCreditoDetailViewModel>();
            CreateMap<ContaCorrenteDetailViewDTO, ContaCorrenteDetailViewModel>();
            CreateMap<ContaEspecieDetailViewDTO, ContaEspecieDetailViewModel>();
            CreateMap<ContaPoupancaDetailViewDTO, ContaPoupancaDetailViewModel>();

            CreateMap<SaldoInicialModel, SaldoInicialDTO>();
            CreateMap<DadoBancarioModel, DadoBancarioDTO>();

            CreateMap<ContaCartaoCreditoDetailInsertModel, ContaCartaoCreditoDetailInsertDTO>();
            CreateMap<ContaCorrenteDetailInsertModel, ContaCorrenteDetailInsertDTO>();
            CreateMap<ContaEspecieDetailInsertModel, ContaEspecieDetailInsertDTO>();
            CreateMap<ContaPoupancaDetailInsertModel, ContaPoupancaDetailInsertDTO>();

            CreateMap<ContaCartaoCreditoDetailEditModel, ContaCartaoCreditoDetailEditDTO>();
            CreateMap<ContaCorrenteDetailEditModel, ContaCorrenteDetailEditDTO>();
            CreateMap<ContaEspecieDetailEditModel, ContaEspecieDetailEditDTO>();
            CreateMap<ContaPoupancaDetailEditModel, ContaPoupancaDetailEditDTO>();
        }
    }
}