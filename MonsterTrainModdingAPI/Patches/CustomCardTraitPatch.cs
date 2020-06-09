using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using System.Linq;

namespace MonsterTrainModdingAPI.Patches
{
    /// <summary>
    /// The base game instantiates card traits by using reflection on their names.
    /// This is done using Type.GetType().
    /// Type.GetType() only searches the core library and current assembly by default,
    /// so it is insufficient to add new trait types.
    /// This patch changes the means through which traits are instantiated by also searching all running assemblies.
    /// </summary>
    [HarmonyPatch(typeof(CardState), "GetNewCardTraitState")]
    public class CreateCustomCardTraitPatch : CardTraitState
    {
        /// <summary>
        /// Maps type names to their appropriate types.
        /// Searching for a type is a costly process, so search results are cached.
        /// </summary>
        public static IDictionary<string, Type> Cache { get; private set; } = new Dictionary<string, Type>();

        static bool Prefix(ref CardState __instance, ref CardTraitState __result, ref CardTraitData traitData)
        {
            Type type = null;
            string traitName = traitData.GetTraitStateName();

            if (Cache.ContainsKey(traitName))
            {
                // Search for the trait type in the cache
                type = Cache[traitName];
            }
            else
            {
                // Search all running assemblies for a type with the correct name, 
                // then store it in the cache so we never have to do it again
                type = AppDomain.CurrentDomain
                    .GetAssemblies()
                    .SelectMany(x => x.GetTypes())
                    .First(t => t.Name == traitName);
                Cache[traitName] = type;
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
