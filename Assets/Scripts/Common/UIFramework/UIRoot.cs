using Ujoy19.Common;
using UnityEngine;

namespace Common
{
    public class UIRoot : MonoBehaviour
    {
        public static UIRoot Instance { get; private set; }

        public Transform MainUIRoot;
        public Transform CombatUIRoot;
        public Transform Overlay;

        public Canvas Canvas { get; private set; }

        public void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            Canvas = GetComponent<Canvas>();
            WindowsManager.Instance.OpenStartUI();
        }
    }
}