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
        static void Postfix(ref string __result, ref string ___titleKey)
        {
            if (__result.StartsWith("KEY>>"))
            {
                __result = ___titleKey;
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

    /// <summary>
    /// Allows for the display of custom relic names.
    /// </summary>
    [HarmonyPatch(typeof(RelicState), "GetName")]
    class CustomRelicNamePatch
    {
        static void Postfix(ref string __result, ref string ___nameKey)
        {
            if (__result.StartsWith("KEY>>"))
            {
                __result = ___nameKey;
            }
        }
    }

    /// <summary>
    /// Allows for the display of custom relic descriptions.
    /// </summary>
    [HarmonyPatch(typeof(RelicState), "GetDescription")]
    class CustomRelicDescriptionPath
    {
        static void Postfix(ref string __result, ref RelicData __instance, string ___descriptionKey)
        {
            if (!___descriptionKey.HasTranslation() && __result == string.Empty)
            {
                __result = ___descriptionKey;
            }
        }
    }

    /// <summary>
    /// Allows for the display of custom class names.
    /// </summary>
    [HarmonyPatch(typeof(ClassData), "GetTitleKey")]
    class CustomClassDataTitlePatch
    {
        static void Postfix(ref string __result, ref string ___titleLoc)
        {
            if (!___titleLoc.HasTranslation() && __result == string.Empty)
            {
                __result = ___titleLoc;
            }
        }
    }

    /// <summary>
    /// Allows for the display of custom class descriptions.
    /// </summary>
    [HarmonyPatch(typeof(ClassData), "GetDescriptionKey")]
    class CustomClassDataDescriptionPatch
    {
        static void Postfix(ref string __result, ref string ___descriptionLoc)
        {
            if (!___descriptionLoc.HasTranslation() && __result == string.Empty)
            {
                __result = ___descriptionLoc;
            }
        }
    }
}
