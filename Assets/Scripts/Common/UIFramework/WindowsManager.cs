using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Logger;
using UnityEngine;

namespace Ujoy19.Common
{
    /// 窗口管理器
    /// 用于打开关闭窗口
    public class WindowsManager
    {
        public static WindowsManager Instance { get; } = new WindowsManager();

        private Dictionary<string, WindowVM> loadedWindows = new Dictionary<string, WindowVM>();

        public string StartUI = "MainUI";

        public async Task Init()
        {

        }

        public async Task OpenStartUI()
        {
            await OpenAsync(StartUI);
        }

        public void Open<T>() where T : WindowVM
        {
#pragma warning disable 4014
            OpenAsync<T>();
#pragma warning restore 4014
        }

        public void Open(string uiName)
        {
#pragma warning disable 4014
            OpenAsync(uiName);
#pragma warning restore 4014
        }

        public async Task<T> OpenAsync<T>() where T : WindowVM
        {
            return (T)await OpenAsync(typeof(T).Name);
        }

        public async Task<WindowVM> OpenAsync(string uiName)
        {
            if (!loadedWindows.TryGetValue(uiName, out var window))
            {
                loadedWindows[uiName] = WindowVM.Default;
                var goUI = await UIContext.Instance.UILoader.LoadUI(uiName);
                CLog.UI.Assert(goUI != null, "goUI == null,uiName = " + uiName);
                goUI.name = uiName;
                var uiView = goUI.GetComponent<UIView>();
                window = new WindowVM();
                var windowRoot = GetWindowRoot(window.GetWindowsCatalog());
                goUI.transform.SetParent(windowRoot, false);
                window.Name = uiName;
                window.Init(uiView);
                loadedWindows[uiName] = window;
            }

            window.Show();
            return window;
        }

        public void Close(WindowVM windowVm)
        {
            if (windowVm.IsShow)
            {
                windowVm.Hide();
            }
        }

        public void Close<T>() where T : WindowVM
        {
            var window = GetWindow<T>();
            Close(window);
        }

        public void Refresh(WindowVM windowVm)
        {
            if (windowVm.IsShow)
            {
                windowVm.Refresh();
            }
        }

        public WindowVM GetWindow(string widowsName)
        {
            loadedWindows.TryGetValue(widowsName, out var window);
            return window;
        }

        public T GetWindow<T>() where T : WindowVM
        {
            return (T)GetWindow(typeof(T).Name);
        }

        public List<WindowVM> GetAllWindows()
        {
            return loadedWindows.Values.ToList();
        }

        public void Release(WindowVM windowVm)
        {
            windowVm.CleanUp();
            loadedWindows.Remove(windowVm.Name);
            GameObject.Destroy(windowVm.UIView);
            UIContext.Instance.UILoader.UnloadUI(windowVm.UIView.gameObject);
        }

        public void Release<T>() where T : WindowVM
        {
            var window = GetWindow<T>();
            Release(window);
        }

        private Transform GetWindowRoot(WindowsCatalog windowsCatalog)
        {
            switch (windowsCatalog)
            {
                case WindowsCatalog.Main: return UIRoot.Instance.MainUIRoot;
                case WindowsCatalog.Combat: return UIRoot.Instance.CombatUIRoot;
            }

            return null;
        }
    }
}