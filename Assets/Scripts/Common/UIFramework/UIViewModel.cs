namespace Common
{
    public class UIViewModel
    {
        public UIView UIView;
        public bool Inited { get; private set; }
        public bool IsShow { get; private set; }

        public void Init(UIView uiView)
        {
            if (!Inited)
            {
                Inited = true;
                IsShow = false;
                this.UIView = uiView;
                this.UIView.Start();
                OnInit();
            }
        }

        public virtual void OnInit()
        {
            //CLog.UI.Debug($"UIView.OnInit, Name = {gameObject.name}");

        }

        public void Show()
        {
            IsShow = true;
            UIView.Show();
            OnShow();
            Refresh();
        }

        public virtual void OnShow()
        {

        }

        public virtual void Refresh()
        {

        }

        public void Hide()
        {
            IsShow = false;
            UIView.Hide();
            OnHide();
        }

        public virtual void OnHide()
        {

        }

        public void CleanUp()
        {
            if (Inited)
            {
                UIView.Cleanup();
                OnCleanUp();
                Inited = false;
            }
        }

        public virtual void OnCleanUp()
        {

        }
    }
}