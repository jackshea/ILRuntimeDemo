using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Common
{
    public class Utils
    {
        public static string ReadFile(string filePath)
        {
#if !UNITY_ANDROID || UNITY_EDITOR
            string content = "";
            try
            {
                content = File.ReadAllText(filePath);
            }
            catch { }

            return content;
#else
            WWW www = new WWW(filePath);
            while (!www.isDone) { }
            return www.text;
#endif
        }

        /// 加权随机。返回随机到的索引
        public static int RandomWithWeight(List<float> weights)
        {
            var sum = weights.Sum();
            var randWeight = Random.Range(0, sum);
            for (int i = weights.Count - 1; i >= 0; i--)
            {
                sum -= weights[i];
                if (randWeight >= sum)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}