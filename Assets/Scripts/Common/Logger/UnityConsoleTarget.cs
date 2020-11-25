using NLog;
using NLog.Targets;

namespace Logger
{
    [Target("UnityConsole")]
    public class UnityConsoleTarget : TargetWithLayout
    {
        protected override void Write(LogEventInfo logEvent)
        {
            var msg = this.Layout.Render(logEvent);
            if (logEvent.Level >= LogLevel.Trace && logEvent.Level <= LogLevel.Info)
            {
                UnityEngine.Debug.Log(msg);
            }
            else if (logEvent.Level == LogLevel.Warn)
            {
                UnityEngine.Debug.LogWarning(msg);
            }
            else
            {
                UnityEngine.Debug.LogError(msg);
            }
        }
    }
}

