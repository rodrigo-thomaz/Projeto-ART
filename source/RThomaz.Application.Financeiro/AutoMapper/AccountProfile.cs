using AutoMapper;
using RThomaz.Application.Financeiro.Models;
using RThomaz.Domain.Financeiro.Services.DTOs;

namespace RThomaz.Application.Financeiro.AutoMapper
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<AccountDetailViewDTO, AccountDetailViewModel>();
        }
    }
}
