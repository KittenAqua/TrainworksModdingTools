using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterTrainModdingAPI.Enums
{
    public enum MTStatusEffect
    {
        Relentless,
        Hunter,
        Multistrike,
        Ambush,
        Trample,
        Immobile,
        Inert,
        Spark,
        Immune,
        Endless,
        Silenced,
        Haste,
        Armor,
        Ephemeral,
        Cardless,
        Buff,
        Spikes,
        DamageShield,
        SpellShield,
        Regen,
        CapturedSoul,
        Stealth,
        Lifesteal,
        HealImmunity,
        Rooted,
        Dazed,
        Debuff,
        Poison,
        SpellWeakness,
        Scorch
    }

    public static class StatusEffectIds
    {
        private static readonly Dictionary<MTStatusEffect, string> statusEffectsDictionary = new Dictionary<MTStatusEffect, string>
        {
            { MTStatusEffect.Ambush, "ambush" },
            { MTStatusEffect.Armor, "armor" },
            { MTStatusEffect.Buff, "buff" },
            { MTStatusEffect.CapturedSoul, "captured_soul" },
            { MTStatusEffect.Cardless, "cardless" },
            { MTStatusEffect.DamageShield, "damage shield" },
            { MTStatusEffect.Dazed, "dazed" },
            { MTStatusEffect.Debuff, "debuff" },
            { MTStatusEffect.Endless, "endless" },
            { MTStatusEffect.Ephemeral, "ephemeral" },
            { MTStatusEffect.Haste, "haste" },
            { MTStatusEffect.HealImmunity, "heal immunity" },
            { MTStatusEffect.Hunter, "hunter" },
            { MTStatusEffect.Immobile, "immobile" },
            { MTStatusEffect.Immune, "immune" },
            { MTStatusEffect.Inert, "inert" },
            { MTStatusEffect.Lifesteal, "lifesteal" },
            { MTStatusEffect.Multistrike, "multistrike" },
            { MTStatusEffect.Poison, "poison" },
            { MTStatusEffect.Regen, "regen" },
            { MTStatusEffect.Relentless, "relentless" },
            { MTStatusEffect.Rooted, "rooted" },
            { MTStatusEffect.Scorch, "scorch" },
            { MTStatusEffect.Silenced, "silenced" },
            { MTStatusEffect.Spark, "spark" },
            { MTStatusEffect.SpellShield, "spell shield" },
            { MTStatusEffect.SpellWeakness, "spell weakness" },
            { MTStatusEffect.Spikes, "spikes" },
            { MTStatusEffect.Stealth, "stealth" },
            { MTStatusEffect.Trample, "trample" }
        };

        public static string GetStatusEffectId(MTStatusEffect statusEffect)
        {
            return statusEffectsDictionary[statusEffect];
        }
    }
}
