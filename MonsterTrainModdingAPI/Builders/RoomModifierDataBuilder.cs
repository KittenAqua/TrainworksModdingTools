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

        public string roomStateModifierClassName;
        public string descriptionKey;
        public Sprite icon;
        public int paramInt;
        public string paramSubtype;
        public StatusEffectStackData[] paramStatusEffects;
        public string extraTooltipTitleKey;
        public string extraTooltipBodyKey;

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
            AccessTools.Field(typeof(RoomModifierData), "roomStateModifierClassName").SetValue(roomModifierData, this.roomStateModifierClassName);
            AccessTools.Field(typeof(RoomModifierData), "descriptionKey").SetValue(roomModifierData, this.descriptionKey);
            AccessTools.Field(typeof(RoomModifierData), "icon").SetValue(roomModifierData, this.icon);
            AccessTools.Field(typeof(RoomModifierData), "paramInt").SetValue(roomModifierData, this.paramInt);
            AccessTools.Field(typeof(RoomModifierData), "paramSubtype").SetValue(roomModifierData, this.paramSubtype);
            AccessTools.Field(typeof(RoomModifierData), "paramStatusEffects").SetValue(roomModifierData, this.paramStatusEffects);
            AccessTools.Field(typeof(RoomModifierData), "extraTooltipTitleKey").SetValue(roomModifierData, this.extraTooltipTitleKey);
            AccessTools.Field(typeof(RoomModifierData), "extraTooltipBodyKey").SetValue(roomModifierData, this.extraTooltipBodyKey);
            return roomModifierData;
        }

        /// <summary>
        /// Add a status effect to this room's params.
        /// </summary>
        /// <param name="statusEffectID">ID of the status effect, most easily retrieved using the helper class "MTStatusEffectIDs"</param>
        /// <param name="stackCount">Number of stacks to apply</param>
        public void AddStartingStatusEffect(string statusEffectID, int stackCount)
        {
            this.paramStatusEffects = BuilderUtils.AddStatusEffect(statusEffectID, stackCount, this.paramStatusEffects);
        }
    }
}
