using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using HarmonyLib;
using ShinyShoe.Logging;
using Trainworks.Managers;

namespace Trainworks.Patches
{
    /// <summary>
    /// 
    /// </summary>
    [HarmonyPatch(typeof(CardTriggerTypeMethods), "GetAssociatedCardTrigger")]
    class GetAssociatedCustomCardTriggersPatch
    {
        static void Postfix(ref CardTriggerType? __result, ref CharacterTriggerData.Trigger charTrigger)
        {
            if (__result == null)
            {
                __result = CustomTriggerManager.GetAssociatedCardTrigger(charTrigger);
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    [HarmonyPatch(typeof(CardTriggerTypeMethods), "GetAssociatedCharacterTrigger")]
    class GetAssociatedCustomCharacterTriggersPatch
    {
        static void Postfix(ref CharacterTriggerData.Trigger? __result, ref CardTriggerType cardTrigger)
        {
            if (__result == null)
            {
                __result = CustomTriggerManager.GetAssociatedCharacterTrigger(cardTrigger);
            }
        }
    }
    [HarmonyPatch(typeof(CombatManager), "VerifyTriggerQueueEmpty")]
    class AdditionalInformationFromVerifyTriggerQueueEmptyPatch
    {
        static void Postfix(ref CombatManager __instance)
        {
            Queue<CombatManager.TriggerQueueData> triggerQueue = (Queue<CombatManager.TriggerQueueData>)AccessTools.PropertyGetter(typeof(CombatManager), "TriggerQueue").Invoke(__instance, null);
            if (!__instance.IsRunningTriggerQueue)
            {
                foreach (CombatManager.TriggerQueueData data in triggerQueue.ToList())
                {
                    Trainworks.Log(BepInEx.Logging.LogLevel.Error, $"Trigger Not Enqued, will be cleared: {data.trigger}");
                }
            }
        }
    }
}
