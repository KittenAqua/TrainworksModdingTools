using System;
using System.Collections.Generic;
using System.Text;

namespace Trainworks.Constants
{
    /// <summary>
    /// Provides easy access to all of the base game's card trigger effect types
    /// </summary>
    public static class VanillaCardTriggerEffectTypes
    {
        public static readonly Type CardTriggerEffectBuffCharacterDamage = typeof(CardTriggerEffectBuffCharacterDamage);
        public static readonly Type CardTriggerEffectBuffCharacterMaxHP = typeof(CardTriggerEffectBuffCharacterMaxHP);
        public static readonly Type CardTriggerEffectBuffSpellDamage = typeof(CardTriggerEffectBuffSpellDamage);
        public static readonly Type CardTriggerEffectDrawAdditionalNextTurn = typeof(CardTriggerEffectDrawAdditionalNextTurn);
        public static readonly Type CardTriggerEffectGainEnergyNextTurn = typeof(CardTriggerEffectGainEnergyNextTurn);
    }
}
