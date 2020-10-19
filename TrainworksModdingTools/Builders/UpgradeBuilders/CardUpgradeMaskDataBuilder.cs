using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Trainworks.Builders
{
    public class CardUpgradeMaskDataBuilder
    {
        public CardType CardType { get; set; } = CardType.Invalid;
        public List<string> RequiredSubtypes { get; set; } = new List<string>();
        public List<string> ExcludedSubtypes { get; set; } = new List<string>();
        public List<StatusEffectStackData> RequiredStatusEffects { get; set; } = new List<StatusEffectStackData>();
        public List<StatusEffectStackData> ExcludedStatusEffects { get; set; } = new List<StatusEffectStackData>();
        public List<string> RequiredCardTraits { get; set; } = new List<string>();
        public List<string> ExcludedCardTraits { get; set; } = new List<string>();
        public List<string> RequiredCardEffects { get; set; } = new List<string>();
        public List<string> ExcludedCardEffects { get; set; } = new List<string>();

        /// <summary>
        /// If there are any cards in this pool, then only the cards in this pool will be allowed
        /// </summary>
        public List<CardPool> AllowedCardPools { get; set; } = new List<CardPool>();

        /// <summary>
        /// No cards in this pool will be allowed
        /// </summary>
        public List<CardPool> DisallowedCardPools { get; set; } = new List<CardPool>();

        public List<int> RequiredSizes { get; set; } = new List<int>();
        public List<int> ExcludedSizes { get; set; } = new List<int>();

        public Vector2 CostRange { get; set; } = new Vector2(0.0f, 99f);

        public bool ExcludeNonAttackingMonsters { get; set; }

        /// <summary>
        /// Operator determines if we require all or at least one
        /// </summary>
        public CardUpgradeMaskDataBuilder.CompareOperator RequiredSubtypesOperator { get; set; }
        public CardUpgradeMaskDataBuilder.CompareOperator ExcludedSubtypesOperator { get; set; }
        public CardUpgradeMaskDataBuilder.CompareOperator RequiredStatusEffectsOperator { get; set; }
        public CardUpgradeMaskDataBuilder.CompareOperator ExcludedStatusEffectsOperator { get; set; }
        public CardUpgradeMaskDataBuilder.CompareOperator RequiredCardTraitsOperator { get; set; }
        public CardUpgradeMaskDataBuilder.CompareOperator ExcludedCardTraitsOperator { get; set; }
        public CardUpgradeMaskDataBuilder.CompareOperator RequiredCardEffectsOperator { get; set; }
        public CardUpgradeMaskDataBuilder.CompareOperator ExcludedCardEffectsOperator { get; set; }

        public bool RequireXCost { get; set; }
        public bool ExcludeXCost { get; set; }

        /// <summary>
        /// This is the reason why a card is filtered away from having this upgrade applied to it
        /// </summary>
        public CardState.UpgradeDisabledReason UpgradeDisabledReason { get; set; }

        /// <summary>
        /// Builds the RoomModifierData represented by this builders's parameters recursively;
        /// </summary>
        /// <returns>The newly created RoomModifierData</returns>
        public CardUpgradeMaskData Build()
        {
            CardUpgradeMaskData cardUpgradeMaskData = new CardUpgradeMaskData();

            Type realEnumType = AccessTools.Inner(typeof(CardUpgradeMaskData), "CompareOperator");

            AccessTools.Field(typeof(CardUpgradeMaskData), "allowedCardPools").SetValue(cardUpgradeMaskData, this.AllowedCardPools);
            AccessTools.Field(typeof(CardUpgradeMaskData), "cardType").SetValue(cardUpgradeMaskData, this.CardType);
            AccessTools.Field(typeof(CardUpgradeMaskData), "costRange").SetValue(cardUpgradeMaskData, this.CostRange);
            AccessTools.Field(typeof(CardUpgradeMaskData), "disallowedCardPools").SetValue(cardUpgradeMaskData, this.DisallowedCardPools);
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedCardEffects").SetValue(cardUpgradeMaskData, this.ExcludedCardEffects);
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedCardEffectsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.ExcludedCardEffectsOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedCardTraits").SetValue(cardUpgradeMaskData, this.ExcludedCardTraits);
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedCardTraitsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.ExcludedCardTraitsOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedSizes").SetValue(cardUpgradeMaskData, this.ExcludedSizes);
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedStatusEffects").SetValue(cardUpgradeMaskData, this.ExcludedStatusEffects);
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedStatusEffectsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.ExcludedStatusEffectsOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedSubtypes").SetValue(cardUpgradeMaskData, this.ExcludedSubtypes);
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludedSubtypesOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.ExcludedSubtypesOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludeNonAttackingMonsters").SetValue(cardUpgradeMaskData, this.ExcludeNonAttackingMonsters);
            AccessTools.Field(typeof(CardUpgradeMaskData), "excludeXCost").SetValue(cardUpgradeMaskData, this.ExcludeXCost);
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredCardEffects").SetValue(cardUpgradeMaskData, this.RequiredCardEffects);
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredCardEffectsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.RequiredCardEffectsOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredCardTraits").SetValue(cardUpgradeMaskData, this.RequiredCardTraits);
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredCardTraitsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.RequiredCardTraitsOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredSizes").SetValue(cardUpgradeMaskData, this.RequiredSizes);
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredStatusEffects").SetValue(cardUpgradeMaskData, this.RequiredStatusEffects);
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredStatusEffectsOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.RequiredStatusEffectsOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredSubtypes").SetValue(cardUpgradeMaskData, this.RequiredSubtypes);
            AccessTools.Field(typeof(CardUpgradeMaskData), "requiredSubtypesOperator").SetValue(cardUpgradeMaskData, Enum.ToObject(realEnumType, this.RequiredSubtypesOperator));
            AccessTools.Field(typeof(CardUpgradeMaskData), "requireXCost").SetValue(cardUpgradeMaskData, this.RequireXCost);
            AccessTools.Field(typeof(CardUpgradeMaskData), "upgradeDisabledReason").SetValue(cardUpgradeMaskData, this.UpgradeDisabledReason);
            return cardUpgradeMaskData;
        }

        public enum CompareOperator
        {
            And,
            Or,
        }
    }
}
