namespace ART.Infra.CrossCutting.MQ
{
    using System;

    public class NoConsumersFoundException : Exception
    {
        #region Constructors

        public NoConsumersFoundException()
            : base("No Consumers Found")
        {
        }

        #endregion Constructors
    }
}