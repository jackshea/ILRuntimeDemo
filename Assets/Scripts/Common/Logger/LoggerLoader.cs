using System;
using System.IO;
using NLog;
using NLog.LayoutRenderers;
using NLog.Targets;
using Ujoy19.Common;
using UnityEngine;

namespace Logger
{
    public class LoggerLoader
    {
        public void Load()
        {
            Target.Register<UnityConsoleTarget>("UnityConsole");
            LayoutRenderer.Register("frameIndex", (logEvent) => Time.frameCount.ToString());
            var startTime = DateTime.Now.ToString("s");
            LayoutRenderer.Register("startTime", (logEvent) => startTime);
            string configFile = GetConfigFile();
            LogManager.LoadConfiguration(configFile);
            if (string.IsNullOrEmpty(LogManager.Configuration.Variables["LogRoot"].Text))
            {
                if (Application.isEditor)
                {
                    LogManager.Configuration.Variables["LogRoot"] = "D:/COTDLog";
                }
                else
                {
                    LogManager.Configuration.Variables["LogRoot"] = Application.persistentDataPath;
                }
            }
        }

        public string GetConfigFile()
        {
            string configFile = Path.Combine(Application.streamingAssetsPath, "NLog", "nlog.config");
#if UNITY_ANDROID
            var contents = Utils.ReadFile(configFile);
            string dir = Path.Combine(Application.temporaryCachePath, "NLog");
            Directory.CreateDirectory(dir);
            configFile = Path.Combine(dir, "nlog.config");
            File.WriteAllText(configFile, contents);
#endif
            //Debug.Log($"ConfigName = {configFile}");
            //Debug.Log($"isFileExist = {File.Exists(configFile)}");
            return configFile;
        }
    }
}

