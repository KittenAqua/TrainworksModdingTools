using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace MonsterTrainModdingAPI.Patches
{
    /// <summary>
    /// Allows for custom names to show up on cards.
    /// If a card's name key isn't found in the localization table,
    /// the game will append "KEY>>" to the start and use that as the name.
    /// This circumvents that, using the name key directly.
    /// </summary>
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

    /// <summary>
    /// The internal name of the card. Never actually seen in game.
    /// </summary>
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
