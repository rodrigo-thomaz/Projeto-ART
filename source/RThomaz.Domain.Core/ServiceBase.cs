using System;

namespace RThomaz.Domain.Core
{
    public abstract class ServiceBase : IServiceBase
    {
        public Guid AplicacaoId { get; set; }
        public string StorageBucketName { get; set; }
    }
}
