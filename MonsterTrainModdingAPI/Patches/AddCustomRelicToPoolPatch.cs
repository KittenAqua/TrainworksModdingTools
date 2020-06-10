using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace MonsterTrainModdingAPI.Patches
{
    /// <summary>
    /// Adds custom relics to their appropriate pools.
    /// </summary>
    [HarmonyPatch(typeof(RelicPool), "GetAllChoices")]
    class AddCustomRelicToPoolPatch
    {
        static void Postfix(ref RelicPool __instance, ref List<CollectableRelicData> __result)
        {
            List<CollectableRelicData> newResult = new List<CollectableRelicData>();
            foreach (CollectableRelicData relicData in __result)
            {
                if (relicData.name == "AttackDecreaseEnemies" || relicData.name == "AddImpToHand")
                {
                    newResult.Add(relicData);
                }
            }
            newResult.AddRange(MonsterTrainModdingAPI.Managers.CustomCollectableRelicManager.CustomRelicData.Values);
            __result = newResult;
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
            if (MonsterTrainModdingAPI.Managers.CustomCollectableRelicManager.CustomRelicData.ContainsKey(relicID))
            {
                __result = MonsterTrainModdingAPI.Managers.CustomCollectableRelicManager.CustomRelicData[relicID];
            }
        }
    }
}
