using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using HarmonyLib;
using MonsterTrainModdingAPI.Managers;
using System.Reflection.Emit;

namespace MonsterTrainModdingAPI.Patches
{
    /// <summary>
    /// Adds custom reward node data to their appropriate pools.
    /// </summary>
    [HarmonyPatch(typeof(RandomMapDataContainer), "GetCopyOfMapNodeDataList")]
    class AddCustomMapNodeToPoolPatch
    {
        static void Postfix(RandomMapDataContainer __instance, RunState.ClassType ____classTypeOverride, List<MapNodeData> __result)
        {
            CustomMapNodePoolManager.AddRewardNodesForPool(__instance.name, __result, ____classTypeOverride);
        }
    }
}
