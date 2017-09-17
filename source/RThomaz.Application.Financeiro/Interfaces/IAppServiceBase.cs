using System;

namespace RThomaz.Application.Financeiro.Interfaces
{
    public interface IAppServiceBase
    {
        Guid AplicacaoId { get; set; }

        string StorageBucketName { get; set; }
    }
}
