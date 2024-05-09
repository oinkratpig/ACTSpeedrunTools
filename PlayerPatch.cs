using HarmonyLib;

namespace SpeedrunTools
{
    internal class PlayerPatch
    {
        [HarmonyPatch(typeof(Player), "health", MethodType.Setter)]
        public static bool SetHealth(Player __instance, float value)
        {
            if (value > __instance.health)
                return true;
            else
                return false;

        } // end SetHealth

    } // end class PlayerPatch

} // end namespace