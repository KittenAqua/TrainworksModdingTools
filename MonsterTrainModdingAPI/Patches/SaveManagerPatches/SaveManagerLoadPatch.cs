using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using ShinyShoe;
using ShinyShoe.Logging;
using System.Reflection;
using System.Reflection.Emit;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI.Patches
{
    /// <summary>
    /// A Functional Rewrite of SaveManager's Load
    /// </summary>
    [HarmonyPatch(typeof(SaveManager), "Load")]
    class SaveManagerLoadPatch
    {
        static bool Prefix(ref bool __result, SaveManager __instance, RunType runType, string sharecode = "")
        {
            AccessTools.Field(typeof(SaveManager),"activeRunType").SetValue(__instance, runType);
            AccessTools.Field(typeof(SaveManager), "activeSharecode").SetValue(__instance, sharecode);
            if (runType != RunType.Class)
            {
                Log.Error(LogGroups.Save, "Non-Singleplayer Modded Save File was attempted to be Loaded");
                __result = false;
                return false;
            }
            SaveManagerExtension.moddedPlayerSave.saveData = (SaveData)AccessTools.Method(typeof(SaveManager), "LoadSaveFile").Invoke(__instance, null);
            AccessTools.Field(typeof(SaveManager), "mutableRules").SetValue(__instance, new MutableRules());
            if (SaveManagerExtension.moddedPlayerSave.saveData != null)
            {
                Log.Verbose(LogGroups.Determinism, string.Format("Load IsChallenge [{0}] RunId [{1}] Version [{2}]", __instance.IsChallengeOrShare(), SaveManagerExtension.moddedPlayerSave.saveData.GetID(), SaveManagerExtension.moddedPlayerSave.saveData.GetBuildVersion().number));
                if (__instance.GetGameSequence() == SaveData.GameSequence.TransitioningIntoSection)
                {
                    __instance.SetGameSequence(SaveData.GameSequence.DestinationReached, true);
                }
                RandomManager.Init(SaveManagerExtension.moddedPlayerSave.saveData.GetRandomState(), __instance);
                AccessTools.Method(typeof(SaveManager), "LoadRelicStatesFromFile").Invoke(__instance, null);
                AccessTools.Method(typeof(SaveManager), "LoadCovenantsFromFile").Invoke(__instance, null);
                AccessTools.Method(typeof(SaveManager), "LoadMutatorsFromFile").Invoke(__instance, null);
                AccessTools.Method(typeof(SaveManager), "LoadDeckFromFile").Invoke(__instance, null);
                AccessTools.Method(typeof(SaveManager), "LoadMapNodesFromFile").Invoke(__instance, null);
                AccessTools.Method(typeof(SaveManager), "LoadStoryEventsFromFile").Invoke(__instance, null);
                AccessTools.Method(typeof(SaveManager), "LoadGeneratedGrantableRewards").Invoke(__instance, null);
                AccessTools.Method(typeof(SaveManager), "LoadEventCachedCardState").Invoke(__instance, null);
                AccessTools.Method(typeof(SaveManager), "LoadMerchantGoodsFromFile").Invoke(__instance, null);
                RelicManager localRelicManager = (RelicManager)AccessTools.Field(typeof(SaveManager), "relicManager").GetValue(__instance);
                if (localRelicManager != null)
                {
                    localRelicManager.ResetRelicCounters();
                    List<RelicState> allRelics = __instance.GetAllRelics();
                    foreach (RelicState type in allRelics)
                    {
                        RelicManager.RelicAdded.Dispatch(allRelics, type, Team.Type.Monsters);
                    }
                }
                Signal signal = __instance.gameSequenceChangedSignal;
                if (signal != null)
                {
                    signal.Dispatch();
                }
                __result = true;
                return false;
            }
            __result = false;
            return false;
        }
    }
}
