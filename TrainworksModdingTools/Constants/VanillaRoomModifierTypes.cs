using System;
using System.Collections.Generic;
using System.Text;

namespace Trainworks.Constants
{
    /// <summary>
    /// Provides easy access to all of the base game's room modifier types
    /// </summary>
    public static class VanillaRoomModifierTypes
    {
        public static readonly Type RoomStateBuffModifier = typeof(RoomStateBuffModifier);
        public static readonly Type RoomStateCostModifierBase = typeof(RoomStateCostModifierBase);
        public static readonly Type RoomStateDamageCostModifier = typeof(RoomStateDamageCostModifier);
        public static readonly Type RoomStateEnergyModifier = typeof(RoomStateEnergyModifier);
        public static readonly Type RoomStateFirstSpellCostModifier = typeof(RoomStateFirstSpellCostModifier);
        public static readonly Type RoomStateFriendlyDamageMultiplierModifier = typeof(RoomStateFriendlyDamageMultiplierModifier);
        public static readonly Type RoomStateFriendlyDamagePerCorruptionModifier = typeof(RoomStateFriendlyDamagePerCorruptionModifier);
        public static readonly Type RoomStateHandSizeModifier = typeof(RoomStateHandSizeModifier);
        public static readonly Type RoomStateHealCostModifier = typeof(RoomStateHealCostModifier);
        public static readonly Type RoomStateMagicalPowerModifier = typeof(RoomStateMagicalPowerModifier);
        public static readonly Type RoomStateRegenModifier = typeof(RoomStateRegenModifier);
        public static readonly Type RoomStateSelfDamagePerCorruptionModifier = typeof(RoomStateSelfDamagePerCorruptionModifier);
        public static readonly Type RoomStateSelfDamageWhenCorruptionModifier = typeof(RoomStateSelfDamageWhenCorruptionModifier);
        public static readonly Type RoomStateSpawnUnitOnUnitSpawnModifier = typeof(RoomStateSpawnUnitOnUnitSpawnModifier);
        public static readonly Type RoomStateSpellCostModifier = typeof(RoomStateSpellCostModifier);
        public static readonly Type RoomStateSpikesDamageModifier = typeof(RoomStateSpikesDamageModifier);
        public static readonly Type RoomStateStatusEffectDamageModifier = typeof(RoomStateStatusEffectDamageModifier);
        public static readonly Type RoomStateStatusEffectOnSpawnModifier = typeof(RoomStateStatusEffectOnSpawnModifier);
        public static readonly Type RoomStateUnitCostModifier = typeof(RoomStateUnitCostModifier);
    }
}
