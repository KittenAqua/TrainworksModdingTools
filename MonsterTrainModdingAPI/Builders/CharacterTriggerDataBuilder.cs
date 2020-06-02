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
    public class CharacterTriggerDataBuilder
    {
        public CharacterTriggerData.Trigger Trigger { get; set; }
        public List<CardEffectData> Effects { get; set; }

        public string DescriptionKey { get; set; }
        public string AdditionalTextOnTriggerKey { get; set; }

        public bool DisplayEffectHintText { get; set; }
        public bool HideTriggerTooltip { get; set; }

        public CharacterTriggerDataBuilder()
        {
            this.Effects = new List<CardEffectData>();
        }

        public CharacterTriggerData Build()
        {
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
