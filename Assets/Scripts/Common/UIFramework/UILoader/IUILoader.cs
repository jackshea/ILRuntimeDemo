using System.Threading.Tasks;
using UnityEngine;

namespace Common
{
    public interface IUILoader
    {
        Task Init();
        Task<GameObject> LoadUI(string key);
        void UnloadUI(GameObject uiGameObject);
    }
}