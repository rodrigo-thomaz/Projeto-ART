using RThomaz.Domain.Financeiro.Services.DTOs;
using System;
using System.Threading.Tasks;

namespace RThomaz.Domain.Financeiro.Interfaces.Services
{
    public interface IAccountService 
    {
        Task<AccountDetailViewDTO> GetDetail(string email);
        Task<bool> ForgotYourPassword(string email);
        Task Register(Guid applicationUserId, string email, string senha);
    }
}