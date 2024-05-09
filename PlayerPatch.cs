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
            // Toggle God mode
            if (Input.GetKeyDown(SpeedrunTools.KeyCodes[SpeedrunTools.Tools.GodMode]))
            {
                SpeedrunTools.GodMode = !SpeedrunTools.GodMode;
                Plugin.LoggerInstance.LogInfo($"God mode now {((SpeedrunTools.GodMode) ? "enabled" : "disabled")}.");
            }

        } // end Update

    } // end class PlayerPatch

} // end namespace