using HarmonyLib;
using JetBrains.Annotations;
using Kingmaker.Blueprints.JsonSystem;
using System;
using BrokenFeatures.Config;
using BrokenFeatures.Utilities;
using UnityModManagerNet;
using System.Reflection;

namespace BrokenFeatures
{
    static class Main
    {
        public static bool Enabled;
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            try
            {
                modEntry.OnToggle = OnToggle;
                var harmony = new Harmony(modEntry.Info.Id);
                ModSettings.ModEntry = modEntry;
                ModSettings.LoadAllSettings();
                harmony.PatchAll(Assembly.GetExecutingAssembly());
                PostPatchInitializer.Initialize();
                return true;
            }
            catch (System.Reflection.ReflectionTypeLoadException ex)
            {
                foreach (Exception inner in ex.LoaderExceptions)
                {
                    Log(inner.ToString());
                }
            }
            return false;
        }
        static bool OnToggle(UnityModManager.ModEntry modEntry, bool value)
        {
            Enabled = value;
            return true;
        }
        public static void Log(string msg)
        {
            ModSettings.ModEntry.Logger.Log(msg);
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public static void LogDebug(string msg)
        {
            ModSettings.ModEntry.Logger.Log(msg);
        }
        public static void LogPatch(string action, [NotNull] IScriptableObjectWithAssetId bp)
        {
            Log($"{action}: {bp.AssetGuid} - {bp.name}");
        }
        public static void LogHeader(string msg)
        {
            Log($"--{msg.ToUpper()}--");
        }
        public static Exception Error(String message)
        {
            Log(message);
            return new InvalidOperationException(message);
        }
    }
}