using AutoMapper;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class MovimentoProfile : Profile
    {
        public MovimentoProfile()
        {
            CreateMap<MovimentoDetailInsertModel, MovimentoDetailInsertDTO>();
            CreateMap<MovimentoManualEditModel, MovimentoManualEditDTO>();
            CreateMap<MovimentoImportadoEditModel, MovimentoImportadoEditDTO>();
            CreateMap<MovimentoConciliadoEditModel, MovimentoConciliadoEditDTO>();

            CreateMap<MovimentoDetailViewDTO, MovimentoDetailViewModel>();
            CreateMap<MovimentoSelectViewDTO, MovimentoSelectViewModel>();
            CreateMap<MovimentoImportacaoMasterDTO, MovimentoImportacaoMasterModel>();
        }
    }
}