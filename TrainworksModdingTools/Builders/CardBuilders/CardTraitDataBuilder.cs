using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ShinyShoe;
using Trainworks.Managers;

namespace Trainworks.Builders
{
    public class CardTraitDataBuilder
    {
        /// <summary>
        /// Don't set directly; use TraitStateType instead.
        /// Type of the trait class to instantiate.
        /// </summary>
        public Type traitStateType;

        /// <summary>
        /// Type of the trait class to instantiate.
        /// Implicitly sets TraitStateName.
        /// </summary>
        public Type TraitStateType
        {
            get { return this.traitStateType; }
            set
            {
                this.traitStateType = value;
                this.TraitStateName = this.traitStateType.AssemblyQualifiedName;
            }
        }

        /// <summary>
        /// Name of the trait class to instantiate.
        /// </summary>
        public string TraitStateName { get; set; }

        /// <summary>
        /// CardData parameter; exact purpose depends on the trait type specified in TraitStateName.
        /// </summary>
        public CardData ParamCardData { get; set; }
        /// <summary>
        /// Type of card to target; exact purpose depends on the trait type specified in TraitStateName.
        /// </summary>
        public CardStatistics.CardTypeTarget ParamCardType { get; set; }
        /// <summary>
        /// Team to target; exact purpose depends on the trait type specified in TraitStateName.
        /// </summary>
        public Team.Type ParamTeamType { get; set; }

        /// <summary>
        /// Float parameter; exact purpose depends on the trait type specified in TraitStateName.
        /// </summary>
        public float ParamFloat { get; set; }
        /// <summary>
        /// Int parameter; exact purpose depends on the trait type specified in TraitStateName.
        /// </summary>
        public int ParamInt { get; set; }
        /// <summary>
        /// String parameter; exact purpose depends on the trait type specified in TraitStateName.
        /// </summary>
        public string ParamStr { get; set; }
        /// <summary>
        /// Subtype parameter; exact purpose depends on the trait type specified in TraitStateName.
        /// </summary>
        public string ParamSubtype { get; set; }

        /// <summary>
        /// Status effect array parameter; exact purpose depends on the trait type specified in TraitStateName.
        /// </summary>
        public StatusEffectStackData[] ParamStatusEffects { get; set; }

        public CardStatistics.EntryDuration ParamEntryDuration { get; set; }
        public CardStatistics.TrackedValueType ParamTrackedValue { get; set; }
        public bool ParamUseScalingParams { get; set; }

        public CardTraitDataBuilder()
        {
            this.ParamStatusEffects = new StatusEffectStackData[0];
        }

        /// <summary>
        /// Builds the CardTraitData represented by this builder's parameters recursively;
        /// all Builders represented in this class's various fields will also be built.
        /// </summary>
        /// <returns>The newly created CardTraitData</returns>
        public CardTraitData Build()
        {
            CardTraitData cardTraitData = new CardTraitData();
            AccessTools.Field(typeof(CardTraitData), "paramCardData").SetValue(cardTraitData, this.ParamCardData);
            AccessTools.Field(typeof(CardTraitData), "paramCardType").SetValue(cardTraitData, this.ParamCardType);
            AccessTools.Field(typeof(CardTraitData), "paramEntryDuration").SetValue(cardTraitData, this.ParamEntryDuration);
            AccessTools.Field(typeof(CardTraitData), "paramFloat").SetValue(cardTraitData, this.ParamFloat);
            AccessTools.Field(typeof(CardTraitData), "paramInt").SetValue(cardTraitData, this.ParamInt);
            AccessTools.Field(typeof(CardTraitData), "paramStatusEffects").SetValue(cardTraitData, this.ParamStatusEffects);
            AccessTools.Field(typeof(CardTraitData), "paramStr").SetValue(cardTraitData, this.ParamStr);
            AccessTools.Field(typeof(CardTraitData), "paramSubtype").SetValue(cardTraitData, this.ParamSubtype);
            AccessTools.Field(typeof(CardTraitData), "paramTeamType").SetValue(cardTraitData, this.ParamTeamType);
            AccessTools.Field(typeof(CardTraitData), "paramTrackedValue").SetValue(cardTraitData, this.ParamTrackedValue);
            AccessTools.Field(typeof(CardTraitData), "paramUseScalingParams").SetValue(cardTraitData, this.ParamUseScalingParams);
            AccessTools.Field(typeof(CardTraitData), "traitStateName").SetValue(cardTraitData, this.TraitStateName);
            return cardTraitData;
        }

        /// <summary>
        /// Add a status effect to this effect's status effect array.
        /// </summary>
        /// <param name="statusEffectID">ID of the status effect, most easily retrieved using the helper class "MTStatusEffectIDs"</param>
        /// <param name="stackCount">Number of stacks to apply</param>
        public void AddStatusEffect(string statusEffectID, int stackCount)
        {
            this.ParamStatusEffects = BuilderUtils.AddStatusEffect(statusEffectID, stackCount, this.ParamStatusEffects);
        }
    }
}
