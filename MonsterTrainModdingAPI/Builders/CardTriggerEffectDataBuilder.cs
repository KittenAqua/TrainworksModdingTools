using System;
using System.Collections.Generic;
using BepInEx;
using BepInEx.Harmony;
using System.Reflection;
using BepInEx.Logging;
using HarmonyLib;
using MonsterTrainModdingAPI.Enum;
using UnityEngine;
using UnityEngine.AddressableAssets;
using ShinyShoe;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI.Builder
{
    public class CardTriggerEffectDataBuilder
    {
        // Card Trigger Types:
        //     OnCast
        //     OnKill
        //     OnDiscard
        //     OnMonsterDeath
        //     OnAnyMonsterDeathOnFloor
        //     OnAnyHeroDeathOnFloor
        //     OnHealed
        //     OnPlayerDamageTaken
        //     OnAnyUnitDeathOnFloor
        //     OnTreasure
        //     OnUnplayed
        //     OnFeed

        public CardTriggerType trigger { get; set; }
        public string descriptionKey { get; set; }
        public List<CardTriggerData> cardTriggerEffects { get; set; }
        public List<CardEffectData> cardEffects { get; set; }

        public CardTriggerEffectDataBuilder()
        {
            this.cardTriggerEffects = new List<CardTriggerData>();
            this.cardEffects = new List<CardEffectData>();
        }

        public CardTriggerEffectData Build()
        {
            CardTriggerEffectData cardTriggerEffectData = new CardTriggerEffectData();
            AccessTools.Field(typeof(CardTriggerEffectData), "trigger").SetValue(cardTriggerEffectData, this.cardEffects);
            AccessTools.Field(typeof(CardTriggerEffectData), "descriptionKey").SetValue(cardTriggerEffectData, this.descriptionKey);
            AccessTools.Field(typeof(CardTriggerEffectData), "cardTriggerEffects").SetValue(cardTriggerEffectData, this.cardTriggerEffects);
            AccessTools.Field(typeof(CardTriggerEffectData), "cardEffects").SetValue(cardTriggerEffectData, this.cardEffects);

            return cardTriggerEffectData;
        }
    }
}
