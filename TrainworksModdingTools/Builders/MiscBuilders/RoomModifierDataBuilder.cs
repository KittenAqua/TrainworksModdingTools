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
using Trainworks.Enums;

namespace Trainworks.Builders
{
    public class RoomModifierDataBuilder
    {
        /// <summary>
        /// Don't set directly; use RoomStateModifierClassType instead.
        /// Type of the room state modifier class to instantiate.
        /// </summary>
        public Type roomStateModifierClassType;

        /// <summary>
        /// Type of the room state modifier class to instantiate.
        /// Implicitly sets RoomStateModifierClassName.
        /// </summary>
        public Type RoomStateModifierClassType
        {
            get { return this.roomStateModifierClassType; }
            set
            {
                this.roomStateModifierClassType = value;
                this.RoomStateModifierClassName = this.roomStateModifierClassType.AssemblyQualifiedName;
            }
        }

        /// <summary>
        /// Don't set directly; use RoomStateModifierClassName instead.
        /// </summary>
        public string roomStateModifierClassName;

        /// <summary>
        /// Implicitly sets DescriptionKey, ExtraTooltipBodyKey, and ExtraTooltipTitleKey if null.
        /// </summary>
        public string RoomStateModifierClassName
        {
            get { return this.roomStateModifierClassName; }
            set
            {
                this.roomStateModifierClassName = value;
                if (this.DescriptionKey == null)
                {
                    this.DescriptionKey = this.roomStateModifierClassName + "_RoomModifierData_DescriptionKey";
                }
                if (this.ExtraTooltipBodyKey == null)
                {
                    this.ExtraTooltipBodyKey = this.roomStateModifierClassName + "_RoomModifierData_ExtraTooltipBodyKey";
                }
                if (this.ExtraTooltipTitleKey == null)
                {
                    this.ExtraTooltipTitleKey = this.roomStateModifierClassName + "_RoomModifierData_ExtraTooltipTitleKey";
                }
            }
        }

        public string Description { get; set; }
        public string DescriptionKey { get; set; }
        public string ExtraTooltipBody { get; set; }
        public string ExtraTooltipTitle { get; set; }
        public string ExtraTooltipTitleKey { get; set; }
        public string ExtraTooltipBodyKey { get; set; }

        public Sprite Icon { get; set; }
        public int ParamInt { get; set; }
        public string ParamSubtype { get; set; }
        public StatusEffectStackData[] ParamStatusEffects { get; set; }

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
            BuilderUtils.ImportStandardLocalization(this.DescriptionKey, this.Description);
            AccessTools.Field(typeof(RoomModifierData), "descriptionKey").SetValue(roomModifierData, this.DescriptionKey);
            BuilderUtils.ImportStandardLocalization(this.ExtraTooltipBodyKey, this.ExtraTooltipBody);
            AccessTools.Field(typeof(RoomModifierData), "extraTooltipBodyKey").SetValue(roomModifierData, this.ExtraTooltipBodyKey);
            BuilderUtils.ImportStandardLocalization(this.ExtraTooltipTitleKey, this.ExtraTooltipTitle);
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
