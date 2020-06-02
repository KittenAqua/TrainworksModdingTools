using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using ShinyShoe;
using ShinyShoe.Logging;
using System.Reflection;
using UnityEngine;
using HarmonyLib;

namespace MonsterTrainModdingAPI.Patches.SaveManagerPatches
{
    /// <summary>
    /// A Functional Rewrite of SaveManager's Reset Save
    /// </summary>
    [HarmonyPatch(typeof(SaveManager), "ResetSave")]
    class SaveManagerResetSavePatch
    {
        static bool Prefix(SaveManager __instance, RunType? runTypeNullable, string sharecode = "")
        {

            RunType runType = RunType.None;
            if (runTypeNullable == null)
            {
                runType = (RunType)AccessTools.Field(typeof(SaveManager), "activeRunType").GetValue(__instance);
                sharecode = (string)AccessTools.Field(typeof(SaveManager), "activeSharecode").GetValue(__instance);
            }
            else
            {
                runType = runTypeNullable.Value;
            }
            if (runType == RunType.None)
            {
                return false;
            }
            AccessTools.Field(typeof(SaveManager), "activeRunType").SetValue(__instance, runType);
            AccessTools.Field(typeof(SaveManager), "activeSharecode").SetValue(__instance, sharecode);
            if (File.Exists(__instance.ActiveSavePath))
            {
                File.Delete(__instance.ActiveSavePath);
            }
            Log.Verbose(LogGroups.Save, string.Format("Resetting save of type {0} (sharecode: {1})", runType, sharecode));
            __instance.SetFtueScenarioSeenIfAdvanced();
            SaveManagerExtension.moddedPlayerSave.saveData = ScriptableObject.CreateInstance<SaveData>();
            if (!__instance.GetHasSeenFtueScenario())
            {
                SaveManagerExtension.moddedPlayerSave.saveData.SetIsFtueRun(true);
            }
            StoryManager story = (StoryManager)AccessTools.Field(typeof(SaveManager), "storyManager").GetValue(__instance);
            if (story != null)
            {
                story.ResetRun();
            }
            AccessTools.Field(typeof(SaveManager), "mutableRules").SetValue(__instance, new MutableRules());
            PreferencesManager preferences = (PreferencesManager)AccessTools.Field(typeof(SaveManager), "preferencesManager").GetValue(__instance);
            if (preferences != null)
            {
                SaveManagerExtension.moddedPlayerSave.saveData.SetPlayerIdentity(preferences.AnalyticsUserId, preferences.AnalyticsUserFriendlyName);
            }
            //
            int num = UnityEngine.Random.Range(0, int.MaxValue);
            int forceSeed = (int)AccessTools.Field(typeof(SaveManager), "forceSeed").GetValue(__instance);
            if (forceSeed != 0)
            {
                Log.Verbose(LogGroups.Save, string.Format("Using forced seed {0}", forceSeed));
                num = forceSeed;
            }
            else
            {
                Log.Verbose(LogGroups.Save, string.Format("Starting new save file with seed {0}", num));
            }
            SaveManagerExtension.moddedPlayerSave.saveData.InitializeSsId();
            SaveManagerExtension.moddedPlayerSave.saveData.SetSeed(num);
            __instance.ResetRandomState();
            SaveManagerExtension.moddedPlayerSave.saveData.SetVersion(63);
            AllGameData agd = (AllGameData)AccessTools.Field(typeof(SaveManager), "allGameData").GetValue(__instance);
            SaveManagerExtension.moddedPlayerSave.saveData.SetMaxTowerHP(agd.GetBalanceData().GetStartingTowerHP());
            AccessTools.Method(typeof(SaveManager), "SetTowerHP", new Type[] { typeof(int) }).Invoke(__instance, new System.Object[] { agd.GetBalanceData().GetStartingTowerHP() });
            __instance.SetGold(agd.GetBalanceData().GetStartingGold(), true);
            __instance.ResetCombatScore();
            __instance.ResetRunTimeAccumulator();
            __instance.Save(true);
            __instance.saveGameChangedSignal.Dispatch();
            return false;

        }
    }
}
