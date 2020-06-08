using System;
using System.Collections.Generic;
using System.Text;
using MonsterTrainModdingAPI.Enums;

namespace MonsterTrainModdingAPI.Builders
{
    public class BuilderUtils
    {
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
