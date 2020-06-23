using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using MonsterTrainModdingAPI.Enums.MTCardPools;
using MonsterTrainModdingAPI.Enums.MTClans;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ShinyShoe;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI.Builders
{
    public class CardUpgradeDataBuilder
    {    
        // Note: add status effect adder, it's different than the one in BuilderUtils
        
        public string upgradeTitleKey { get; set; }
        public string upgradeDescriptionKey { get; set; }
        public string upgradeNotificationKey { get; set; }
        public Sprite upgradeIcon { get; set; }
        public bool hideUpgradeIconOnCard { get; set; }
        public bool useUpgradeHighlightTextTags { get; set; }
        public int bonusDamage { get; set; }
        public int bonusHP { get; set; }
        public int costReduction { get; set; }
        public int xCostReduction { get; set; }
        public int bonusHeal { get; set; }
        public int bonusSize { get; set; }


        public List<CardTraitDataBuilder> traitDataUpgradeBuilders { get; set; }
        public List<CharacterTriggerDataBuilder> triggerUpgradeBuilders { get; set; }
        public List<CardTriggerEffectDataBuilder> cardTriggerUpgradeBuilders { get; set; }
        public List<RoomModifierDataBuilder> roomModifierUpgradeBuilders { get; set; }
        public List<CardUpgradeMaskDataBuilder> filtersBuilders { get; set; }
        public List<CardUpgradeDataBuilder> upgradesToRemoveBuilders { get; set; }

        /// <summary>
        /// To add a status effect, no need for a builder. new StatusEffectStackData with properties statusId (string) and count (int) are sufficient.
        /// Get the string with -> statusEffectID = MTStatusEffectIDs.GetIDForType(statusEffectType);
        /// </summary>
        public List<StatusEffectStackData> statusEffectUpgrades { get; set; }
        public List<CardTraitData> traitDataUpgrades { get; set; }
        public List<string> removeTraitUpgrades { get; set; }
        public List<CharacterTriggerData> triggerUpgrades { get; set; }
        public List<CardTriggerEffectData> cardTriggerUpgrades { get; set; }
        public List<RoomModifierData> roomModifierUpgrades { get; set; }
        public List<CardUpgradeMaskData> filters { get; set; }
        public List<CardUpgradeData> upgradesToRemove { get; set; }

        public CardUpgradeDataBuilder()
        {
            this.upgradeNotificationKey = "EmptyString-0000000000000000-00000000000000000000000000000000-v2";
            this.useUpgradeHighlightTextTags = true;

            this.traitDataUpgradeBuilders = new List<CardTraitDataBuilder>();
            this.triggerUpgradeBuilders = new List<CharacterTriggerDataBuilder>();
            this.cardTriggerUpgradeBuilders = new List<CardTriggerEffectDataBuilder>();
            this.roomModifierUpgradeBuilders = new List<RoomModifierDataBuilder>();
            this.filtersBuilders = new List<CardUpgradeMaskDataBuilder>();
            this.upgradesToRemoveBuilders = new List<CardUpgradeDataBuilder>();

            this.statusEffectUpgrades = new List<StatusEffectStackData>();
            this.traitDataUpgrades = new List<CardTraitData>();
            this.removeTraitUpgrades = new List<string>();
            this.triggerUpgrades = new List<CharacterTriggerData>();
            this.cardTriggerUpgrades = new List<CardTriggerEffectData>();
            this.roomModifierUpgrades = new List<RoomModifierData>();
            this.filters = new List<CardUpgradeMaskData>();
            this.upgradesToRemove = new List<CardUpgradeData>();
        }

        public CardUpgradeData Build()
        {
            CardUpgradeData cardUpgradeData = new CardUpgradeData();

            foreach (var builder in this.traitDataUpgradeBuilders)
            {
                this.traitDataUpgrades.Add(builder.Build());
            }
            foreach (var builder in this.triggerUpgradeBuilders)
            {
                this.triggerUpgrades.Add(builder.Build());
            }
            foreach (var builder in this.cardTriggerUpgradeBuilders)
            {
                this.cardTriggerUpgrades.Add(builder.Build());
            }
            foreach (var builder in this.roomModifierUpgradeBuilders)
            {
                this.roomModifierUpgrades.Add(builder.Build());
            }
            foreach (var builder in this.filtersBuilders)
            {
                this.filters.Add(builder.Build());
            }
            foreach (var builder in this.upgradesToRemoveBuilders)
            {
                this.upgradesToRemove.Add(builder.Build());
            }

            AccessTools.Field(typeof(CardUpgradeData), "upgradeTitleKey").SetValue(cardUpgradeData, this.upgradeTitleKey);
            AccessTools.Field(typeof(CardUpgradeData), "upgradeDescriptionKey").SetValue(cardUpgradeData, this.upgradeDescriptionKey);
            AccessTools.Field(typeof(CardUpgradeData), "upgradeNotificationKey").SetValue(cardUpgradeData, this.upgradeNotificationKey);
            AccessTools.Field(typeof(CardUpgradeData), "upgradeIcon").SetValue(cardUpgradeData, this.upgradeIcon);
            AccessTools.Field(typeof(CardUpgradeData), "hideUpgradeIconOnCard").SetValue(cardUpgradeData, this.hideUpgradeIconOnCard);
            AccessTools.Field(typeof(CardUpgradeData), "useUpgradeHighlightTextTags").SetValue(cardUpgradeData, this.useUpgradeHighlightTextTags);
            AccessTools.Field(typeof(CardUpgradeData), "bonusDamage").SetValue(cardUpgradeData, this.bonusDamage);
            AccessTools.Field(typeof(CardUpgradeData), "bonusHP").SetValue(cardUpgradeData, this.bonusHP);
            AccessTools.Field(typeof(CardUpgradeData), "costReduction").SetValue(cardUpgradeData, this.costReduction);
            AccessTools.Field(typeof(CardUpgradeData), "xCostReduction").SetValue(cardUpgradeData, this.xCostReduction);
            AccessTools.Field(typeof(CardUpgradeData), "bonusHeal").SetValue(cardUpgradeData, this.bonusHeal);
            AccessTools.Field(typeof(CardUpgradeData), "bonusSize").SetValue(cardUpgradeData, this.bonusSize);
            AccessTools.Field(typeof(CardUpgradeData), "statusEffectUpgrades").SetValue(cardUpgradeData, this.statusEffectUpgrades);
            AccessTools.Field(typeof(CardUpgradeData), "traitDataUpgrades").SetValue(cardUpgradeData, this.traitDataUpgrades);
            AccessTools.Field(typeof(CardUpgradeData), "removeTraitUpgrades").SetValue(cardUpgradeData, this.removeTraitUpgrades);
            AccessTools.Field(typeof(CardUpgradeData), "triggerUpgrades").SetValue(cardUpgradeData, this.triggerUpgrades);
            AccessTools.Field(typeof(CardUpgradeData), "cardTriggerUpgrades").SetValue(cardUpgradeData, this.cardTriggerUpgrades);
            AccessTools.Field(typeof(CardUpgradeData), "roomModifierUpgrades").SetValue(cardUpgradeData, this.roomModifierUpgrades);
            AccessTools.Field(typeof(CardUpgradeData), "filters").SetValue(cardUpgradeData, this.filters);
            AccessTools.Field(typeof(CardUpgradeData), "upgradesToRemove").SetValue(cardUpgradeData, this.upgradesToRemove);

            return cardUpgradeData;
        }
    }
}
