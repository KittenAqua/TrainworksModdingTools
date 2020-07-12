using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace MonsterTrainModdingAPI.Builders
{
    public class CardUpgradeMaskDataBuilder
    {

        public CardType cardType = CardType.Invalid;
        public List<string> requiredSubtypes = new List<string>();
        public List<string> excludedSubtypes = new List<string>();
        public List<StatusEffectStackData> requiredStatusEffects = new List<StatusEffectStackData>();
        public List<StatusEffectStackData> excludedStatusEffects = new List<StatusEffectStackData>();
        public List<string> requiredCardTraits = new List<string>();
        public List<string> excludedCardTraits = new List<string>();
        public List<string> requiredCardEffects = new List<string>();
        public List<string> excludedCardEffects = new List<string>();

        /// <summary>
        /// If there are any cards in this pool, then only the cards in this pool will be allowed
        /// </summary>
        public List<CardPool> allowedCardPools = new List<CardPool>();

        /// <summary>
        /// No cards in this pool will be allowed
        /// </summary>
        public List<CardPool> disallowedCardPools = new List<CardPool>();

        public List<int> requiredSizes = new List<int>();
        public List<int> excludedSizes = new List<int>();

        public Vector2 costRange = new Vector2(0.0f, 99f);

        public bool excludeNonAttackingMonsters;

        /// <summary>
        /// Operator determines if we require all or at least one
        /// </summary>
        public CardUpgradeMaskDataBuilder.CompareOperator requiredSubtypesOperator;
        public CardUpgradeMaskDataBuilder.CompareOperator excludedSubtypesOperator;
        public CardUpgradeMaskDataBuilder.CompareOperator requiredStatusEffectsOperator;
        public CardUpgradeMaskDataBuilder.CompareOperator excludedStatusEffectsOperator;
        public CardUpgradeMaskDataBuilder.CompareOperator requiredCardTraitsOperator;
        public CardUpgradeMaskDataBuilder.CompareOperator excludedCardTraitsOperator;
        public CardUpgradeMaskDataBuilder.CompareOperator requiredCardEffectsOperator;
        public CardUpgradeMaskDataBuilder.CompareOperator excludedCardEffectsOperator;

        public bool requireXCost;
        public bool excludeXCost;

        /// <summary>
        /// This is the reason why a card is filtered away from having this upgrade applied to it
        /// </summary>
        public CardState.UpgradeDisabledReason upgradeDisabledReason;

        /// <summary>
        /// Builds the RoomModifierData represented by this builders's parameters recursively;
        /// </summary>
        /// <returns>The newly created RoomModifierData</returns>
        public CardUpgradeMaskData Build()
        {
            CardUpgradeMaskData cardUpgradeMaskData = ScriptableObject.CreateInstance<CardUpgradeMaskData>();
            AccessTools.Field(typeof(CardUpgradeMaskData), "cardType").SetValue(cardUpgradeMaskData, this.cardType);
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredSubtypes").SetValue(cardUpgradeMaskData, this.requiredSubtypes);
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedSubtypes").SetValue(cardUpgradeMaskData, this.excludedSubtypes);
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredStatusEffects").SetValue(cardUpgradeMaskData, this.requiredStatusEffects);
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedStatusEffects").SetValue(cardUpgradeMaskData, this.excludedStatusEffects);
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredCardTraits").SetValue(cardUpgradeMaskData, this.requiredCardTraits);
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedCardTraits").SetValue(cardUpgradeMaskData, this.excludedCardTraits);
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredCardEffects").SetValue(cardUpgradeMaskData, this.requiredCardEffects);
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedCardEffects").SetValue(cardUpgradeMaskData, this.excludedCardEffects);
            AccessTools.Field(typeof(CardUpgradeMaskData), "allowedCardPools").SetValue(cardUpgradeMaskData, this.allowedCardPools);
            AccessTools.Field(typeof(CardUpgradeMaskData), "disallowedCardPools").SetValue(cardUpgradeMaskData, this.disallowedCardPools);
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredSizes").SetValue(cardUpgradeMaskData, this.requiredSizes);
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedSizes").SetValue(cardUpgradeMaskData, this.excludedSizes);
            AccessTools.Field(typeof(CardUpgradeMaskData), "costRange").SetValue(cardUpgradeMaskData, this.costRange);
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludeNonAttackingMonsters").SetValue(cardUpgradeMaskData, this.excludeNonAttackingMonsters);

            Type realEnumType = AccessTools.Inner(typeof(CardUpgradeMaskData), "CompareOperator");
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredSubtypesOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.requiredSubtypesOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedSubtypesOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.excludedSubtypesOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredStatusEffectsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.requiredStatusEffectsOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedStatusEffectsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.excludedStatusEffectsOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredCardTraitsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.requiredCardTraitsOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedCardTraitsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType,this.excludedCardTraitsOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredCardEffectsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.requiredCardEffectsOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedCardEffectsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.excludedCardEffectsOperator));

            AccessTools.Field(typeof(CardUpgradeMaskData), "requireXCost").SetValue(cardUpgradeMaskData, this.requireXCost);
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludeXCost").SetValue(cardUpgradeMaskData, this.excludeXCost);
            AccessTools.Field(typeof(CardUpgradeMaskData), "upgradeDisabledReason").SetValue(cardUpgradeMaskData, this.upgradeDisabledReason);
            return cardUpgradeMaskData;
        }

        public enum CompareOperator
        {
            And,
            Or,
        }
    }
}
