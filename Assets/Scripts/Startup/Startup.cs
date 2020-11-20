using ILRuntime.Runtime.Enviorment;
using System.Collections;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Startup : MonoBehaviour
{
    AppDomain appdomain;
    System.IO.MemoryStream fs;
    System.IO.MemoryStream p;

    void Start()
    {
        StartCoroutine(LoadHotFixAssembly());
    }

    IEnumerator LoadHotFixAssembly()
    {
        appdomain = new AppDomain();

#if UNITY_ANDROID
        WWW www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.dll");
#else
        WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project.dll");
#endif
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] dll = www.bytes;
        www.Dispose();

        //PDB文件是调试数据库，如需要在日志中显示报错的行号，则必须提供PDB文件，不过由于会额外耗用内存，正式发布时请将PDB去掉，下面LoadAssembly的时候pdb传null即可
#if UNITY_ANDROID
        www = new WWW(Application.streamingAssetsPath + "/HotFix_Project.pdb");
#else
        www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project.pdb");
#endif
        while (!www.isDone)
            yield return null;
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] pdb = www.bytes;
        fs = new MemoryStream(dll);
        p = new MemoryStream(pdb);
        //fs=ReadFileFromAA()
        try
        {
            appdomain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        }
        catch
        {
            Debug.LogError("加载热更DLL失败，请确保已经通过VS打开Assets/Samples/ILRuntime/1.6/Demo/HotFix_Project/HotFix_Project.sln编译过热更DLL");
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
        appdomain.Invoke("HotFix_Project.InstanceClass", "StaticFunTest", null, null);

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
