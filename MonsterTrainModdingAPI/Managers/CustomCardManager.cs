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
    public class CustomCardManager
    {
        public static IDictionary<string, CardData> CustomCardData { get; } = new Dictionary<string, CardData>();
        public static IDictionary<string, List<int>> CustomCardPoolData { get; } = new Dictionary<string, List<int>>();
        private static List<string> PreregisteredCardIDs { get; set; } = new List<string>();
        public static SaveManager SaveManager { get; set; }
        
        public static void RegisterCustomCard(CardData cardData, List<int> cardPoolData)
        {
            CustomCardData.Add(cardData.GetID(), cardData);
            CustomCardPoolData.Add(cardData.GetID(), cardPoolData);
            if (SaveManager == null)
            {
                PreregisteredCardIDs.Add(cardData.GetID());
            }
            else
            {
                SaveManager.GetAllGameData().GetAllCardData().Add(cardData);
            }
        }

        public static void FinishCustomCardRegistration()
        {
            foreach (string cardID in PreregisteredCardIDs)
            {
                SaveManager.GetAllGameData().GetAllCardData().Add(CustomCardData[cardID]);
            }
            PreregisteredCardIDs.Clear();
        }

        public static CardData GetCardDataByID(string cardID)
        {
            if (CustomCardData.ContainsKey(cardID))
            {
                return CustomCardData[cardID];
            }
            return null;
        }

        public static List<CardData> GetCardsForPool(int cardPoolID)
        {
            var validCards = new List<CardData>();
            foreach (KeyValuePair<string, CardData> entry in CustomCardData)
            {
                foreach (int customPoolID in CustomCardPoolData[entry.Key])
                {
                    if (customPoolID == cardPoolID)
                    {
                        validCards.Add(entry.Value);
                        break;
                    }
                }
            }
            return validCards;
        }

        public static List<CardData> GetCardsForPoolSatisfyingConstraints(int cardPoolID, ClassData classData, CollectableRarity paramRarity, CardPoolHelper.RarityCondition rarityCondition, bool testRarityCondition)
        {
            var allValidCards = GetCardsForPool(cardPoolID);
            var validCards = new List<CardData>();
            foreach (CardData cardData in allValidCards)
            {
                if (cardData.GetLinkedClass() == classData && (!testRarityCondition || rarityCondition(paramRarity, cardData.GetRarity())))
                {
                    validCards.Add(cardData);
                }
            }
            return validCards;
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

        public static GameObject CreateCardGameObject(string cardID)
        {
            CardData cardData = CustomCardData[cardID];

            // Get the path to the asset from the card's asset reference data
            var assetRef = (AssetReferenceGameObject)AccessTools.Field(typeof(CardData), "cardArtPrefabVariantRef").GetValue(cardData);
            string assetPath = (string)AccessTools.Field(typeof(AssetReferenceGameObject), "m_AssetGUID").GetValue(assetRef);
            string cardPath = "BepInEx/plugins/" + assetPath;
            if (File.Exists(cardPath))
            {
                // Create the card sprite
                byte[] fileData = File.ReadAllBytes(cardPath);
                Texture2D tex = new Texture2D(1, 1);
                UnityEngine.ImageConversion.LoadImage(tex, fileData);
                Sprite sprite = Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), new Vector2(0.5f, 0.5f), 128f);

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
            return null;
        }
    }
}
