using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace MonsterTrainModdingAPI.Patches
{
    [HarmonyPatch(typeof(CardState), "GetTitle")]
    class CustomCardNamePatch
    {
        static void Postfix(ref string __result, ref CardData __instance)
        {
            if (__result.StartsWith("KEY>>"))
            {
                __result = __instance.GetNameKey();
            }
        }
    }
}
