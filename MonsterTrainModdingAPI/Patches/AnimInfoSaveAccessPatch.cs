using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using ShinyShoe;
using UnityEngine;

namespace MonsterTrainModdingAPI.Patches
{
    /// <summary>
    /// Prevents an error from uninstantiated animInfos
    /// </summary>
    [HarmonyPatch(typeof(CharacterUIMeshSpine), "GetAnimInfo")]
    class AnimInfoSaveAccessPatch
    {
        public static bool Prefix(ref object __result,ref CharacterUIMeshSpine __instance)
        {
            if (AccessTools.Field(typeof(CharacterUIMeshSpine), "_animInfos").GetValue(__instance) == null)
            {
                __result = null;
                return false;
            }
            return true;
        }
    }
}
