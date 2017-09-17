using AutoMapper;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class TransferenciaProfile : Profile
    {
        public TransferenciaProfile()
        {
            CreateMap<TransferenciaDetailViewDTO, TransferenciaDetailViewModel>();

            CreateMap<TransferenciaDetailInsertModel, TransferenciaDetailInsertDTO>();
            CreateMap<TransferenciaDetailEditModel, TransferenciaDetailEditDTO>();
        }
    }
}