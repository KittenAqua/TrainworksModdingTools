using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using BepInEx;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI.Patches
{
    //If this Patch doesnt work, try solution at: https://ludeon.com/forums/index.php?topic=38328.0
    [HarmonyPatch(typeof(BaseUnityPlugin), MethodType.Constructor)]
    class BaseUnityPluginConstructorPatch
    {
        static void Postfix(ref BaseUnityPlugin __instance)
        {
            PluginManager.RegisterPlugin(__instance);
        }
    }
}
