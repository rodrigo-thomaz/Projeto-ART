using AutoMapper;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class PerfilProfile : Profile
    {
        public PerfilProfile()
        {
            CreateMap<PerfilPersonalInfoViewDTO, PerfilPersonalInfoViewModel>();

            CreateMap<PerfilPersonalInfoEditModel, PerfilPersonalInfoEditDTO>();
            CreateMap<PerfilAvatarEditModel, PerfilAvatarEditDTO>();
        }
    }
}