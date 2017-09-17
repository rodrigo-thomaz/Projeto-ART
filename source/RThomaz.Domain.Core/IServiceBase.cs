using System;

namespace RThomaz.Domain.Core
{
    public interface IServiceBase
    {
        Guid AplicacaoId { get; set; }
        string StorageBucketName { get; set; }
    }
}
