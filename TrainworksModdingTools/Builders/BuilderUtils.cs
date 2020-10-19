using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.Enums;
using Trainworks.Managers;

namespace Trainworks.Builders
{
    public class BuilderUtils
    {
        /// <summary>
        /// Create a new status effect array and add the status effect with the specified information onto the end of it.
        /// </summary>
        /// <param name="statusEffectID">ID of the status effect</param>
        /// <param name="stackCount">Number of stacks to apply</param>
        /// <param name="oldStatuses">Status effect array to append to</param>
        /// <returns>A new status effect array one element longer than the previous one, with the status effect in the last slot</returns>
        public static StatusEffectStackData[] AddStatusEffect(string statusEffectID, int stackCount, StatusEffectStackData[] oldStatuses)
        {
            var statusEffectData = new StatusEffectStackData
            {
                statusId = statusEffectID,
                count = stackCount
            };
            var newStatuses = new StatusEffectStackData[oldStatuses.Length + 1];
            int i;
            for (i = 0; i < oldStatuses.Length; i++)
            {
                newStatuses[i] = oldStatuses[i];
            }
            newStatuses[i] = statusEffectData;
            return newStatuses;
        }

        /// <summary>
        /// Imports localization data for a key.
        /// Sets the translation to text for all languages.
        /// If either key or text is null, the function returns harmlessly.
        /// </summary>
        /// <param name="key">Key to set localization data for</param>
        /// <param name="text">Text for the key in all languages</param>
        public static void ImportStandardLocalization(string key, string text)
        {
            if (key != null && text != null)
            {
                CustomLocalizationManager.ImportSingleLocalization(key, "Text", "", "", "", "", text, text, text, text, text, text);
            }
        }
    }
}
