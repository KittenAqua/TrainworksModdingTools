//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Reflection;
//using HarmonyLib;
//using MonsterTrainModdingAPI.Managers;
//using System.Reflection.Emit;

//namespace MonsterTrainModdingAPI.Patches
//{
//    /// <summary>
//    /// Adds custom reward node data to their appropriate pools.
//    /// </summary>
//    [HarmonyPatch(typeof(RandomMapDataContainer), "GetMapNodeBucketData")]
//    class AddCustomMapNodeToPoolPatch
//    {
//        //static FieldInfo f_someField = AccessTools.Field(typeof(SomeType), "someField");
//        //static MethodInfo m_MyExtraMethod = SymbolExtensions.GetMethodInfo(() => Tools.MyExtraMethod());

//        static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
//        {
//            int newObjFound = 0;
//            foreach (var instruction in instructions)
//            {
//                yield return instruction;
//                if (newObjFound == 2)
//                { // Insert into the map node list
//                    UnityEngine.Debug.Log("FOUND");
//                    var method = AccessTools.Method(typeof(CustomMapNodeManager), "GetRewardNodesForPool");
//                    yield return new CodeInstruction(OpCodes.Ldloc_3);
//                    yield return new CodeInstruction(OpCodes.Newobj, method);
//                    newObjFound++; // Increment so we don't do this more than once
//                }
//                if (instruction.opcode.ToString() == "newobj")
//                {
//                    newObjFound++;
//                }
//            }
//        }

//        //static void Postfix(ref RandomMapDataContainer __instance)
//        //{
//        //    //List<RewardNodeData> customNodesToAddToPool = CustomMapNodeManager.GetRewardNodesForPool(__instance);
//        //    var mapNodeDataList = (Malee.ReorderableArray<MapNodeData>)AccessTools.Field(typeof(RandomMapDataContainer), "mapNodeDataList").GetValue(__instance);
//        //    UnityEngine.Debug.Log("COUNT: " + mapNodeDataList.Count + "  " + __instance.name);
//        //    //if (__instance.name == "RandomChosenMainClassUnit" || __instance.name == "RandomChosenSubClassUnit")
//        //    //{
//        //    //    var rewardNode = GameObject.Instantiate(mapNodeDataList[0]);
//        //    //    rewardNode.name = "RewardNodeUnitPackTest";
//        //    //    AccessTools.Field(typeof(RewardNodeData), "requiredClass").SetValue(rewardNode, CustomClassManager.GetClassDataByID("TestMod_TestClan"));
//        //    //    mapNodeDataList.Add(rewardNode);
//        //    //}
//        //    foreach (MapNodeData mapNodeData in mapNodeDataList)
//        //    {
//        //        UnityEngine.Debug.Log(mapNodeData.name + " " + mapNodeData.GetID());
//        //    }
//        //    //__result.AddRange(customCardsToAddToPool);
//        //}
//    }
//}
