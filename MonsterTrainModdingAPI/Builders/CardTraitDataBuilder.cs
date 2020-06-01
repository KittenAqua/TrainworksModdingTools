using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ShinyShoe;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Enum;

namespace MonsterTrainModdingAPI.Builder
{
    public class CardTraitDataBuilder
    {
        public string TraitStateName { get; set; }

        public CardData ParamCardData { get; set; }
        public CardStatistics.CardTypeTarget ParamCardType { get; set; }
        public Team.Type ParamTeamType { get; set; }

        public float ParamFloat { get; set; }
        public int ParamInt { get; set; }
        public string ParamStr { get; set; }
        public string ParamSubtype { get; set; }

        public StatusEffectStackData[] ParamStatusEffects { get; set; }

        public CardStatistics.EntryDuration ParamEntryDuration { get; set; }
        public CardStatistics.TrackedValueType ParamTrackedValue { get; set; }
        public bool ParamUseScalingParams { get; set; }

        public CardTraitDataBuilder()
        {
            this.ParamStatusEffects = new StatusEffectStackData[0];
        }

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

        public void AddStatusEffect(MTStatusEffect statusEffect, int stackCount)
        {
            this.ParamStatusEffects = BuilderUtils.AddStatusEffect(statusEffect, stackCount, this.ParamStatusEffects);
        }
    }
}
