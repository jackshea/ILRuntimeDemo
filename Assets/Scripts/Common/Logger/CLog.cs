namespace Logger
{
    public class CLog
    {
        public static void Init()
        {
            Main = new LogImpl("Main");
            Battle = new LogImpl("Battle");
            UI = new LogImpl("UI");
            Damage = new LogImpl("Damage");
            Damage.Enable = false;
        }
        public static ILog Main { get; private set; }
        public static ILog Battle { get; private set; }
        public static ILog UI { get; private set; }

        public static ILog Damage { get; private set; }
    }
}