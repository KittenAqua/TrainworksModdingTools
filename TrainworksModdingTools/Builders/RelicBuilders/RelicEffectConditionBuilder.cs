using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Trainworks.Builders
{
	public class RelicEffectConditionBuilder
	{
		public CardStatistics.TrackedValueType paramTrackedValue { get; set; }

		public CardStatistics.CardTypeTarget paramCardType { get; set; }

		///Determines if we should use the total trigger count as the tracked value instead of using a value from CardStatistics. Use this to limit triggers to X per turn or per battle.
		public bool paramTrackTriggerCount { get; set; }

		public CardStatistics.EntryDuration paramEntryDuration { get; set; }

		public RelicEffectCondition.Comparator paramComparator { get; set; } = RelicEffectCondition.Comparator.Equal | RelicEffectCondition.Comparator.GreaterThan;

		public int paramInt { get; set; }

		///Allow multiple triggers per duration that is defined as Entry Duration. This can be used to limit a relic from trigger more than X times per turn or per battle
		public bool allowMultipleTriggersPerDuration { get; set; } = true;

		public string paramSubtype { get; set; }

		/// <summary>
		/// Builds the RelicEffectCondition represented by this builder's parameters
		/// </summary>
		/// <returns>The newly created RelicEffectCondition</returns>
		public RelicEffectCondition Build()
		{
			RelicEffectCondition relicEffectCondition = new RelicEffectCondition();

			var t = Traverse.Create(relicEffectCondition);

			t.Field("paramTrackedValue").SetValue(paramTrackedValue);
			t.Field("paramCardType").SetValue(paramCardType);
			t.Field("paramTrackTriggerCount").SetValue(paramTrackTriggerCount);
			t.Field("paramEntryDuration").SetValue(paramEntryDuration);
			t.Field("paramComparator").SetValue(paramComparator);
			t.Field("paramInt").SetValue(paramInt);
			t.Field("allowMultipleTriggersPerDuration").SetValue(allowMultipleTriggersPerDuration);
			t.Field("paramSubtype").SetValue(paramSubtype);

			return relicEffectCondition;
		}
	}
}