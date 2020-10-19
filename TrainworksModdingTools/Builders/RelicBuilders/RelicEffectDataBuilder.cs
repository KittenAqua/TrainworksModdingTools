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
    public class RelicEffectDataBuilder
    {
        /// <summary>
        /// Don't set directly; use RelicEffectClassType instead.
        /// Type of the relic effect class to instantiate.
        /// </summary>
        public Type relicEffectClassType;

        /// <summary>
        /// Type of the relic effect class to instantiate.
        /// Implicitly sets RelicEffectClassName.
        /// </summary>
        public Type RelicEffectClassType
        {
            get { return this.relicEffectClassType; }
            set
            {
                this.relicEffectClassType = value;
                this.RelicEffectClassName = this.relicEffectClassType.AssemblyQualifiedName;
            }
        }

        /// <summary>
        /// Don't set directly; use RelicEffectClassName instead.
        /// </summary>
        public string relicEffectClassName;

        /// <summary>
        /// Implicitly sets TooltipBodyKey and TooltipTitleKey if null.
        /// </summary>
        public string RelicEffectClassName
        {
            get { return this.relicEffectClassName; }
            set
            {
                this.relicEffectClassName = value;
                if (this.TooltipBodyKey == null)
                {
                    this.TooltipBodyKey = this.relicEffectClassName + "_RelicEffectData_TooltipBodyKey";
                }
                if (this.TooltipTitleKey == null)
                {
                    this.TooltipTitleKey = this.relicEffectClassName + "_RelicEffectData_TooltipTitleKey";
                }
            }
        }

        public List<RelicEffectConditionBuilder> EffectConditionBuilders { get; set; }
        public List<CardTraitDataBuilder> TraitBuilders { get; set; }
        public List<CharacterTriggerDataBuilder> TriggerBuilders { get; set; }
        public List<RelicEffectCondition> EffectConditions { get; set; }
        public List<CardTraitData> Traits { get; set; }
        public List<CharacterTriggerData> Triggers { get; set; }

        public bool ParamBool { get; set; }
        public List<CardEffectData> ParamCardEffects { get; set; }
        public List<CardEffectDataBuilder> ParamCardEffectBuilders { get; set; }
        public CardUpgradeMaskData ParamCardFilter { get; set; }
        public CardPool ParamCardPool { get; set; }
        public CardSetBuilder ParamCardSetBuilder { get; set; }
        public CardType ParamCardType { get; set; }
        public CardUpgradeData ParamCardUpgradeData { get; set; }
        public List<CharacterData> ParamCharacters { get; set; }
        public string ParamCharacterSubtype { get; set; }
        public string[] ParamExcludeCharacterSubtypes { get; set; }
        public float ParamFloat { get; set; }
        public int ParamInt { get; set; }
        public int ParamMaxInt { get; set; }
        public int ParamMinInt { get; set; }
        public bool ParamUseIntRange { get; set; }
        public CollectableRelicData ParamRelic { get; set; }
        public RewardData ParamReward { get; set; }
        public RoomData ParamRoomData { get; set; }
        public Team.Type ParamSourceTeam { get; set; }
        public SpecialCharacterType ParamSpecialCharacterType { get; set; }
        public StatusEffectStackData[] ParamStatusEffects { get; set; }
        public string ParamString { get; set; }
        public TargetMode ParamTargetMode { get; set; }
        public CharacterTriggerData.Trigger ParamTrigger { get; set; }

        public string SourceCardTraitParam { get; set; }
        public string TargetCardTraitParam { get; set; }
        public List<CardTraitData> ExcludedTraits { get; set; }

        public string TooltipBody { get; set; }
        public string TooltipTitle { get; set; }
        public string TooltipBodyKey { get; set; }
        public string TooltipTitleKey { get; set; }
        public bool TriggerTooltipsSuppressed { get; set; }
        public AdditionalTooltipData[] AdditionalTooltips { get; set; }

        public VfxAtLoc AppliedVfx { get; set; }

        public RelicEffectDataBuilder()
        {
            this.EffectConditions = new List<RelicEffectCondition>();
            this.Traits = new List<CardTraitData>();
            this.Triggers = new List<CharacterTriggerData>();
            this.ParamCardEffects = new List<CardEffectData>();
            this.ParamCharacters = new List<CharacterData>();
            this.ParamExcludeCharacterSubtypes = new string[0];
            this.ExcludedTraits = new List<CardTraitData>();
            this.ParamStatusEffects = new StatusEffectStackData[0];
            this.AdditionalTooltips = new AdditionalTooltipData[0];
            this.EffectConditionBuilders = new List<RelicEffectConditionBuilder>();
            this.TraitBuilders = new List<CardTraitDataBuilder>();
            this.TriggerBuilders = new List<CharacterTriggerDataBuilder>();
            this.ParamCardEffectBuilders = new List<CardEffectDataBuilder>();
        }

        /// <summary>
        /// Builds the RelicEffectData represented by this builder's parameters recursively;
        /// all Builders represented in this class's various fields will also be built.
        /// </summary>
        /// <returns>The newly created RelicEffectData</returns>
        public RelicEffectData Build()
        {
            foreach (var builder in this.EffectConditionBuilders)
            {
                this.EffectConditions.Add(builder.Build());
            }
            foreach (var builder in this.TraitBuilders)
            {
                this.Traits.Add(builder.Build());
            }
            foreach (var builder in this.TriggerBuilders)
            {
                this.Triggers.Add(builder.Build());
            }
            foreach(var builder in this.ParamCardEffectBuilders)
            {
                this.ParamCardEffects.Add(builder.Build());
            }

            RelicEffectData relicEffectData = new RelicEffectData();
            AccessTools.Field(typeof(RelicEffectData), "additionalTooltips").SetValue(relicEffectData, this.AdditionalTooltips);
            AccessTools.Field(typeof(RelicEffectData), "appliedVfx").SetValue(relicEffectData, this.AppliedVfx);
            AccessTools.Field(typeof(RelicEffectData), "effectConditions").SetValue(relicEffectData, this.EffectConditions);
            AccessTools.Field(typeof(RelicEffectData), "excludedTraits").SetValue(relicEffectData, this.ExcludedTraits);
            AccessTools.Field(typeof(RelicEffectData), "paramBool").SetValue(relicEffectData, this.ParamBool);
            AccessTools.Field(typeof(RelicEffectData), "paramCardEffects").SetValue(relicEffectData, this.ParamCardEffects);
            AccessTools.Field(typeof(RelicEffectData), "paramCardFilter").SetValue(relicEffectData, this.ParamCardFilter);
            AccessTools.Field(typeof(RelicEffectData), "paramCardPool").SetValue(relicEffectData, this.ParamCardPool);
            AccessTools.Field(typeof(RelicEffectData), "paramCardSetBuilder").SetValue(relicEffectData, this.ParamCardSetBuilder);
            AccessTools.Field(typeof(RelicEffectData), "paramCardType").SetValue(relicEffectData, this.ParamCardType);
            AccessTools.Field(typeof(RelicEffectData), "paramCardUpgradeData").SetValue(relicEffectData, this.ParamCardUpgradeData);
            AccessTools.Field(typeof(RelicEffectData), "paramCharacters").SetValue(relicEffectData, this.ParamCharacters);
            AccessTools.Field(typeof(RelicEffectData), "paramCharacterSubtype").SetValue(relicEffectData, this.ParamCharacterSubtype);
            AccessTools.Field(typeof(RelicEffectData), "paramExcludeCharacterSubtypes").SetValue(relicEffectData, this.ParamExcludeCharacterSubtypes);
            AccessTools.Field(typeof(RelicEffectData), "paramFloat").SetValue(relicEffectData, this.ParamFloat);
            AccessTools.Field(typeof(RelicEffectData), "paramInt").SetValue(relicEffectData, this.ParamInt);
            AccessTools.Field(typeof(RelicEffectData), "paramMaxInt").SetValue(relicEffectData, this.ParamMaxInt);
            AccessTools.Field(typeof(RelicEffectData), "paramMinInt").SetValue(relicEffectData, this.ParamMinInt);
            AccessTools.Field(typeof(RelicEffectData), "paramRelic").SetValue(relicEffectData, this.ParamRelic);
            AccessTools.Field(typeof(RelicEffectData), "paramReward").SetValue(relicEffectData, this.ParamReward);
            AccessTools.Field(typeof(RelicEffectData), "paramRoomData").SetValue(relicEffectData, this.ParamRoomData);
            AccessTools.Field(typeof(RelicEffectData), "paramSourceTeam").SetValue(relicEffectData, this.ParamSourceTeam);
            AccessTools.Field(typeof(RelicEffectData), "paramSpecialCharacterType").SetValue(relicEffectData, this.ParamSpecialCharacterType);
            AccessTools.Field(typeof(RelicEffectData), "paramStatusEffects").SetValue(relicEffectData, this.ParamStatusEffects);
            AccessTools.Field(typeof(RelicEffectData), "paramString").SetValue(relicEffectData, this.ParamString);
            AccessTools.Field(typeof(RelicEffectData), "paramTargetMode").SetValue(relicEffectData, this.ParamTargetMode);
            AccessTools.Field(typeof(RelicEffectData), "paramTrigger").SetValue(relicEffectData, this.ParamTrigger);
            AccessTools.Field(typeof(RelicEffectData), "paramUseIntRange").SetValue(relicEffectData, this.ParamUseIntRange);
            AccessTools.Field(typeof(RelicEffectData), "relicEffectClassName").SetValue(relicEffectData, this.RelicEffectClassName);
            AccessTools.Field(typeof(RelicEffectData), "sourceCardTraitParam").SetValue(relicEffectData, this.SourceCardTraitParam);
            AccessTools.Field(typeof(RelicEffectData), "targetCardTraitParam").SetValue(relicEffectData, this.TargetCardTraitParam);
            BuilderUtils.ImportStandardLocalization(this.TooltipBodyKey, this.TooltipBody);
            AccessTools.Field(typeof(RelicEffectData), "tooltipBodyKey").SetValue(relicEffectData, this.TooltipBodyKey);
            BuilderUtils.ImportStandardLocalization(this.TooltipTitleKey, this.TooltipTitle);
            AccessTools.Field(typeof(RelicEffectData), "tooltipTitleKey").SetValue(relicEffectData, this.TooltipTitleKey);
            AccessTools.Field(typeof(RelicEffectData), "traits").SetValue(relicEffectData, this.Traits);
            AccessTools.Field(typeof(RelicEffectData), "triggers").SetValue(relicEffectData, this.Triggers);
            AccessTools.Field(typeof(RelicEffectData), "triggerTooltipsSuppressed").SetValue(relicEffectData, this.TriggerTooltipsSuppressed);
            return relicEffectData;
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
