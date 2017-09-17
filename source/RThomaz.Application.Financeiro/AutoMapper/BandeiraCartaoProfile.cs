using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Models;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class BandeiraCartaoProfile : Profile
    {
        public BandeiraCartaoProfile()
        {
            CreateMap<BandeiraCartaoMasterDTO, BandeiraCartaoMasterModel>();

            CreateMap<BandeiraCartaoSelectViewDTO, BandeiraCartaoSelectViewModel>();
            CreateMap<BandeiraCartaoDetailViewDTO, BandeiraCartaoDetailViewModel>();

            CreateMap<BandeiraCartaoDetailInsertModel, BandeiraCartaoDetailInsertDTO>();
            CreateMap<BandeiraCartaoDetailEditModel, BandeiraCartaoDetailEditDTO>();
        }
    }
}