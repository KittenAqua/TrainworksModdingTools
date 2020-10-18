using HarmonyLib;
using TrainworksModdingTools.Managers;
using System;
using System.Collections.Generic;
using System.Text;

namespace TrainworksModdingTools.Patches
{
    /// <summary>
    /// This patch allows the getters for subtype data to return custom values.
    /// </summary>
    [HarmonyPatch(typeof(SubtypeManager), "GetSubtypeData", new Type[] { typeof(string) })]
    class CustomSubtypePatch
    {
        static void Postfix(ref SubtypeData __result, string key)
        {
            if (key == null) { return; }
            if (CustomCharacterManager.CustomSubtypeData.ContainsKey(key))
                CustomCharacterManager.CustomSubtypeData.TryGetValue(key, out __result);
        }
    }

    /// <summary>
    /// This patch allows the getters for subtype data to return custom values as part of a list lookup.
    /// </summary>
    [HarmonyPatch(typeof(SubtypeManager), "GetSubtypeData", new Type[] { typeof(List<string>) })]
    class CustomSubtypeListPatch
    {
        static void Postfix(ref List<SubtypeData> __result, List<string> keys)
        {
            if (keys == null) { return; }

            SubtypeData sub;
            foreach (var key in keys)
            {
                if (CustomCharacterManager.CustomSubtypeData.ContainsKey(key))
                {
                    CustomCharacterManager.CustomSubtypeData.TryGetValue(key, out sub);
                    __result.Insert(0, sub);
                }
            }
        }
    }
}
