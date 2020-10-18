using System.Collections.Generic;
using System.IO;
using BepInEx.Logging;
using HarmonyLib;
using MonsterTrainModdingAPI.Builders;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using MonsterTrainModdingAPI.Utilities;

namespace MonsterTrainModdingAPI.Managers
{
    /// <summary>
    /// Handles registration and storage of custom card data.
    /// </summary>
    public class CustomCardManager
    {
        /// <summary>
        /// Maps custom card IDs to their respective CardData.
        /// </summary>
        public static IDictionary<string, CardData> CustomCardData { get; } = new Dictionary<string, CardData>();
        /// <summary>
        /// Static reference to the game's SaveManager, which is necessary to register new cards.
        /// </summary>
        public static SaveManager SaveManager { get; set; }

        /// <summary>
        /// Register a custom card with the manager, allowing it to show up in game
        /// both in the logbook and whenever cards are chosen from the specified pools.
        /// </summary>
        /// <param name="cardData">The custom card data to register</param>
        /// <param name="cardPoolData">The card pools the custom card should be a part of</param>
        public static void RegisterCustomCard(CardData cardData, List<string> cardPoolData)
        {
            if (!CustomCardData.ContainsKey(cardData.GetID()))
            {
                CustomCardData.Add(cardData.GetID(), cardData);
                CustomCardPoolManager.AddCardToPools(cardData, cardPoolData);
                SaveManager.GetAllGameData().GetAllCardData().Add(cardData);
            }
            else
            {
                API.Log(LogLevel.Warning, "Attempted to register duplicate card data with name: " + cardData.name);
            }
        }

        /// <summary>
        /// Get the custom card data corresponding to the given ID
        /// </summary>
        /// <param name="cardID">ID of the custom card to get</param>
        /// <returns>The custom card data for the given ID</returns>
        public static CardData GetCardDataByID(string cardID)
        {
            var guid = GUIDGenerator.GenerateDeterministicGUID(cardID);
            if (CustomCardData.ContainsKey(guid))
            {
                return CustomCardData[guid];
            }
            API.Log(LogLevel.All, "Couldn't find custom card: " + cardID + " - This will cause crashes.");
            
            return null;
        }
    }
}
