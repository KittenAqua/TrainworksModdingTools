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
        /// <summary>
        /// Maps card pool IDs to the CardData of cards which can appear in them.
        /// Cards which naturally appear in the pool in the base game will not appear in these lists.
        /// </summary>
        public static IDictionary<string, List<CardData>> CustomCardPoolData { get; } = new Dictionary<string, List<CardData>>();

        /// <summary>
        /// Add the card to the card pools with given IDs.
        /// </summary>
        /// <param name="cardData">CardData to be added to the pools</param>
        /// <param name="cardPoolIDs">List of card pool IDs to add the card to</param>
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

        /// <summary>
        /// Gets a list of all cards added to the given card pool by mods.
        /// Cards which naturally appear in the pool will not be returned.
        /// </summary>
        /// <param name="cardPoolID">ID of the card pool to get cards for</param>
        /// <returns>A list of cards added to the card pool with given ID by mods</returns>
        public static List<CardData> GetCardsForPool(string cardPoolID)
        {
            if (CustomCardPoolData.ContainsKey(cardPoolID))
            {
                return CustomCardPoolData[cardPoolID];
            }
            return new List<CardData>();
        }

        /// <summary>
        /// Gets a list of all cards added to the given card pool by mods
        /// which satisfy the constraints specified by the parameters passed in.
        /// Cards which naturally appear in the pool will not be returned.
        /// </summary>
        /// <param name="cardPoolID">ID of the card pool to get cards for</param>
        /// <param name="classData">Card must be part of this class</param>
        /// <param name="paramRarity">Rarity which is compared against the rarities of the cards in the pool using rarityCondition</param>
        /// <param name="rarityCondition">Rarity condition which takes into account paramRarity and the rarities of the cards in the pool</param>
        /// <param name="testRarityCondition">Whether or not the rarity condition should be checked</param>
        /// <returns>A list of cards added to the card pool with given ID by mods, all of which satisfy the given constraints.</returns>
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
