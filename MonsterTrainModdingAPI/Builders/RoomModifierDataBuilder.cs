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
    public class RoomModifierDataBuilder
    {

        public string RoomStateModifierClassName { get; set; }
        public string DescriptionKey { get; set; }
        public Sprite Icon { get; set; }
        public int ParamInt { get; set; }
        public string ParamSubtype { get; set; }
        public StatusEffectStackData[] ParamStatusEffects { get; set; }
        public string ExtraTooltipTitleKey { get; set; }
        public string ExtraTooltipBodyKey { get; set; }

        public RoomModifierDataBuilder()
        {
        }

        /// <summary>
        /// Builds the RoomModifierData represented by this builders's parameters recursively;
        /// </summary>
        /// <returns>The newly created RoomModifierData</returns>
        public RoomModifierData Build()
        {
            RoomModifierData roomModifierData = new RoomModifierData();
            AccessTools.Field(typeof(RoomModifierData), "descriptionKey").SetValue(roomModifierData, this.DescriptionKey);
            AccessTools.Field(typeof(RoomModifierData), "extraTooltipBodyKey").SetValue(roomModifierData, this.ExtraTooltipBodyKey);
            AccessTools.Field(typeof(RoomModifierData), "extraTooltipTitleKey").SetValue(roomModifierData, this.ExtraTooltipTitleKey);
            AccessTools.Field(typeof(RoomModifierData), "icon").SetValue(roomModifierData, this.Icon);
            AccessTools.Field(typeof(RoomModifierData), "paramInt").SetValue(roomModifierData, this.ParamInt);
            AccessTools.Field(typeof(RoomModifierData), "paramStatusEffects").SetValue(roomModifierData, this.ParamStatusEffects);
            AccessTools.Field(typeof(RoomModifierData), "paramSubtype").SetValue(roomModifierData, this.ParamSubtype);
            AccessTools.Field(typeof(RoomModifierData), "roomStateModifierClassName").SetValue(roomModifierData, this.RoomStateModifierClassName);
            return roomModifierData;
        }

        /// <summary>
        /// Add a status effect to this room's params.
        /// </summary>
        /// <param name="statusEffectID">ID of the status effect, most easily retrieved using the helper class "MTStatusEffectIDs"</param>
        /// <param name="stackCount">Number of stacks to apply</param>
        public void AddStartingStatusEffect(string statusEffectID, int stackCount)
        {
            this.ParamStatusEffects = BuilderUtils.AddStatusEffect(statusEffectID, stackCount, this.ParamStatusEffects);
        }
    }
}
