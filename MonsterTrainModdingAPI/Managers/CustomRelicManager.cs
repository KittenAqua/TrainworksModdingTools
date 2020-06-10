using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using BepInEx.Logging;
using MonsterTrainModdingAPI.Builders;
using HarmonyLib;
using UnityEngine;
using ShinyShoe;
using UnityEngine.AddressableAssets;

namespace MonsterTrainModdingAPI.Managers
{
    /// <summary>
    /// Handles registration and storage of custom relic data.
    /// </summary>
    class CustomCollectableRelicManager
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
        /// <param name="data">The custom relic data to register</param>
        public static void RegisterCustomRelic(CollectableRelicData data)
        {
            CustomRelicData.Add(data.GetID(), data);
            SaveManager.GetAllGameData().GetAllCollectableRelicData().Add(data);
        }

        /// <summary>
        /// Get the custom relic data corresponding to the given ID
        /// </summary>
        /// <param name="relicID">ID of the custom relic to get</param>
        /// <returns>The custom relic data for the given ID</returns>
        public static CollectableRelicData GetRelicDataByID(string relicID)
        {
            if (CustomRelicData.ContainsKey(relicID))
            {
                return CustomRelicData[relicID];
            }
            return null;
        }
    }
}
