using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using AppDomain = ILRuntime.Runtime.Enviorment.AppDomain;

public class Startup : MonoBehaviour
{
    AppDomain appdomain;
    MemoryStream fs;
    MemoryStream p;

    void Start()
    {
        LoadHotFixAssembly();
    }

    private async void LoadHotFixAssembly()
    {
        appdomain = new AppDomain();
        fs = await ReadFileFromAA("HotFixDlls/HotFixProject.dll.bytes");
        p = await ReadFileFromAA("HotFixDlls/HotFixProject.pdb.bytes");
        try
        {
            appdomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        }
        catch (Exception ex)
        {
            Debug.LogError("加载热更DLL失败. ex = " + ex.ToString());
        }

        InitializeILRuntime();
        OnHotFixLoaded();
    }

    void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        //由于Unity的Profiler接口只允许在主线程使用，为了避免出异常，需要告诉ILRuntime主线程的线程ID才能正确将函数运行耗时报告给Profiler
        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
        //这里做一些ILRuntime的注册，HelloWorld示例暂时没有需要注册的
    }

    void OnHotFixLoaded()
    {
        //HelloWorld，第一次方法调用
        appdomain.Invoke("HotFixProject.Startup", "Hello", null, null);
        //appdomain.Invoke("HotFix_Project.InstanceClass", "StaticFunTest", null, null);
    }

    private async Task<MemoryStream> ReadFileFromAA(string key)
    {
        var asset = await Addressables.LoadAssetAsync<TextAsset>(key).Task;
        return new MemoryStream(asset.bytes);
    }

    private void OnDestroy()
    {
        if (fs != null)
            fs.Close();
        if (p != null)
            p.Close();
        fs = null;
        p = null;
    }
}
