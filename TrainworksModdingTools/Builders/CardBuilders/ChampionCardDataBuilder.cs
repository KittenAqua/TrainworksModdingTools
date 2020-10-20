using BepInEx.Logging;
using HarmonyLib;
using Trainworks.Managers;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Trainworks.Builders
{
    public class ChampionCardDataBuilder : CardDataBuilder
    {
        public CharacterDataBuilder Champion { get; set; }
        public CardData StarterCardData { get; set; }
        public CardUpgradeTreeDataBuilder UpgradeTree { get; set; }
        public String ChampionIconPath { get; set; }
        public String ChampionSelectedCue { get; set; }

        public ChampionCardDataBuilder()
        {
            Rarity = CollectableRarity.Champion;

            EffectBuilders.Add(new CardEffectDataBuilder
            {
                EffectStateName = "CardEffectSpawnMonster",
                TargetMode = TargetMode.DropTargetCharacter,
            });

            PluginManager.PluginGUIDToPath.TryGetValue(PluginManager.AssemblyNameToPluginGUID[Assembly.GetCallingAssembly().FullName], out string basePath);
            this.BaseAssetPath = basePath;

            CardType = CardType.Monster;
            TargetsRoom = true;
        }

        /// <summary>
        /// Builds the CardData represented by this builder's parameters recursively
        /// and registers it and its components with the appropriate managers.
        /// </summary>
        /// <returns>The newly registered CardData</returns>
        public CardData BuildAndRegister(int ChampionIndex = 0)
        {
            var cardData = this.Build();
            Trainworks.Log(LogLevel.Debug, "Adding custom card: " + cardData.GetName());
            CustomCardManager.RegisterCustomCard(cardData, this.CardPoolIDs);

            var Clan = cardData.GetLinkedClass();

            ChampionData ClanChamp = Clan.GetChampionData(ChampionIndex);
            ClanChamp.championCardData = cardData;
            if (this.ChampionIconPath != null)
            {
                Sprite championIconSprite = CustomAssetManager.LoadSpriteFromPath(this.BaseAssetPath + "/" + this.ChampionIconPath);
                ClanChamp.championIcon = championIconSprite;
            }
            ClanChamp.starterCardData = StarterCardData;
            if (this.UpgradeTree != null)
            {
                ClanChamp.upgradeTree = UpgradeTree.Build();
            }
            ClanChamp.championSelectedCue = ChampionSelectedCue;

            return cardData;
        }

        public new CardData BuildAndRegister()
        {
            return BuildAndRegister(0);
        }

        /// <summary>
        /// Builds the CardData represented by this builder's parameters recursively;
        /// i.e. all CardEffectBuilders in EffectBuilders will also be built.
        /// </summary>
        /// <returns>The newly created CardData</returns>
        public new CardData Build()
        {
            Champion.SubtypeKeys.Add("SubtypesData_Champion_83f21cbe-9d9b-4566-a2c3-ca559ab8ff34");
            EffectBuilders[0].ParamCharacterDataBuilder = Champion;

            CardData cardData = base.Build();

            return cardData;
        }
    }
}
