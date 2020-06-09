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
using MonsterTrainModdingAPI.Enums;

namespace MonsterTrainModdingAPI.Builders
{
    public class CharacterTriggerDataBuilder
    {
        public CharacterTriggerData.Trigger Trigger { get; set; }

        /// <summary>
        /// Append to this list to add new card effects. The Build() method recursively builds all nested builders.
        /// </summary>
        public List<CardEffectDataBuilder> EffectBuilders { get; set; }
        /// <summary>
        /// List of pre-built card effects.
        /// </summary>
        public List<CardEffectData> Effects { get; set; }

        /// <summary>
        /// Use an existing base game trigger's description key to copy the format of its description.
        /// </summary>
        public string DescriptionKey { get; set; }
        public string AdditionalTextOnTriggerKey { get; set; }

        public bool DisplayEffectHintText { get; set; }
        public bool HideTriggerTooltip { get; set; }

        public CharacterTriggerDataBuilder()
        {
            this.EffectBuilders = new List<CardEffectDataBuilder>();
            this.Effects = new List<CardEffectData>();
        }

        /// <summary>
        /// Builds the CharacterTriggerData represented by this builders's parameters recursively;
        /// all Builders represented in this class's various fields will also be built.
        /// </summary>
        /// <returns>The newly created CardTraitData</returns>
        public CharacterTriggerData Build()
        {
            foreach (var builder in this.EffectBuilders)
            {
                this.Effects.Add(builder.Build());
            }
            CharacterTriggerData characterTriggerData = new CharacterTriggerData(this.Trigger, null);
            AccessTools.Field(typeof(CharacterTriggerData), "additionalTextOnTriggerKey").SetValue(characterTriggerData, this.AdditionalTextOnTriggerKey);
            AccessTools.Field(typeof(CharacterTriggerData), "descriptionKey").SetValue(characterTriggerData, this.DescriptionKey);
            AccessTools.Field(typeof(CharacterTriggerData), "displayEffectHintText").SetValue(characterTriggerData, this.DisplayEffectHintText);
            AccessTools.Field(typeof(CharacterTriggerData), "effects").SetValue(characterTriggerData, this.Effects);
            AccessTools.Field(typeof(CharacterTriggerData), "hideTriggerTooltip").SetValue(characterTriggerData, this.HideTriggerTooltip);
            AccessTools.Field(typeof(CharacterTriggerData), "trigger").SetValue(characterTriggerData, this.Trigger);
            return characterTriggerData;
        }
    }
}
