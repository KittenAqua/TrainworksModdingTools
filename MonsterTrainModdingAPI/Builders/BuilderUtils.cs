﻿using System;
using System.Collections.Generic;
using System.Text;
using MonsterTrainModdingAPI.Enums;

namespace MonsterTrainModdingAPI.Builders
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
    }
}
