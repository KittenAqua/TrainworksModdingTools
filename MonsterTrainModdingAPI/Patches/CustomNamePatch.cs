using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace MonsterTrainModdingAPI.Patches
{
    // The title that is displayed on the card
    [HarmonyPatch(typeof(CardState), "GetTitle")]
    class CustomCardTitlePatch
    {
        static void Postfix(ref string __result, ref CardData __instance)
        {
            if (__result.StartsWith("KEY>>"))
            {
                __result = __instance.GetNameKey();
            }
        }
    }

    [HarmonyPatch(typeof(CardData), "GetName")]
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
