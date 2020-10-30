using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using Trainworks.Enums.MTTriggers;

namespace TrainworksModdingTools.Patches
{
    [HarmonyPatch(typeof(CombatManager), "InitializeBattleTriggerEvents")]
    class InitializeAdditionalBattleTriggerEventsPatch
    {
        static void Prefix(ref bool __state, ref Dictionary<Team.Type, Dictionary<CharacterTriggerData.Trigger, List<CardEffectState>>> useBattleTriggeredEvents)
        {
            __state = useBattleTriggeredEvents == null;
        }
        static void Postfix(ref bool __state, ref Dictionary<Team.Type, Dictionary<CharacterTriggerData.Trigger, List<CardEffectState>>> useBattleTriggeredEvents)
        {
            if (__state)
            {
                var register = CharacterTrigger.GetAllExtendedEnums();
                foreach(var team in useBattleTriggeredEvents.Values)
                {
                    foreach(var registeredEnum in register) 
                    {
                        team.Add(registeredEnum, new List<CardEffectState>());
                    }
                }
            }
        }
    }
}
