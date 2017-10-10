using System;
using System.Runtime.CompilerServices;

namespace ART.Infra.CrossCutting.Logging
{
    public interface ILogger
    {
        void Debug([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        void Debug(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        void Debug(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        void Error([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        void Error(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        void Error(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        void Fatal([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        void Fatal(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        void Fatal(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        void Info([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        void Info(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        void Info(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        void Warn([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        void Warn(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        void Warn(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
    }
}