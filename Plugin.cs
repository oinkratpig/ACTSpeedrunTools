using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

namespace SpeedrunTools
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        public static ManualLogSource LoggerInstance { get; private set; }

        private Harmony _harmony;

        private void Awake()
        {
            LoggerInstance = Logger;

            _harmony = new(PluginInfo.PLUGIN_GUID);
            _harmony.PatchAll(typeof(PlayerPatch));

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

        } // end Awake

    } // end class Plugin

} // end namespace
