using System.Collections.Generic;
using System.IO;
using BepInEx.Logging;
using HarmonyLib;
using MonsterTrainModdingAPI.Builders;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

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
        /// Maps custom card IDs to the information required to load their Assets.
        /// </summary>
        public static IDictionary<string, CustomAssetManager.AssetBundleLoadingInfo> CardBundleData { get; } = new Dictionary<string, CustomAssetManager.AssetBundleLoadingInfo>();
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
        /// <param name="info">The info used to load Art from AssetBundles</param>
        public static void RegisterCustomCard(CardData cardData, List<string> cardPoolData, CustomAssetManager.AssetBundleLoadingInfo info = null)
        {
            if (info != null)
            {
                CardBundleData.Add(cardData.GetID(), info);
            }
            CustomCardData.Add(cardData.GetID(), cardData);
            CustomCardPoolManager.AddCardToPools(cardData, cardPoolData);
            SaveManager.GetAllGameData().GetAllCardData().Add(cardData);
        }

        /// <summary>
        /// Get the custom card data corresponding to the given ID
        /// </summary>
        /// <param name="cardID">ID of the custom card to get</param>
        /// <returns>The custom card data for the given ID</returns>
        public static CardData GetCardDataByID(string cardID)
        {
            if (CustomCardData.ContainsKey(cardID))
            {
                return CustomCardData[cardID];
            }
            API.Log(LogLevel.All, "Couldn't find custom card: " + cardID + " - This will cause crashes.");
            
            return null;
        }

        /// <summary>
        /// Get the player's current primary clan.
        /// </summary>
        /// <returns>ClassData of the player's primary clan</returns>
        public static ClassData CurrentPrimaryClan()
        {
            var saveData = (SaveData)AccessTools.Property(typeof(SaveManager), "ActiveSaveData").GetValue(SaveManager);
            ClassData mainClass = SaveManager.GetAllGameData().FindClassData(saveData.GetStartingConditions().Class);
            return mainClass;
        }

        /// <summary>
        /// Get the player's current allied clan.
        /// </summary>
        /// <returns>ClassData of the player's allied clan</returns>
        public static ClassData CurrentAlliedClan()
        {
            var saveData = (SaveData)AccessTools.Property(typeof(SaveManager), "ActiveSaveData").GetValue(SaveManager);
            ClassData mainClass = SaveManager.GetAllGameData().FindClassData(saveData.GetStartingConditions().Class);
            return mainClass;
        }

        /// <summary>
        /// Create a GameObject for the custom card with the AssetReference and Sprite
        /// </summary>
        /// <param name="assetRef">Reference to inform of loading</param>
        /// <param name="sprite">Sprite to create card with</param>
        /// <param name="cardID">ID of the card to create the sprite for</param>
        /// <returns></returns>
        private static GameObject CreateCardGameObject(AssetReferenceGameObject assetRef, Sprite sprite, string cardID)
        {
            // Create a new card GameObject from scratch
            // Cards are simple enough that we can get away with doing this
            GameObject cardGameObject = new GameObject();
            cardGameObject.name = cardID;
            Image newImage = cardGameObject.AddComponent<Image>();
            newImage.sprite = sprite;

            // Tell the asset reference that the GameObject has already been loaded
            // This circumvents an issue where the game attempts to load the asset but fails
            AccessTools.Field(typeof(AssetReference), "m_LoadedAsset").SetValue(assetRef, cardGameObject);
            return cardGameObject;
        }

        /// <summary>
        /// Create a GameObject for the custom card with the given ID.
        /// Used for loading custom card art.
        /// </summary>
        /// <param name="cardID">ID of the custom card to create the GameObject for</param>
        /// <returns>The GameObject for the custom card with given ID</returns>
        public static GameObject CreateCardGameObject(string cardID)
        {
            CardData cardData = CustomCardData[cardID];


            // Get the path to the asset from the card's asset reference data
            var assetRef = (AssetReferenceGameObject)AccessTools.Field(typeof(CardData), "cardArtPrefabVariantRef").GetValue(cardData);
            Sprite sprite = null;
            if (CardBundleData.ContainsKey(cardID))
            {
                Texture2D tex = CustomAssetManager.LoadAssetFromBundle<Texture2D>(CardBundleData[cardID]);
                sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);
                
            }
            else
            {
                // Get the path to the asset from the card's asset reference data
                string assetPath = (string)AccessTools.Field(typeof(AssetReferenceGameObject), "m_AssetGUID").GetValue(assetRef);
                sprite = CustomAssetManager.LoadSpriteFromPath(assetPath);
            }
            if (sprite != null)
            {
                return CreateCardGameObject(assetRef, sprite, cardID);
            }
            else
            {
                return null;
            }
        }
    }
}
