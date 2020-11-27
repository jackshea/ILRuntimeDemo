using Common;
using UnityEngine.UI;

namespace HotFixProject.UI
{
    public class MainUI : WindowVM
    {
        public override void OnInit()
        {
            base.OnInit();
            var version = UIView.Get<Text>("VersionValue");
            if (version!=null)
            {
                version.text = "1.0.0.2";
            }
        }
    }
}