using AutoMapper;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class TransferenciaProgramadaProfile : Profile
    {
        public TransferenciaProgramadaProfile()
        {
            CreateMap<TransferenciaProgramadaDetailViewDTO, TransferenciaProgramadaDetailViewModel>();


            CreateMap<ProgramacaoDetailUpdateBaseModel, ProgramacaoDetailBaseDTO>()
                .Include<TransferenciaProgramadaDetailInsertModel, TransferenciaProgramadaDetailInsertDTO>()
                .Include<TransferenciaProgramadaDetailEditModel, TransferenciaProgramadaDetailEditDTO>();

            CreateMap<TransferenciaProgramadaDetailInsertModel, TransferenciaProgramadaDetailInsertDTO>();
            CreateMap<TransferenciaProgramadaDetailEditModel, TransferenciaProgramadaDetailEditDTO>();
        }
    }
}