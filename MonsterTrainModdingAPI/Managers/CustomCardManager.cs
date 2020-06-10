using System.Collections.Generic;
using System.IO;
using BepInEx.Logging;
using HarmonyLib;
using MonsterTrainModdingAPI.Builders;
using MonsterTrainModdingAPI.Utilities;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace MonsterTrainModdingAPI.Managers
{
    public class CustomCardManager
    {
        public static IDictionary<string, CardData> CustomCardData { get; } = new Dictionary<string, CardData>();
        public static IDictionary<string, AssetBundleLoadingInfo> CardBundleData { get; } = new Dictionary<string, AssetBundleLoadingInfo>();
        public static SaveManager SaveManager { get; set; }

        public static void RegisterCustomCard(CardData cardData, List<string> cardPoolData, AssetBundleLoadingInfo info = null)
        {
            if (info != null) CardBundleData.Add(cardData.GetID(), info);
            CustomCardData.Add(cardData.GetID(), cardData);
            CustomCardPoolManager.AddCardToPools(cardData, cardPoolData);
            SaveManager.GetAllGameData().GetAllCardData().Add(cardData);
        }

        public static CardData GetCardDataByID(string cardID)
        {
            if (CustomCardData.ContainsKey(cardID))
            {
                return CustomCardData[cardID];
            }
            return null;
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

        private static GameObject CreateCardGameObject(AssetReferenceGameObject assetRef, Sprite sprite)
        {
            // Create a new card GameObject from scratch
            // Cards are simple enough that we can get away with doing this
            GameObject cardGameObject = new GameObject();
            Image newImage = cardGameObject.AddComponent<Image>();
            newImage.sprite = sprite;

            // Tell the asset reference that the GameObject has already been loaded
            // This circumvents an issue where the game attempts to load the asset but fails
            AccessTools.Field(typeof(AssetReference), "m_LoadedAsset").SetValue(assetRef, cardGameObject);
            return cardGameObject;
        }

        public static GameObject CreateCardGameObject(string cardID)
        {
            CardData cardData = CustomCardData[cardID];


            // Get the path to the asset from the card's asset reference data
            var assetRef = (AssetReferenceGameObject)AccessTools.Field(typeof(CardData), "cardArtPrefabVariantRef").GetValue(cardData);

            if (CardBundleData.ContainsKey(cardID))
            {
                Texture2D tex = AssetBundleUtils.LoadAssetFromPath<Texture2D>(CardBundleData[cardID]);
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);
                return CreateCardGameObject(assetRef, sprite);
            }

            string assetPath = (string)AccessTools.Field(typeof(AssetReferenceGameObject), "m_AssetGUID").GetValue(assetRef);
            string cardPath = "BepInEx/plugins/" + assetPath;
            if (File.Exists(cardPath))
            {
                // Create the card sprite
                byte[] fileData = File.ReadAllBytes(cardPath);
                Texture2D tex = new Texture2D(1, 1);
                UnityEngine.ImageConversion.LoadImage(tex, fileData);
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);

                return CreateCardGameObject(assetRef, sprite);
            }
            return null;
        }
    }
}
