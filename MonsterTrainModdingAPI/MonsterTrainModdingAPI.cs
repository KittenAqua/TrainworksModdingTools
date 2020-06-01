using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using MonsterTrainModdingAPI.Builder;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Models;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ShinyShoe;

namespace MonsterTrainModdingAPI
{
    // Credit to Rawsome, Stable Infery for the base of this method.
    [BepInPlugin("api.modding.train.monster", "Monster Train Modding API", "0.0.3")]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    public class API : BaseUnityPlugin
    {
        public static List<TrainModule> plugins;
        private static ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource("API");
        
        public static void Log(LogLevel lvl, string msg)
        {
            logger.Log(lvl, msg);
        }
        
        private void Awake()
        {
            var harmony = new Harmony("api.modding.train.monster");
            harmony.PatchAll();
            Initialize();
        }

        private void Initialize()
        {
            FillPluginsList();
            CustomCardManager.RegisterAllCustomCards();
        }
        
        private void FillPluginsList()
        {
            plugins = new List<TrainModule>();
            // Somebody figure out how to fill in this list of mods right here
        }
    }
}
