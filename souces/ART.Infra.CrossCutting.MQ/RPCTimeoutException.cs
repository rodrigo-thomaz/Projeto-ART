namespace ART.Infra.CrossCutting.MQ
{
    using System;

    public class RPCTimeoutException : Exception
    {
        #region Constructors

        public RPCTimeoutException()
            : base("RPC Timeout")
        {
        }

        #endregion Constructors
    }
}