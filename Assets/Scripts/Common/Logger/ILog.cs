namespace Logger
{
    public interface ILog
    {
        void Assert(bool condition, object msg);
        void Trace(object msg);
        void Debug(object msg);
        void Info(object msg);
        void Warn(object msg);
        void Error(object msg);
        void Fatal(object msg);

        bool Enable { get; set; }
    }
}