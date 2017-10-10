using System.Runtime.CompilerServices;

namespace ART.Infra.CrossCutting.Logging
{
    public interface ILogger
    {
        void Debug([CallerMemberName] string callerMemberName = "", [CallerLineNumber] int callerLineNumber = 0);
        void Debug(object message);
    }
}