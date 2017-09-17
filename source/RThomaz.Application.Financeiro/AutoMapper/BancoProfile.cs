using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Models;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class BancoProfile : Profile
    {
        public BancoProfile()
        {
            CreateMap<BancoMasterDTO, BancoMasterModel>();

            CreateMap<BancoDetailViewDTO, BancoDetailViewModel>();
            CreateMap<BancoMascarasDetailViewDTO, BancoMascarasDetailViewModel>();
            CreateMap<BancoSelectViewDTO, BancoSelectViewModel>();

            CreateMap<BancoDetailInsertModel, BancoDetailInsertDTO>();
            CreateMap<BancoDetailEditModel, BancoDetailEditDTO>();            
        }
    }
}