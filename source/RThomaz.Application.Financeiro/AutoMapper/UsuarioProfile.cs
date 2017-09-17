using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using RThomaz.Application.Financeiro.Models;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<UsuarioMasterDTO, UsuarioMasterModel>();

            CreateMap<UsuarioSelectViewDTO, UsuarioSelectViewModel>();
            CreateMap<UsuarioDetailViewDTO, UsuarioDetailViewModel>();

            CreateMap<UsuarioDetailInsertModel, UsuarioDetailInsertDTO>();
            CreateMap<UsuarioDetailEditModel, UsuarioDetailEditDTO>();
        }
    }
}