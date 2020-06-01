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
    public class CardEffectDataBuilder
    {
        public string EffectStateName { get; set; }

        public int ParamInt { get; set; }
        public int AdditionalParamInt { get; set; }
        public int ParamMaxInt { get; set; }
        public int ParamMinInt { get; set; }
        public float ParamMultiplier { get; set; }
        public bool ParamBool { get; set; }
        public string ParamStr { get; set; }
        public string ParamSubtype { get; set; }
        public string StatusEffectStackMultiplier { get; set; }

        public RoomData ParamRoomData { get; set; }
        public CharacterData ParamAdditionalCharacterData { get; set; }
        public CardUpgradeMaskData ParamCardFilter { get; set; }
        public CardPool ParamCardPool { get; set; }
        public CardUpgradeData ParamCardUpgradeData { get; set; }
        public CharacterData ParamCharacterData { get; set; }
        public Vector3 ParamTimingDelays { get; set; }
        public CharacterTriggerData.Trigger ParamTrigger { get; set; }

        public StatusEffectStackData[] ParamStatusEffects { get; set; }

        public AdditionalTooltipData[] AdditionalTooltips { get; set; }

        public CharacterUI.Anim AnimToPlay { get; set; }
        public VfxAtLoc AppliedToSelfVFX { get; set; }
        public VfxAtLoc AppliedVFX { get; set; }

        public bool UseIntRange { get; set; }
        public bool UseStatusEffectStackMultiplier { get; set; }
        public bool CopyModifiersFromSource { get; set; }
        public bool FilterBasedOnMainSubClass { get; set; }
        public bool HideTooltip { get; set; }
        public bool IgnoreTemporaryModifiersFromSource { get; set; }
        public bool ShouldTest { get; set; }

        public CardEffectData.CardSelectionMode TargetCardSelectionMode { get; set; }
        public CardType TargetCardType { get; set; }
        public string TargetCharacterSubtype { get; set; }
        public bool TargetIgnoreBosses { get; set; }
        public bool TargetIgnorePyre { get; set; }
        public TargetMode TargetMode { get; set; }
        public CardEffectData.HealthFilter TargetModeHealthFilter { get; set; }
        public string[] TargetModeStatusEffectsFilter { get; set; }
        public Team.Type TargetTeamType { get; set; }

        public CardEffectDataBuilder()
        {
            this.TargetTeamType = Team.Type.Heroes;
            this.ShouldTest = true;
            this.ParamMultiplier = 1f;
            this.ParamStatusEffects = new StatusEffectStackData[0];
            this.ParamTimingDelays = Vector3.zero;
            this.AdditionalTooltips = new AdditionalTooltipData[0];
            this.TargetModeStatusEffectsFilter = new string[0];
        }

        public CardEffectData Build()
        {
            CardEffectData cardEffectData = new CardEffectData();
            AccessTools.Field(typeof(CardEffectData), "additionalParamInt").SetValue(cardEffectData, this.AdditionalParamInt);
            AccessTools.Field(typeof(CardEffectData), "additionalTooltips").SetValue(cardEffectData, this.AdditionalTooltips);
            AccessTools.Field(typeof(CardEffectData), "animToPlay").SetValue(cardEffectData, this.AnimToPlay);
            AccessTools.Field(typeof(CardEffectData), "appliedToSelfVFX").SetValue(cardEffectData, this.AppliedToSelfVFX);
            AccessTools.Field(typeof(CardEffectData), "appliedVFX").SetValue(cardEffectData, this.AppliedVFX);
            AccessTools.Field(typeof(CardEffectData), "copyModifiersFromSource").SetValue(cardEffectData, this.CopyModifiersFromSource);
            AccessTools.Field(typeof(CardEffectData), "effectStateName").SetValue(cardEffectData, this.EffectStateName);
            AccessTools.Field(typeof(CardEffectData), "filterBasedOnMainSubClass").SetValue(cardEffectData, this.FilterBasedOnMainSubClass);
            AccessTools.Field(typeof(CardEffectData), "hideTooltip").SetValue(cardEffectData, this.HideTooltip);
            AccessTools.Field(typeof(CardEffectData), "ignoreTemporaryModifiersFromSource").SetValue(cardEffectData, this.IgnoreTemporaryModifiersFromSource);
            AccessTools.Field(typeof(CardEffectData), "paramAdditionalCharacterData").SetValue(cardEffectData, this.ParamAdditionalCharacterData);
            AccessTools.Field(typeof(CardEffectData), "paramBool").SetValue(cardEffectData, this.ParamBool);
            AccessTools.Field(typeof(CardEffectData), "paramCardFilter").SetValue(cardEffectData, this.ParamCardFilter);
            AccessTools.Field(typeof(CardEffectData), "paramCardPool").SetValue(cardEffectData, this.ParamCardPool);
            AccessTools.Field(typeof(CardEffectData), "paramCardUpgradeData").SetValue(cardEffectData, this.ParamCardUpgradeData);
            AccessTools.Field(typeof(CardEffectData), "paramCharacterData").SetValue(cardEffectData, this.ParamCharacterData);
            AccessTools.Field(typeof(CardEffectData), "paramInt").SetValue(cardEffectData, this.ParamInt);
            AccessTools.Field(typeof(CardEffectData), "paramMaxInt").SetValue(cardEffectData, this.ParamMaxInt);
            AccessTools.Field(typeof(CardEffectData), "paramMinInt").SetValue(cardEffectData, this.ParamMinInt);
            AccessTools.Field(typeof(CardEffectData), "paramMultiplier").SetValue(cardEffectData, this.ParamMultiplier);
            AccessTools.Field(typeof(CardEffectData), "paramRoomData").SetValue(cardEffectData, this.ParamRoomData);
            AccessTools.Field(typeof(CardEffectData), "paramStatusEffects").SetValue(cardEffectData, this.ParamStatusEffects);
            AccessTools.Field(typeof(CardEffectData), "paramStr").SetValue(cardEffectData, this.ParamStr);
            AccessTools.Field(typeof(CardEffectData), "paramSubtype").SetValue(cardEffectData, this.ParamSubtype);
            AccessTools.Field(typeof(CardEffectData), "paramTimingDelays").SetValue(cardEffectData, this.ParamTimingDelays);
            AccessTools.Field(typeof(CardEffectData), "paramTrigger").SetValue(cardEffectData, this.ParamTrigger);
            AccessTools.Field(typeof(CardEffectData), "shouldTest").SetValue(cardEffectData, this.ShouldTest);
            AccessTools.Field(typeof(CardEffectData), "statusEffectStackMultiplier").SetValue(cardEffectData, this.StatusEffectStackMultiplier);
            //AccessTools.Field(typeof(CardData), "stringBuilder").SetValue(cardData, this.);
            AccessTools.Field(typeof(CardEffectData), "targetCardSelectionMode").SetValue(cardEffectData, this.TargetCardSelectionMode);
            AccessTools.Field(typeof(CardEffectData), "targetCardType").SetValue(cardEffectData, this.TargetCardType);
            AccessTools.Field(typeof(CardEffectData), "targetCharacterSubtype").SetValue(cardEffectData, this.TargetCharacterSubtype);
            AccessTools.Field(typeof(CardEffectData), "targetIgnoreBosses").SetValue(cardEffectData, this.TargetIgnoreBosses);
            AccessTools.Field(typeof(CardEffectData), "targetIgnorePyre").SetValue(cardEffectData, this.TargetIgnorePyre);
            AccessTools.Field(typeof(CardEffectData), "targetMode").SetValue(cardEffectData, this.TargetMode);
            AccessTools.Field(typeof(CardEffectData), "targetModeHealthFilter").SetValue(cardEffectData, this.TargetModeHealthFilter);
            AccessTools.Field(typeof(CardEffectData), "targetModeStatusEffectsFilter").SetValue(cardEffectData, this.TargetModeStatusEffectsFilter);
            AccessTools.Field(typeof(CardEffectData), "targetTeamType").SetValue(cardEffectData, this.TargetTeamType);
            AccessTools.Field(typeof(CardEffectData), "useIntRange").SetValue(cardEffectData, this.UseIntRange);
            AccessTools.Field(typeof(CardEffectData), "useStatusEffectStackMultiplier").SetValue(cardEffectData, this.UseStatusEffectStackMultiplier);
            return cardEffectData;
        }

        public void AddStatusEffect(MTStatusEffect statusEffect, int stackCount)
        {
            string statusEffectID = StatusEffectIds.GetStatusEffectId(statusEffect);
            var statusEffectData = new StatusEffectStackData
            {
                statusId = statusEffectID,
                count = stackCount
            };
            var newStatusEffectStackData = new StatusEffectStackData[this.ParamStatusEffects.Length + 1];
            int i;
            for (i = 0; i < this.ParamStatusEffects.Length; i++)
            {
                newStatusEffectStackData[i] = this.ParamStatusEffects[i];
            }
            newStatusEffectStackData[i] = statusEffectData;
            this.ParamStatusEffects = newStatusEffectStackData;
        }
    }
}
