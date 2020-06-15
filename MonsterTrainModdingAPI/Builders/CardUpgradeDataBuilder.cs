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
        
        private string upgradeTitleKey { get; set; }
        private string upgradeDescriptionKey { get; set; }
        private string upgradeNotificationKey { get; set; }
        private Sprite upgradeIcon { get; set; }
        private bool hideUpgradeIconOnCard { get; set; }
        private bool useUpgradeHighlightTextTags { get; set; }
        private int bonusDamage { get; set; }
        private int bonusHP { get; set; }
        private int costReduction { get; set; }
        private int xCostReduction { get; set; }
        private int bonusHeal { get; set; }
        private int bonusSize { get; set; }
        private List<StatusEffectStackData> statusEffectUpgrades { get; set; }
        private List<CardTraitData> traitDataUpgrades { get; set; }
        private List<string> removeTraitUpgrades { get; set; }
        private List<CharacterTriggerData> triggerUpgrades { get; set; }
        private List<CardTriggerEffectData> cardTriggerUpgrades { get; set; }
        private List<RoomModifierData> roomModifierUpgrades { get; set; }
        private List<CardUpgradeMaskData> filters { get; set; }
        private List<CardUpgradeData> upgradesToRemove { get; set; }

        public CardUpgradeDataBuilder()
        {
            this.useUpgradeHighlightTextTags = true;

            this.triggerUpgrades = new List<CharacterTriggerData>();
            this.cardTriggerUpgrades = new List<CardTriggerEffectData>();
            this.roomModifierUpgrades = new List<RoomModifierData>();
            this.filters = new List<CardUpgradeMaskData>();
            this.upgradesToRemove = new List<CardUpgradeData>();
        }

        public CardUpgradeData Build()
        {
            CardUpgradeData cardUpgradeData = new CardUpgradeData();

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
