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
    class CustomCardPoolManager
    {
        public static IDictionary<string, List<CardData>> CustomCardPoolData { get; } = new Dictionary<string, List<CardData>>();

        public static void AddCardToPools(CardData cardData, List<string> cardPoolIDs)
        {
            foreach (string cardPoolID in cardPoolIDs)
            {
                if (!CustomCardPoolData.ContainsKey(cardPoolID))
                {
                    CustomCardPoolData[cardPoolID] = new List<CardData>();
                }
                CustomCardPoolData[cardPoolID].Add(cardData);
            }
        }

        public static List<CardData> GetCardsForPool(string cardPoolID)
        {
            if (CustomCardPoolData.ContainsKey(cardPoolID))
            {
                return CustomCardPoolData[cardPoolID];
            }
            return new List<CardData>();
        }

        public static List<CardData> GetCardsForPoolSatisfyingConstraints(string cardPoolID, ClassData classData, CollectableRarity paramRarity, CardPoolHelper.RarityCondition rarityCondition, bool testRarityCondition)
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
    }
}
