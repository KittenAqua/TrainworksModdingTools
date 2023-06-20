using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using BepInEx.Logging;
using HarmonyLib;
using Trainworks.Builders;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Trainworks.Managers
{
    public class CustomCardPoolManager
    {
        /// <summary>
        /// Maps card pool IDs to the CardData of cards which can appear in them.
        /// Cards which naturally appear in the pool in the base game will not appear in these lists.
        /// </summary>
        public static IDictionary<string, List<CardData>> CustomCardPoolData { get; } = new Dictionary<string, List<CardData>>();

        /// <summary>
        /// Maps custom card pool IDs to their actual CardPool instances.
        /// </summary>
        public static IDictionary<string, CardPool> CustomCardPools { get; } = new Dictionary<string, CardPool>();

        public static void RegisterCustomCardPool(CardPool cardPool)
        {
            if (!CustomCardPools.ContainsKey(cardPool.name))
            {
                CustomCardPools.Add(cardPool.name, cardPool);
            }
            else
            {
                Trainworks.Log(LogLevel.Warning, "Attempted to register duplicate card pool with name: " + cardPool.name);
            }
        }

        /// <summary>
        /// Add the card to the card pools with given IDs.
        /// </summary>
        /// <param name="cardData">CardData to be added to the pools</param>
        /// <param name="cardPoolIDs">List of card pool IDs to add the card to</param>
        public static void AddCardToPools(CardData cardData, List<string> cardPoolIDs)
        {
            foreach (string cardPoolID in cardPoolIDs)
            {
                if (CustomCardPools.ContainsKey(cardPoolID))
                {
                    var pool = CustomCardPools[cardPoolID];
                    var cardDataList = (Malee.ReorderableArray<CardData>)AccessTools.Field(typeof(CardPool), "cardDataList").GetValue(pool);
                    cardDataList.Add(cardData);
                }
                else
                {
                    if (!CustomCardPoolData.ContainsKey(cardPoolID))
                    {
                        CustomCardPoolData[cardPoolID] = new List<CardData>();
                    }
                    CustomCardPoolData[cardPoolID].Add(cardData);
                }
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

            //if (rarityCondition == null)
            //{    
                //testRarityCondition = false;
            //}
            rarityCondition = (rarityCondition ?? EqualRarity);

            foreach (CardData cardData in allValidCards)
            {
                if (cardData.GetLinkedClass() == classData && (!testRarityCondition || rarityCondition(paramRarity, cardData.GetRarity())))
                {
                    validCards.Add(cardData);
                }
            }

            return validCards;
        }

        private static CardPoolHelper.RarityCondition EqualRarity = (CollectableRarity paramRarity, CollectableRarity cardRarity) => paramRarity == cardRarity;

        /// <summary>
        /// Gets a list of all cards added to the given card pool by mods
        /// which satisfy the constraints specified by the mask data passed in.
        /// Cards which naturally appear in the pool will not be returned.
        /// </summary>
        /// <param name="cardPoolID">ID of the card pool to get cards for</param>
        /// <param name="paramCardFilter">Constraints to satisfy</param>
        /// <param name="relicManager">a RelicManager</param>
        /// <returns>A list of cards added to the card pool with given ID by mods, all of which satisfy the given constraints.</returns>
        public static List<CardData> GetCardsForPoolSatisfyingConstraints(string cardPoolID, CardUpgradeMaskData paramCardFilter, RelicManager relicManager)
        {
            var allValidCards = GetCardsForPool(cardPoolID);
            var validCards = new List<CardData>();
            foreach (CardData cardData in allValidCards)
            {
                if (paramCardFilter == null)
                {
                    validCards.Add(cardData);
                }
                else if (paramCardFilter.FilterCard<CardData>(cardData, relicManager))
                {
                    validCards.Add(cardData);
                }
            }
            return validCards;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cardPoolID"></param>
        /// <returns></returns>
        public static CardPool GetCustomCardPoolByID(string cardPoolID)
        {
            if (CustomCardPools.ContainsKey(cardPoolID))
            {
                return CustomCardPools[cardPoolID];
            }
            Trainworks.Log(LogLevel.Warning, "Could not find card pool: " + cardPoolID);
            return null;
        }
    }
}
