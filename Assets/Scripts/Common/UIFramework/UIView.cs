using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Common
{
    // 一般的UI视图组件
    public class UIView : MonoBehaviour
    {
        public GameObject[] GameObjectRefs;
        public Dictionary<string, GameObject> GoMap;

        public void Start()
        {
            GoMap = GameObjectRefs.ToDictionary(p => p.name);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Cleanup()
        {
            if (GoMap != null)
            {
                GoMap.Clear();
                GoMap = null;
            }
        }
    }
}