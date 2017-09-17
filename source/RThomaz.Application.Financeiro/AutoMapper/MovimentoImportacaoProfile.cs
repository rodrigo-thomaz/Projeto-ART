using AutoMapper;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class MovimentoImportacaoProfile : Profile
    {
        public MovimentoImportacaoProfile()
        {
            CreateMap<MovimentoImportacaoDetailViewDTO, MovimentoImportacaoDetailViewModel>();
            CreateMap<MovimentoImportacaoOFXItemDTO, MovimentoImportacaoOFXItemModel>();
            CreateMap<MovimentoImportacaoOFXDTO, MovimentoImportacaoOFXModel>();
            CreateMap<MovimentoImportacaoDetailViewDTO, MovimentoImportacaoDetailViewModel>();
            CreateMap<MovimentoImportacaoSelectViewDTO, MovimentoImportacaoSelectViewModel>();

            CreateMap<MovimentoImportacaoOFXItemModel, MovimentoImportacaoOFXItemDTO>();
            CreateMap<MovimentoImportacaoOFXModel, MovimentoImportacaoOFXDTO>();
        }
    }
}