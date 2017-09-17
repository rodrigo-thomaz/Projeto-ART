using System;

namespace RThomaz.Infra.CrossCutting.ExceptionHandling
{
    public class RecordNotFoundException : Exception
    {
        private const string DefaultMessage = "Record not found";

        public RecordNotFoundException() 
            : base(DefaultMessage)
        {

        }
        public RecordNotFoundException(string message) 
            : base(message)
        {

        }

        public RecordNotFoundException(Exception innerException)
            : base(DefaultMessage, innerException)
        {

        }

        public RecordNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}