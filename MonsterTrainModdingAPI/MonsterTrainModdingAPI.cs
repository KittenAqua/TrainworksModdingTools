using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using MonsterTrainModdingAPI.Interfaces;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Utilities;
using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace MonsterTrainModdingAPI
{
    // Credit to Rawsome, Stable Infery for the base of this method.
    /// <summary>
    /// The entry point for the API.
    /// </summary>
    [BepInPlugin("api.modding.train.monster", "Monster Train Modding API", "0.0.9.1")]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    public class API : BaseUnityPlugin
    {
        /// <summary>
        /// The API's logging source.
        /// </summary>
        private static readonly ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource("API");

        public static AssetBundle TrainworksBundle { get; private set; }
        public static string APIBasePath { get; private set; }
        /// <summary>
        /// Logs a message into the BepInEx console.
        /// </summary>
        /// <param name="lvl">The severity of the message</param>
        /// <param name="msg">The message to log</param>
        public static void Log(LogLevel lvl, string msg)
        {
            logger.Log(lvl, msg);
        }
        /// <summary>
        /// Called on startup. Executes all Harmony patches anywhere in the API.
        /// </summary>
        private void Awake()
        {
            API.Log(BepInEx.Logging.LogLevel.All, "We're awake here in API town");

            DepInjector.AddClient(new MonsterTrainModdingAPI.Managers.ProviderManager());
            API.Log(BepInEx.Logging.LogLevel.All, "Patching?");

            //Load the Trainworks Bundle, which will be used by the API for templating
            var assembly = this.GetType().Assembly;
            APIBasePath = Path.GetDirectoryName(assembly.Location);
            TrainworksBundle = AssetBundle.LoadFromFile(Path.Combine(APIBasePath, "trainworks"));

            var harmony = new Harmony("api.modding.train.monster");
            API.Log(BepInEx.Logging.LogLevel.All, "We Made a harmony");


            //Assembly.GetExecutingAssembly().GetTypes().Do(type => harmony.CreateClassProcessor(type).Patch());
            //var patchProcessors = Assembly.GetExecutingAssembly().GetTypes().Select(ProcessorForAnnotatedClass).Where(x => x != null).ToList();

            harmony.PatchAll();
            API.Log(BepInEx.Logging.LogLevel.All, "Patch complete");
        }
    }
}
