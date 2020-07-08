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
    [HarmonyPatch(typeof(RandomMapDataContainer), "GetMapNodeBucketData")]
    class AddCustomMapNodeToPoolPatch
    {
        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            int newObjFound = 0;
            foreach (var instruction in instructions)
            {
                UnityEngine.Debug.Log(instruction.opcode);
                yield return instruction;
                if (newObjFound == 2)
                { // Insert custom nodes into the map node list local variable
                    var nameGetter = AccessTools.PropertyGetter(typeof(RandomMapDataContainer), "name");
                    var addRewardsMethod = AccessTools.Method(typeof(CustomMapNodeManager), "AddRewardNodesForPool");
                    var classTypeOverrideField = AccessTools.Field(typeof(RandomMapDataContainer), "classTypeOverride");
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Call, nameGetter);
                    yield return new CodeInstruction(OpCodes.Ldloc_3);
                    yield return new CodeInstruction(OpCodes.Ldarg_0);
                    yield return new CodeInstruction(OpCodes.Ldfld, classTypeOverrideField);
                    yield return new CodeInstruction(OpCodes.Call, addRewardsMethod);
                    newObjFound++; // Increment so we don't do this more than once
                }
                if (instruction.opcode.ToString() == "newobj")
                { // Search for the second newobj opcode, which initializes the local variable for the map node list
                    newObjFound++;
                }
            }
        }
    }
}
