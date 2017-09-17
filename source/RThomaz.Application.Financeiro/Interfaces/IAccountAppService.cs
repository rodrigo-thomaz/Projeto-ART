using RThomaz.Application.Financeiro.Models;
using System;
using System.Threading.Tasks;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface IAccountAppService : IAppServiceBase
    {
        Task<AccountDetailViewModel> GetDetail(string email);
        Task Register(Guid applicationUserId, string email, string senha);
        Task<bool> ForgotYourPassword(string email);
    }
}