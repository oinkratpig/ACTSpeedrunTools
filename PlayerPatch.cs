using HarmonyLib;
using System.Collections;
using System.Reflection;
using UnityEngine;


namespace SpeedrunTools
{
    internal class PlayerPatch : MonoBehaviour
    {
        static bool hasSetMat;
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
            if (Input.GetKeyDown(SpeedrunTools.KeyCodes[SpeedrunTools.Tools.CALTrainer]))
            {
                switch (SpeedrunTools.CalTrainer)
                {
                    case 0: SpeedrunTools.CalTrainer = 1; break;
                    case 1: SpeedrunTools.CalTrainer = 2; break;
                    case 2: SpeedrunTools.CalTrainer = 0; break;

                }
            }

        } // end Update


        

        /// <summary>
        /// Patch: CAL Timing Trainer
        /// </summary>
        [HarmonyPatch(typeof(Player), "Update")]
        [HarmonyPrefix]
        public static void CALJump(Player __instance)
        {
            SkinnedMeshRenderer mr = __instance.transform.GetComponentInChildren<SkinnedMeshRenderer>(true);
            MethodInfo method = AccessTools.Method(typeof(Player), "Jump");
            Material mat = null;

            //Blink near correct timing
            if (__instance.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .525 && __instance.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .575 && __instance.anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "an_Kril_AtkChargeThrust")
            {

                if (mr != null && hasSetMat == false && SpeedrunTools.CalTrainer > 0)
                {
                    mat = mr.material;
                    hasSetMat = true;
                    Debug.Log("found");
                    mr.SetMaterial(null);
                    mr.material.color = Color.blue;
                    __instance.StartCoroutine(ColorFlashCoroutine(mat, mr));
                }
            }
            //Auto Jump
            if (__instance.anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= .55 && __instance.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < .65 && __instance.anim.GetCurrentAnimatorClipInfo(0)[0].clip.name == "an_Kril_AtkChargeThrust")
            {


                if (SpeedrunTools.CalTrainer == 2)
                {
                    method.Invoke(__instance, null);
                }
            }

        }
        public static IEnumerator ColorFlashCoroutine(Material mat, SkinnedMeshRenderer mr)
        {
            Debug.Log("In Coroutine");
            yield return new WaitForSeconds(.1f);
            mr.SetMaterial(mat);
            hasSetMat = false;
        }
    } // end class PlayerPatch

} // end namespace