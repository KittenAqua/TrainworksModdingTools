using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using TrainworksModdingTools.Interfaces;
using TrainworksModdingTools.Managers;
using TrainworksModdingTools.Utilities;
using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace TrainworksModdingTools
{
    // Credit to Rawsome, Stable Infery for the base of this method.
    /// <summary>
    /// The entry point for the framework.
    /// </summary>
    [BepInPlugin(GUID, NAME, VERSION)]
    [BepInProcess("MonsterTrain.exe")]
    [BepInProcess("MtLinkHandler.exe")]
    public class Trainworks : BaseUnityPlugin
    {
        public const string GUID = "tools.modding.trainworks";
        public const string NAME = "Trainworks Modding Tools";
        public const string VERSION = "0.0.9.2";

        /// <summary>
        /// Built in asset bundle used for templating
        /// </summary>
        public static AssetBundle TrainworksBundle { get; private set; }

        /// <summary>
        /// The frameworks's logging source.
        /// </summary>
        private static readonly ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource("Trainworks");

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
        /// Called on startup. Executes all Harmony patches anywhere in the framework.
        /// </summary>
        private void Awake()
        {
            DepInjector.AddClient(new TrainworksModdingTools.Managers.ProviderManager());

            // Load the Trainworks Bundle, which will be used by the framework for templating
            var assembly = this.GetType().Assembly;
            var basePath = Path.GetDirectoryName(assembly.Location);
            TrainworksBundle = AssetBundle.LoadFromFile(Path.Combine(basePath, "trainworks"));

            var harmony = new Harmony(GUID);
            harmony.PatchAll();
        }
    }
}
