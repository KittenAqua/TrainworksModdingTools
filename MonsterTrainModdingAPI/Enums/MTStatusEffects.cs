using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterTrainModdingAPI.Enums.MTStatusEffects
{
    public interface IMTStatusEffect
    {
        string ID { get; }
    }

    public class MTStatusEffect_Quick : IMTStatusEffect { public string ID => "ambush"; }
    public class MTStatusEffect_Armor : IMTStatusEffect { public string ID => "armor"; }
    public class MTStatusEffect_Rage : IMTStatusEffect { public string ID => "buff"; }
    public class MTStatusEffect_Soul : IMTStatusEffect { public string ID => "captured_soul"; }
    public class MTStatusEffect_Cardless : IMTStatusEffect { public string ID => "cardless"; }
    public class MTStatusEffect_DamageShield : IMTStatusEffect { public string ID => "damage shield"; }
    public class MTStatusEffect_Dazed : IMTStatusEffect { public string ID => "dazed"; }
    public class MTStatusEffect_Sap : IMTStatusEffect { public string ID => "debuff"; }
    public class MTStatusEffect_Endless : IMTStatusEffect { public string ID => "endless"; }
    public class MTStatusEffect_Burnout : IMTStatusEffect { public string ID => "ephemeral"; }
    public class MTStatusEffect_Haste : IMTStatusEffect { public string ID => "haste"; }
    public class MTStatusEffect_HealImmunity : IMTStatusEffect { public string ID => "heal immunity"; }
    public class MTStatusEffect_Sweep : IMTStatusEffect { public string ID => "hunter"; }
    public class MTStatusEffect_Immobile : IMTStatusEffect { public string ID => "immobile"; }
    public class MTStatusEffect_Immune : IMTStatusEffect { public string ID => "immune"; }
    public class MTStatusEffect_Inert : IMTStatusEffect { public string ID => "inert"; }
    public class MTStatusEffect_Lifesteal : IMTStatusEffect { public string ID => "lifesteal"; }
    public class MTStatusEffect_Multistrike : IMTStatusEffect { public string ID => "multistrike"; }
    public class MTStatusEffect_Frostbite : IMTStatusEffect { public string ID => "poison"; }
    public class MTStatusEffect_Regen : IMTStatusEffect { public string ID => "regen"; }
    public class MTStatusEffect_Relentless : IMTStatusEffect { public string ID => "relentless"; }
    public class MTStatusEffect_Rooted : IMTStatusEffect { public string ID => "rooted"; }
    public class MTStatusEffect_Emberdrain : IMTStatusEffect { public string ID => "scorch"; }
    public class MTStatusEffect_Silenced : IMTStatusEffect { public string ID => "silenced"; }
    public class MTStatusEffect_Fuel : IMTStatusEffect { public string ID => "spark"; }
    public class MTStatusEffect_SpellShield : IMTStatusEffect { public string ID => "spell shield"; }
    public class MTStatusEffect_SpellWeakness : IMTStatusEffect { public string ID => "spell weakness"; }
    public class MTStatusEffect_Spikes : IMTStatusEffect { public string ID => "spikes"; }
    public class MTStatusEffect_Stealth : IMTStatusEffect { public string ID => "stealth"; }
    public class MTStatusEffect_Trample : IMTStatusEffect { public string ID => "trample"; }

    public static class MTStatusEffectIDs
    {
        public static string GetIDForType(Type statusEffectType)
        {
            if (typeof(IMTStatusEffect).IsAssignableFrom(statusEffectType))
            {
                var statusEffect = (IMTStatusEffect)Activator.CreateInstance(statusEffectType);
                return statusEffect.ID;
            }
            return "";
        }
    }
}
