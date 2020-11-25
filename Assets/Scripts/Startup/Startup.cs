using System.Threading.Tasks;
using Logger;
using Ujoy19.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

/// 负责检查更新资源
public class Startup : MonoBehaviour
{
    private InitHotFix initHotFix;

    async Task Start()
    {
        new LoggerLoader().Load();
        CLog.Init();
        CLog.Main.Info("CLog init.");
        DontDestroyOnLoad(GameObject.Find("Core"));
        initHotFix = new InitHotFix();
        initHotFix.Init();
        var uiLoader = new DefaultUILoader();
        await uiLoader.Init();
        UIContext.Instance.SetUILoader(uiLoader);
        SceneManager.LoadSceneAsync("UI");
    }

    void OnDestroy()
    {
        if (initHotFix != null)
        {
            initHotFix.Cleanup();
            initHotFix = null;
        }
    }
}
