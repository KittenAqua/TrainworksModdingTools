using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using HarmonyLib;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI.Patches
{
    /// <summary>
    /// Adds custom cards to their appropriate pools.
    /// </summary>
    [HarmonyPatch(typeof(CardPoolHelper), "GetCardsForClass")]
    [HarmonyPatch(new Type[] { typeof(CardPool), typeof(ClassData), typeof(CollectableRarity), typeof(CardPoolHelper.RarityCondition), typeof(bool) })]
    class AddCustomCardToPoolPatch
    {
        static void Postfix(ref List<CardData> __result, ref CardPool cardPool, ClassData classData, CollectableRarity paramRarity, CardPoolHelper.RarityCondition rarityCondition, bool testRarityCondition)
        {
            List<CardData> customCardsToAddToPool = CustomCardPoolManager.GetCardsForPoolSatisfyingConstraints(cardPool.name, classData, paramRarity, rarityCondition, testRarityCondition);
            __result.AddRange(customCardsToAddToPool);
        }
    }
}
