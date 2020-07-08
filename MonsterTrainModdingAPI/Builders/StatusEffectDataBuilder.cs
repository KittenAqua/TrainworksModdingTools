using HarmonyLib;
using MonsterTrainModdingAPI.Managers;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static CharacterTriggerData;
using static StatusEffectData;

namespace MonsterTrainModdingAPI.Builders
{
	public class StatusEffectDataBuilder
	{
		public string statusEffectStateName { get; set; }
		public string statusId { get; set; }
		public Sprite icon { get; set; }
		public string appliedSFXName { get; set; }
		public string triggeredSFXName { get; set; }

		//[Tooltip("This category determines the color used for the icon.")]
		public StatusEffectData.DisplayCategory displayCategory { get; set; }

		//[Tooltip("The VFX to display on the character when the status effect is added.")]
		public VfxAtLoc addedVFX { get; set; }
		public VfxAtLocList moreAddedVFX { get; set; }

		//[Tooltip("The VFX to display on the character while this status effect is active")]
		public VfxAtLoc persistentVFX { get; set; }
		public VfxAtLocList morePersistentVFX { get; set; }

		//[Tooltip("The VFX to display on the character when the effect is triggered.")]
		public VfxAtLoc triggeredVFX { get; set; }
		public VfxAtLocList moreTriggeredVFX { get; set; }

		//[Tooltip("The VFX to display on the character when the status effect is removed.")]
		public VfxAtLoc removedVFX { get; set; }
		public VfxAtLocList moreRemovedVFX { get; set; }

		//[Tooltip("The VFX to display on a character when it is damaged/affected by this effect.")]
		public VfxAtLoc affectedVFX { get; set; }

		public TriggerStage triggerStage { get; set; }
		public bool removeStackAtEndOfTurn { get; set; }
		public bool removeAtEndOfTurn { get; set; }
		public bool removeWhenTriggered { get; set; }

		//[Tooltip("This is the same as Remove When Triggered except it will be removed only after the card currently being played finishes playing\n\nNOTE: This should only be used for status effects that are triggered by a card being played.")]
		public bool removeWhenTriggeredAfterCardPlayed { get; set; }
		public bool isStackable { get; set; }
		public bool showNotificationsOnRemoval { get; set; }
		public string paramStr { get; set; }
		public int paramInt { get; set; }
		public int paramSecondaryInt { get; set; }
		public float paramFloat { get; set; }

		public StatusEffectDataBuilder()
		{
			isStackable = true;
			showNotificationsOnRemoval = true;
		}

		public StatusEffectData Build()
		{
			StatusEffectData statusEffect = new StatusEffectData();

			AccessTools.Field(typeof(StatusEffectData), "statusEffectStateName").SetValue(statusEffect, statusEffectStateName);
			AccessTools.Field(typeof(StatusEffectData), "statusId").SetValue(statusEffect, statusId);
			AccessTools.Field(typeof(StatusEffectData), "icon").SetValue(statusEffect, icon);
			AccessTools.Field(typeof(StatusEffectData), "appliedSFXName").SetValue(statusEffect, appliedSFXName);
			AccessTools.Field(typeof(StatusEffectData), "triggeredSFXName").SetValue(statusEffect, triggeredSFXName);
			AccessTools.Field(typeof(StatusEffectData), "displayCategory").SetValue(statusEffect, displayCategory);
			AccessTools.Field(typeof(StatusEffectData), "addedVFX").SetValue(statusEffect, addedVFX);
			AccessTools.Field(typeof(StatusEffectData), "moreAddedVFX").SetValue(statusEffect, moreAddedVFX);
			AccessTools.Field(typeof(StatusEffectData), "persistentVFX").SetValue(statusEffect, persistentVFX);
			AccessTools.Field(typeof(StatusEffectData), "morePersistentVFX").SetValue(statusEffect, morePersistentVFX);
			AccessTools.Field(typeof(StatusEffectData), "triggeredVFX").SetValue(statusEffect, triggeredVFX);
			AccessTools.Field(typeof(StatusEffectData), "moreTriggeredVFX").SetValue(statusEffect, moreTriggeredVFX);
			AccessTools.Field(typeof(StatusEffectData), "removedVFX").SetValue(statusEffect, removedVFX);
			AccessTools.Field(typeof(StatusEffectData), "moreRemovedVFX").SetValue(statusEffect, moreRemovedVFX);
			AccessTools.Field(typeof(StatusEffectData), "affectedVFX").SetValue(statusEffect, affectedVFX);
			AccessTools.Field(typeof(StatusEffectData), "triggerStage").SetValue(statusEffect, triggerStage);
			AccessTools.Field(typeof(StatusEffectData), "removeStackAtEndOfTurn").SetValue(statusEffect, removeStackAtEndOfTurn);
			AccessTools.Field(typeof(StatusEffectData), "removeAtEndOfTurn").SetValue(statusEffect, removeAtEndOfTurn);
			AccessTools.Field(typeof(StatusEffectData), "removeWhenTriggered").SetValue(statusEffect, removeWhenTriggered);
			AccessTools.Field(typeof(StatusEffectData), "removeWhenTriggeredAfterCardPlayed").SetValue(statusEffect, removeWhenTriggeredAfterCardPlayed);
			AccessTools.Field(typeof(StatusEffectData), "isStackable").SetValue(statusEffect, isStackable);
			AccessTools.Field(typeof(StatusEffectData), "showNotificationsOnRemoval").SetValue(statusEffect, showNotificationsOnRemoval);
			AccessTools.Field(typeof(StatusEffectData), "paramStr").SetValue(statusEffect, paramStr);
			AccessTools.Field(typeof(StatusEffectData), "paramInt").SetValue(statusEffect, paramInt);
			AccessTools.Field(typeof(StatusEffectData), "paramSecondaryInt").SetValue(statusEffect, paramSecondaryInt);
			AccessTools.Field(typeof(StatusEffectData), "paramFloat").SetValue(statusEffect, paramFloat);

			StatusEffectManager manager = GameObject.FindObjectOfType<StatusEffectManager>() as StatusEffectManager;
			manager.GetAllStatusEffectsData().GetStatusEffectData().Add(statusEffect);

			return statusEffect;
		}
	}
}
