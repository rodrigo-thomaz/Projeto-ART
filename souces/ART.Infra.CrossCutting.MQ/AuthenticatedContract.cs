using System;

namespace ART.Infra.CrossCutting.MQ
{
    public class AuthenticatedContract<TContract> : NoAuthenticatedContract<TContract>
    {
        public Guid ApplicationUserId { get; set; }
    }
}
