using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ShinyShoe;

namespace MonsterTrainModdingAPI
{
    // Credit to Rawsome, Stable Infery for the base of this method.
    [BepInPlugin("api.modding.train.monster", "Monster Train Modding API", "0.0.1")]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    public class TestPlugin : BaseUnityPlugin
    {
        void Awake()
        {
            var harmony = new Harmony("api.modding.train.monster");
            harmony.PatchAll();
        }
    }

    // Not used yet; we still need to find the right entry point to initialize its SaveManager
    public class CustomCardManager
    {
        public static List<CardData> CustomCardData { get; } = new List<CardData>();
        public static SaveManager SaveManager { get; set; }

        public static void RegisterCustomCardData(CardData cardData)
        {
            CustomCardData.Add(cardData);
            SaveManager.GetAllGameData().GetAllCardData().Add(cardData);
        }

        public static ClassData CurrentPrimaryClan()
        {
            var saveData = (SaveData)AccessTools.Property(typeof(SaveManager), "ActiveSaveData").GetValue(SaveManager);
            ClassData mainClass = SaveManager.GetAllGameData().FindClassData(saveData.GetStartingConditions().Class);
            return mainClass;
        }

        public static ClassData CurrentAlliedClan()
        {
            var saveData = (SaveData)AccessTools.Property(typeof(SaveManager), "ActiveSaveData").GetValue(SaveManager);
            ClassData mainClass = SaveManager.GetAllGameData().FindClassData(saveData.GetStartingConditions().Class);
            return mainClass;
        }
    }
}
