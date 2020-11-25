namespace Ujoy19.Common
{
    public class UIContext
    {
        public static UIContext Instance { get; } = new UIContext();

        public IUILoader UILoader { get; private set; }

        public void SetUILoader(IUILoader uiLoader)
        {
            UILoader = uiLoader;
        }
    }
}