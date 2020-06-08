using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using BepInEx;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI.Patches
{
    [HarmonyPatch(typeof(BaseUnityPlugin), MethodType.Constructor)]
    class BaseUnityPluginConstructorPatch
    {
        static void Postfix(ref BaseUnityPlugin __instance)
        {
            PluginManager.RegisterPlugin(__instance);
        }
    }
}
