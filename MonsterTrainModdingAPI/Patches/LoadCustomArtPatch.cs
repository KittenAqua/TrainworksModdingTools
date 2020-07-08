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
    /// <summary>
    /// Loads custom card art.
    /// </summary>
    [HarmonyPatch(typeof(CardData), "GetCardArtPrefabVariant")]
    class LoadCustomCardArtPatch
    {
        static void Prefix(ref CardData __instance)
        {
            if (!__instance.HasCardArtPrefabVariant() && CustomCardManager.CustomCardData.ContainsKey(__instance.GetID()))
            {
                GameObject obj = CustomCardManager.CreateCardGameObject(__instance.GetID());

                if (obj != null)
                {
                    UnityEngine.Object.DontDestroyOnLoad(obj);
                }
            }
        }
    }

    /// <summary>
    /// Loads custom character art.
    /// </summary>
    [HarmonyPatch(typeof(CharacterData), "GetCharacterPrefabVariant")]
    class LoadCustomCharacterArtPatch
    {
        static void Prefix(ref CharacterData __instance)
        {
            if (!__instance.HasCharacterPrefabVariant() && CustomCharacterManager.CustomCharacterData.ContainsKey(__instance.GetID()))
            {
                GameObject obj = CustomCharacterManager.CreateCharacterGameObject(__instance.GetID());
                if (obj != null)
                {
                    UnityEngine.Object.DontDestroyOnLoad(obj);
                }
            }
        }
    }
}
