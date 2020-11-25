using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    /// 一般的UI窗口
    public class WindowVM : UIViewModel
    {
        private List<UIViewModel> subVmList = new List<UIViewModel>();
        public string Name { get; set; }

        public override void OnInit()
        {
            base.OnInit();
            foreach (var vm in subVmList)
            {
                // 防御性处理
                if (vm == this)
                {
                    continue;
                }
                vm.OnInit();
            }
        }

        public override void OnCleanUp()
        {
            foreach (var vm in subVmList)
            {
                if (vm == this)
                {
                    continue;
                }
                vm.CleanUp();
            }

            base.OnCleanUp();
        }

        public override void OnShow()
        {
            var rectTransform = UIView.GetComponent<RectTransform>();
            rectTransform.SetAsLastSibling();
            base.OnShow();
            foreach (var vm in subVmList)
            {
                if (vm == this)
                {
                    continue;
                }
                vm.OnShow();
            }
        }

        public override void OnHide()
        {
            foreach (var vm in subVmList)
            {
                if (vm == this)
                {
                    continue;
                }
                vm.OnHide();
            }

            base.OnHide();
        }


        public void AddSubUIView(UIViewModel vm)
        {
            subVmList.Add(vm);
            vm.OnInit();
        }

        public virtual WindowsCatalog GetWindowsCatalog()
        {
            return WindowsCatalog.Main;
        }

        public static WindowVM Default { get; } = new WindowVM();
    }

    public enum WindowsCatalog
    {
        Main,
        Combat
    }
}