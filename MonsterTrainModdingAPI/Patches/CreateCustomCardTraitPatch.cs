using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using System.Linq;

namespace MonsterTrainModdingAPI.Patches
{
    public static class CardTraitStateTypeCache
    {
        public static IDictionary<string, Type> Cache { get; private set; } = new Dictionary<string, Type>();
    }

    [HarmonyPatch(typeof(CardState), "GetNewCardTraitState")]
    public class CreateCustomCardTraitPatch : CardTraitState
    {
        static bool Prefix(ref CardState __instance, ref CardTraitState __result, ref CardTraitData traitData)
        {
            Type type = null;
            string traitName = traitData.GetTraitStateName();

            if (CardTraitStateTypeCache.Cache.ContainsKey(traitName))
            {
                // Search for the trait type in the cache
                type = CardTraitStateTypeCache.Cache[traitName];
            }
            else
            {
                // Search all running assemblies for a type with the correct name, 
                // then store it in the cache so we never have to do it again
                type = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .First(t => t.Name == traitName);
                CardTraitStateTypeCache.Cache[traitName] = type;
            }

            // Instantiate an instance of the trait type and set the return value to it
            CardTraitState cardTraitState = (CardTraitState)Activator.CreateInstance(type);
            cardTraitState.Setup(traitData, __instance);
            __result = cardTraitState;

            // Don't run the original method since it doesn't know where to look for trait types
            return false;
        }
    }
}
