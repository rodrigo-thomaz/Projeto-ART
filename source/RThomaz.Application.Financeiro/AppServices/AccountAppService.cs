using RThomaz.Application.Financeiro.Interfaces;
using RThomaz.Domain.Financeiro.Interfaces.Services;
using RThomaz.Application.Financeiro.Models;
using System.Threading.Tasks;
using AutoMapper;
using RThomaz.Domain.Financeiro.Services.DTOs;
using System;

namespace RThomaz.Application.Financeiro.AppServices
{
    public class AccountAppService : AppServiceBase, IAccountAppService
    {
        #region private fields

        private readonly IAccountService _accountService;

        #endregion

        #region constructors

        public AccountAppService(IAccountService segurancaService)
        {
            _accountService = segurancaService;
        }

        #endregion

        #region public voids

        public async Task<AccountDetailViewModel> GetDetail(string email)
        {
            var dto = await _accountService.GetDetail(email);
            var model = Mapper.Map<AccountDetailViewDTO, AccountDetailViewModel>(dto);
            return model;
        }

        public async Task Register(Guid applicationUserId, string email, string senha)
        {
            await _accountService.Register(applicationUserId, email, senha);
        }

        public async Task<bool> ForgotYourPassword(string email)
        {
            return await _accountService.ForgotYourPassword(email);
        }

        #endregion
    }
}