﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BepInEx.Logging;
using MonsterTrainModdingAPI.Builders;
using HarmonyLib;
using UnityEngine;
using ShinyShoe;
using UnityEngine.AddressableAssets;
using MonsterTrainModdingAPI.Utilities;

namespace MonsterTrainModdingAPI.Managers
{
    /// <summary>
    /// Handles registration and storage of custom relic data.
    /// </summary>
    public class CustomCollectableRelicManager
    {
        /// <summary>
        /// Maps custom relic IDs to their respective RelicData.
        /// </summary>
        public static IDictionary<string, CollectableRelicData> CustomRelicData { get; } = new Dictionary<string, CollectableRelicData>();
        /// <summary>
        /// Static reference to the game's SaveManager, which is necessary to register new relics.
        /// </summary>
        public static SaveManager SaveManager { get; set; }

        /// <summary>
        /// Register a custom relic with the manager, allowing it to show up in game.
        /// </summary>
        /// <param name="relicData">The custom relic data to register</param>
        /// <param name="relicPoolData">The pools to insert the custom relic data into</param>
        public static void RegisterCustomRelic(CollectableRelicData relicData, List<string> relicPoolData)
        {
            CustomRelicData.Add(relicData.GetID(), relicData);
            CustomRelicPoolManager.AddRelicToPools(relicData, relicPoolData);
            SaveManager.GetAllGameData().GetAllCollectableRelicData().Add(relicData);
        }

        /// <summary>
        /// Get the relic data corresponding to the given ID
        /// </summary>
        /// <param name="relicID">ID of the relic to get</param>
        /// <returns>The relic data for the given ID</returns>
        public static CollectableRelicData GetRelicDataByID(string relicID)
        {
            // Search for custom relic matching ID
            var guid = GUIDGenerator.GenerateDeterministicGUID(relicID);
            if (CustomRelicData.ContainsKey(guid))
            {
                return CustomRelicData[guid];
            }

            // No custom relic found; search for vanilla relic matching ID
            var vanillaRelic = SaveManager.GetAllGameData().FindCollectableRelicData(relicID);
            if (vanillaRelic == null)
            {
                API.Log(LogLevel.All, "Couldn't find relic: " + relicID + " - This will cause crashes.");
            }
            return vanillaRelic;
        }
    }
}
