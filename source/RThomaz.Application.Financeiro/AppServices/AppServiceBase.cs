using System;
using RThomaz.Application.Financeiro.Interfaces;

namespace RThomaz.Application.Financeiro.AppServices
{
    public abstract class AppServiceBase : IAppServiceBase
    {
        public Guid AplicacaoId { get; set; }

        public string StorageBucketName { get; set; }
    }
}
