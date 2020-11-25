namespace Common
{
    public interface IUILogic
    {
        void OnInit();

        void OnCleanup();

        void OnShow();

        void OnHide();

        void Refresh();
    }
}