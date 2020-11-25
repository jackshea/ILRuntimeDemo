using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Ujoy19.Common
{
    public class AssetLoader
    {
        public static async Task<T> LoadAsset<T>(string key)
        {
            var handle = Addressables.LoadAssetAsync<T>(key);
            return await handle.Task;
        }

        public static void ReleaseAsset<T>(T asset)
        {
            Addressables.Release(asset);
        }

        public static async Task<GameObject> InstantiateAsync(string key)
        {
            var handle = Addressables.InstantiateAsync(key);
            return await handle.Task;
        }

        public static bool ReleaseInstance(GameObject gameObject)
        {
            return Addressables.ReleaseInstance(gameObject);
        }
    }
}