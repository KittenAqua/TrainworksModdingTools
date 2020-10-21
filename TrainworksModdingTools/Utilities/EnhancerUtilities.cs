using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Trainworks.Managers;

namespace TrainworksModdingTools.Utilities
{
    class EnhancerUtilities
    {
        public static void AddElementToEnhancerList(string element, string enhancerID, string enhancerListName)
        {
            var enhancerData = ProviderManager.SaveManager.GetAllGameData().FindEnhancerData(enhancerID);
            var filter = enhancerData.GetEffects()[0].GetParamCardUpgradeData().GetFilters()[0];
            var list = Traverse.Create(filter).Field(enhancerListName).GetValue<List<string>>();
            list.Add(element);
        }

        /// <summary>
        /// Allows a card with effect to be selected as a valid target for an enhancer
        /// </summary>
        /// <param name="effectID">ID of the effect to add</param>
        /// <param name="enhancerID">Enhancer to allow the effect for</param>
        public static void AddEffectToEnhancer(string effectID, string enhancerID)
        {
            AddElementToEnhancerList(effectID, enhancerID, "requiredCardEffects");
        }

        public static void AddTraitToEnhancer(string traitID, string enhancerID)
        {
            AddElementToEnhancerList(traitID, enhancerID, "requiredCardTraits");
        }

        public static void ExcludeEffectFromEnhancer(string effectID, string enhancerID)
        {
            AddElementToEnhancerList(effectID, enhancerID, "excludedCardEffects");
        }

        public static void ExcludeTraitFromEnhancer(string effectID, string enhancerID)
        {
            AddElementToEnhancerList(effectID, enhancerID, "excludedCardTraits");
        }
    }
}
