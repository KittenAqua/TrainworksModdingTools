using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Malee;
using MonsterTrainModdingAPI.Managers;
using UnityEngine;

namespace MonsterTrainModdingAPI.Patches
{
    /// <summary>
    /// Adds custom relics to their appropriate pools.
    /// </summary>
    [HarmonyPatch(typeof(EnhancerPool), "GetAllChoices")]
    class AddCustomEnhancerToPoolPatch
    {
        static void Postfix(ref EnhancerPool __instance, ref List<EnhancerData> __result)
        {
            if (CustomEnhancerPoolManager.CustomEnhancerPoolData.ContainsKey(__instance.name))
                __result.AddRange(CustomEnhancerPoolManager.CustomEnhancerPoolData[__instance.name]);
        }
    }

    public class EnhancerDataList : ReorderableArray<EnhancerData> { }

    /// <summary>
    /// Without this patch, custom relics, when chosen from a pool, will be replaced by an empty slot.
    /// </summary>
    [HarmonyPatch(typeof(EnhancerPool), "GetFilteredChoices")]
    class AddCustomEnhancerToFilteredPoolPatch
    {
        static bool Prefix(EnhancerPool __instance, ref List<EnhancerData> __result, PoolRewardData.RandomChoiceData randomChoiceData)
        {
            var newList = new EnhancerDataList();
            newList.CopyFrom(__instance.GetAllChoices());

            __result = PoolRewardData.GetFilteredChoices(new PoolRewardData.RandomChoiceParameters<EnhancerData>
            {
                list = newList,
                randomChoiceData = randomChoiceData,
                rarityTicketType = PoolRewardData.RarityTicketType.Enhancer
            });

            return false;
        }
    }
}
