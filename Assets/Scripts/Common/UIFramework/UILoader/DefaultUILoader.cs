using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Common
{
    public class DefaultUILoader : IUILoader
    {
        private Dictionary<string, string> key2AssetPath = new Dictionary<string, string>();
        public async Task Init()
        {
            await Addressables.InitializeAsync().Task;
            var prefabNameRegex = new Regex(@"([^\\/]*)\.prefab");
            foreach (var resourceLocator in Addressables.ResourceLocators)
            {
                foreach (var key in resourceLocator.Keys)
                {
                    var skey = key as string;
                    if (skey == null || !skey.StartsWith("UI/") || !skey.EndsWith(".prefab"))
                    {
                        continue;
                    }
                    string name = prefabNameRegex.Match(skey).Groups[1].Value;
                    key2AssetPath.Add(name, skey);
                }
            }
        }

        public async Task<GameObject> LoadUI(string key)
        {
            if (!key2AssetPath.TryGetValue(key, out var path))
            {
                return null;
            }

            return await AssetLoader.InstantiateAsync(path);
        }

        public void UnloadUI(GameObject uiGameObject)
        {
            AssetLoader.ReleaseInstance(uiGameObject);
        }
    }
}