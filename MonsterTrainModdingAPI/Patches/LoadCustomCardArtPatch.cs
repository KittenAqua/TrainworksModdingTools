using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using ShinyShoe;
using Spine.Unity;
using MonsterTrainModdingAPI.Managers;
using TMPro;

namespace MonsterTrainModdingAPI.Patches
{
    [HarmonyPatch(typeof(CardData), "GetCardArtPrefabVariant")]
    class LoadCustomCardArtPatch
    {
        static void Prefix(ref CardData __instance)
        {
            if (!__instance.HasCardArtPrefabVariant() && CustomCardManager.CustomCardData.ContainsKey(__instance.GetID()))
            {
                CustomCardManager.CreateCardGameObject(__instance.GetID());
            }
        }
    }

    [HarmonyPatch(typeof(CharacterData), "GetCharacterPrefabVariant")]
    class LoadCustomCharacterArtPatch
    {
        static void Prefix(ref CharacterData __instance, ref GameObject __result)
        {
            if (!__instance.HasCharacterPrefabVariant() && CustomCharacterManager.CustomCharacterData.ContainsKey(__instance.GetID()))
            {
                CustomCharacterManager.CreateCharacterGameObject(__instance.GetID());
            }
        }
    }
}
