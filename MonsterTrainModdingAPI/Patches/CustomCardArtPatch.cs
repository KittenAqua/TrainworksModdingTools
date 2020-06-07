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
using MonsterTrainModdingAPI.Utilities;

namespace MonsterTrainModdingAPI.Patches
{
    [HarmonyPatch(typeof(CharacterData), "GetCharacterPrefabVariant")]
    class LoadCustomCharacterArtPatch
    {
        static void Postfix(ref CharacterData __instance, ref GameObject __result)
        {
            if (__result == null && CustomCharacterManager.CustomCharacterData.ContainsKey(__instance.GetID()))
            {
                __result = CustomCharacterManager.CreateCharacterGameObject(__instance.GetID());
            }
        }
    }
}
