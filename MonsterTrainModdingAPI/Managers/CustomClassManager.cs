﻿using System.Collections.Generic;
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
    /// Handles registration and storage of custom class data.
    /// </summary>
    public class CustomClassManager
    {
        /// <summary>
        /// Maps custom class IDs to their respective ClassData.
        /// </summary>
        public static IDictionary<string, ClassData> CustomClassData { get; } = new Dictionary<string, ClassData>();
        /// <summary>
        /// Maps custom class IDs to their respective Class Frame Sprites. The list should be in the format Unit, Spell.
        /// </summary>
        public static IDictionary<string, List<Sprite>> CustomClassFrame { get; } = new Dictionary<string, List<Sprite>> ();
        /// <summary>
        /// Static reference to the game's SaveManager, which is necessary to register new classes.
        /// </summary>
        public static SaveManager SaveManager { get; set; }
        
        /// <summary>
        /// Register a custom class with the manager, allowing it to show up in game.
        /// </summary>
        /// <param name="classData">The custom class data to register</param>
        public static void RegisterCustomClass(ClassData classData)
        {
            CustomClassData.Add(classData.GetID(), classData);
            SaveManager.GetAllGameData().GetAllClassDatas().Add(classData);
            SaveManager.GetAllGameData().GetBalanceData().GetClassDatas().Add(classData);
        }

        /// <summary>
        /// Get the custom class data corresponding to the given ID.
        /// </summary>
        /// <param name="classID">ID of the custom class to get</param>
        /// <returns>The custom class data for the given ID</returns>
        public static ClassData GetClassDataByID(string classID)
        {
            if (CustomClassData.ContainsKey(classID))
            {
                return CustomClassData[classID];
            }
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
        /// Sets the Champion of your built Clan Data. Champion should be already Built and Registered!
        /// </summary>
        /// <param name="classData"></param>
        /// <param name="championCard"></param>
        /// <param name="champion"></param>
        public static void SetChampion(ClassData classData, CardData championCard, CharacterData champion)
        {
            var champ = classData.GetStartingChampionData();
            champ.cardData = championCard;

            CardUpgradeTreeData tree = classData.GetUpgradeTree();
            AccessTools.Field(typeof(CardUpgradeTreeData), "champion").SetValue(tree, champion);
            AccessTools.Field(typeof(ClassData), "upgradeTree").SetValue(classData, tree);
        }

        /// <summary>
        /// Sets the Starter Card of your built Clan Data. Starter should be already Built and Registered!
        /// Custom starter sets and configurations can be created manually.
        /// </summary>
        /// <param name="classData"></param>
        /// <param name="starterCard"></param>
        public static void SetStarter(ClassData classData, CardData starterCard)
        {
            ClassData c = classData;

            List<ClassData.StartingCardOptions> cardList = new List<ClassData.StartingCardOptions>
                {
                    new ClassData.StartingCardOptions
                    {
                        cards = new List<CardData>
                        {
                            starterCard,
                        }
                    },
                    new ClassData.StartingCardOptions
                    {
                        cards = new List<CardData>
                        {
                            starterCard,
                        }
                    },
                    new ClassData.StartingCardOptions
                    {
                        cards = new List<CardData>
                        {
                            starterCard,
                        }
                    },
                };

            AccessTools.Field(typeof(ClassData), "mainClassStartingCards").SetValue(c, cardList);
            AccessTools.Field(typeof(ClassData), "subclassStartingCards").SetValue(c, cardList);
        }
    }
}