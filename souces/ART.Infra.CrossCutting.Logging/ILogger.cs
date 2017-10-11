namespace ART.Infra.CrossCutting.Logging
{
    using System.Runtime.CompilerServices;

    public interface ILogger
    {
        #region Methods

        void DebugEnter([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);

        void DebugLeave([CallerMemberName] string callerMethod = "", [CallerLineNumber] int callerLine = 0);
        
        #endregion Methods
    }
}