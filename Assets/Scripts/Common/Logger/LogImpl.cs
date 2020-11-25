using NLog;

namespace Logger
{
    public class LogImpl : ILog
    {
        private NLog.Logger log;

        public bool Enable { get; set; }

        public LogImpl() : this("Default")
        {
        }

        public LogImpl(string name)
        {
            log = LogManager.GetLogger(name);
            Enable = true;
        }

        public void Assert(bool condition, object msg)
        {
            if (!Enable)
            {
                return;
            }

            if (!condition)
            {
                log.Error(msg);
            }
        }

        public void Trace(object msg)
        {
            if (!Enable)
            {
                return;
            }

            log.Trace(msg);
        }

        public void Debug(object msg)
        {
            if (!Enable)
            {
                return;
            }

            log.Debug(msg);
        }

        public void Info(object msg)
        {
            if (!Enable)
            {
                return;
            }

            log.Info(msg);
        }

        public void Warn(object msg)
        {
            if (!Enable)
            {
                return;
            }

            log.Warn(msg);
        }

        public void Error(object msg)
        {
            if (!Enable)
            {
                return;
            }

            log.Error(msg);
        }

        public void Fatal(object msg)
        {
            if (!Enable)
            {
                return;
            }

            log.Fatal(msg);
        }
    }
}