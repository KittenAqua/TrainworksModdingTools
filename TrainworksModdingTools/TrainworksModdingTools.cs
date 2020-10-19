using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using Trainworks.Interfaces;
using Trainworks.Managers;
using Trainworks.Utilities;
using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Trainworks
{
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
        /// The framework's logging source.
        /// </summary>
        private static readonly ManualLogSource logger = BepInEx.Logging.Logger.CreateLogSource("Trainworks");

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
        /// Called on startup. Executes all Harmony patches anywhere in the framework's assembly.
        /// </summary>
        private void Awake()
        {
            DepInjector.AddClient(new ProviderManager());

            // Load the Trainworks Bundle, which will be used by the framework for templating
            var assembly = this.GetType().Assembly;
            APIBasePath = Path.GetDirectoryName(assembly.Location);
            TrainworksBundle = AssetBundle.LoadFromFile(Path.Combine(APIBasePath, "trainworks"));

            var harmony = new Harmony(GUID);
            harmony.PatchAll();
        }
    }
}
