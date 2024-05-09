using HarmonyLib;
using UnityEngine;

namespace SpeedrunTools
{
    internal class PlayerPatch
    {
        /// <summary>
        /// Patch: God mode. Prevents health loss.
        /// </summary>
        [HarmonyPatch(typeof(Player), "health", MethodType.Setter)] [HarmonyPrefix]
        public static bool SetHealth(Player __instance, float value)
        {
            if (!SpeedrunTools.GodMode || value > __instance.health)
                return true;
            else
                return false;

        } // end SetHealth

        /// <summary>
        /// Patch: Toggle tools.
        /// </summary>
        [HarmonyPatch(typeof(Player), "Update")] [HarmonyPostfix]
        public static void Update()
        {
            Plugin.LoggerInstance.LogInfo("update");

            // Toggle God mode
            if (Input.GetKeyDown(SpeedrunTools.KeyCodes[SpeedrunTools.Tools.GodMode]))
            {
                Plugin.LoggerInstance.LogInfo("Toggling God mode.");
                SpeedrunTools.GodMode = !SpeedrunTools.GodMode;
            }

        } // end Update

    } // end class PlayerPatch

} // end namespace