using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace TrainworksModdingTools.Patches
{
    /// <summary>
    /// Adds custom relics to their appropriate pools.
    /// </summary>
    [HarmonyPatch(typeof(RelicPool), "GetAllChoices")]
    class AddCustomRelicToPoolPatch
    {
        static void Postfix(ref RelicPool __instance, ref List<CollectableRelicData> __result)
        {
            var customRelicsToAdd = TrainworksModdingTools.Managers.CustomRelicPoolManager.GetRelicsForPool(__instance.name);
            __result.AddRange(customRelicsToAdd);
        }
    }

    /// <summary>
    /// Without this patch, custom relics, when chosen from a pool, will be replaced by an empty slot.
    /// </summary>
    [HarmonyPatch(typeof(RelicPool), "FindRelic")]
    class AddCustomRelicToPoolPatch2
    {
        static void Postfix(ref CollectableRelicData __result, ref string relicID)
        {
            if (TrainworksModdingTools.Managers.CustomCollectableRelicManager.CustomRelicData.ContainsKey(relicID))
            {
                __result = TrainworksModdingTools.Managers.CustomCollectableRelicManager.CustomRelicData[relicID];
            }
        }
    }
}
