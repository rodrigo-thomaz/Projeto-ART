namespace ART.Infra.CrossCutting.Logging
{
    using System;
    using System.Runtime.CompilerServices;

    public interface ILogger
    {
        #region Methods

        void Debug([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void Debug(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void Debug(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void DebugFormat(IFormatProvider provider, string format, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0, params object[] args);

        void DebugFormat(string format, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0, params object[] args);

        void DebugFormat(string format, object arg0, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void DebugFormat(string format, object arg0, object arg1, object arg2, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void DebugFormat(string format, object arg0, object arg1, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void Error([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void Error(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void Error(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void ErrorFormat(IFormatProvider provider, string format, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0, params object[] args);

        void ErrorFormat(string format, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0, params object[] args);

        void ErrorFormat(string format, object arg0, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void ErrorFormat(string format, object arg0, object arg1, object arg2, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void ErrorFormat(string format, object arg0, object arg1, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void Fatal([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void Fatal(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void Fatal(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void FatalFormat(IFormatProvider provider, string format, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0, params object[] args);

        void FatalFormat(string format, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0, params object[] args);

        void FatalFormat(string format, object arg0, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void FatalFormat(string format, object arg0, object arg1, object arg2, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void FatalFormat(string format, object arg0, object arg1, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void Info([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void Info(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void Info(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void InfoFormat(IFormatProvider provider, string format, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0, params object[] args);

        void InfoFormat(string format, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0, params object[] args);

        void InfoFormat(string format, object arg0, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void InfoFormat(string format, object arg0, object arg1, object arg2, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void InfoFormat(string format, object arg0, object arg1, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void Warn([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void Warn(object message, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void Warn(object message, Exception ex, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void WarnFormat(IFormatProvider provider, string format, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0, params object[] args);

        void WarnFormat(string format, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0, params object[] args);

        void WarnFormat(string format, object arg0, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void WarnFormat(string format, object arg0, object arg1, object arg2, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void WarnFormat(string format, object arg0, object arg1, [CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        #endregion Methods
    }
}