using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MonsterTrainModdingAPI.Enums.MTTriggers;
using UnityEngine;

namespace MonsterTrainModdingAPI.Managers
{
    public class CustomTriggerManager
    {
        private const int TriggerGUIDOffset = 100;
        private static int NumTriggers = Enum.GetNames(typeof(CharacterTriggerData.Trigger)).Length + TriggerGUIDOffset;

        /// <summary>
        /// Gets a New GUID and sets up a Localization Key of [Trigger_TypeName]
        /// </summary>
        /// <typeparam name="T">The type of the MTTrigger attempting</typeparam>
        /// <returns></returns>
        public static int GetNewGUID<T>() where T : IMTCharacterTrigger
        {
            return GetNewGUID<T>("Trigger_" + typeof(T).Name);
        }
        /// <summary>
        /// Gets a New GUID and sets up a Localization key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="LocalizationKey"></param>
        /// <returns></returns>
        public static int GetNewGUID<T>(string LocalizationKey) where T : IMTCharacterTrigger
        {
            NumTriggers++;
            CharacterTriggerData.TriggerToLocalizationExpression.Add(GetTrigger(NumTriggers), LocalizationKey);
            return NumTriggers;
        }
        /// <summary>
        /// Returns a Trigger by Integer ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static CharacterTriggerData.Trigger GetTrigger(int ID)
        {
            return (CharacterTriggerData.Trigger)ID;
        }
        /// <summary>
        /// Returns a Trigger by an IMT reference
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static CharacterTriggerData.Trigger GetTrigger(IMTCharacterTrigger data)
        {
            return GetTrigger(data.ID);
        }
        /// <summary>
        /// Returns a Trigger by Type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static CharacterTriggerData.Trigger GetTrigger(Type type)
        {
            if (typeof(IMTCharacterTrigger).IsAssignableFrom(type))
            {
                var trigger = (IMTCharacterTrigger)Activator.CreateInstance(type);
                return GetTrigger(trigger);
            }
            return CharacterTriggerData.Trigger.OnDeath;
        }
        /// <summary>
        /// Queues an internal Trigger
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="character"></param>
        /// <param name="canAttackOrHeal"></param>
        /// <param name="canFireTriggers"></param>
        /// <param name="fireTriggersData"></param>
        /// <param name="triggerCount"></param>
        /// <returns>Returns Whether it succeeded at Queuing a trigger internally</returns>
        public static bool QueueTrigger<T>(CharacterState character, bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1) where T : IMTCharacterTrigger
        {
            if (ProviderManager.TryGetProvider<CombatManager>(out CombatManager combatManager))
            {
                combatManager.QueueTrigger(
                    character,
                    GetTrigger(typeof(T)),
                    canAttackOrHeal,
                    canFireTriggers,
                    fireTriggersData,
                    triggerCount
                    );
                return true;
            }
            return false;
        }
        /// <summary>
        /// Queues Internal Triggers to multiple characters
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="characters"></param>
        /// <param name="canAttackOrHeal"></param>
        /// <param name="canFireTriggers"></param>
        /// <param name="fireTriggersData"></param>
        /// <param name="triggerCount"></param>
        /// <returns></returns>
        public static bool QueueTrigger<T>(CharacterState[] characters, bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1) where T : IMTCharacterTrigger
        {
            if (ProviderManager.TryGetProvider<CombatManager>(out CombatManager combatManager))
            {
                foreach (CharacterState character in characters)
                {
                    combatManager.QueueTrigger(
                        character,
                        GetTrigger(typeof(T)),
                        canAttackOrHeal,
                        canFireTriggers,
                        fireTriggersData,
                        triggerCount
                        );
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// Queues Internal Triggers to all characters of a Manager
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Manager"></typeparam>
        /// <param name="canAttackOrHeal"></param>
        /// <param name="canFireTriggers"></param>
        /// <param name="fireTriggersData"></param>
        /// <param name="triggerCount"></param>
        /// <returns></returns>
        public static bool QueueTrigger<T, Manager>(bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1) where T : IMTCharacterTrigger where Manager : ICharacterManager, IProvider
        {
            if (ProviderManager.TryGetProvider<CombatManager>(out CombatManager combatManager) && ProviderManager.TryGetProvider<Manager>(out Manager characterManager))
            {
                for (int i = 0; i < characterManager.GetNumCharacters(); i++)
                {
                    combatManager.QueueTrigger(
                        characterManager.GetCharacter(i),
                        GetTrigger(typeof(T)),
                        canAttackOrHeal,
                        canFireTriggers,
                        fireTriggersData,
                        triggerCount
                        );
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// Remotely Runs the Trigger Queue
        /// </summary>
        /// <returns></returns>
        public static IEnumerator RunTriggerQueueRemote()
        {
            if (ProviderManager.TryGetProvider<CombatManager>(out CombatManager combatManager))
            {
                yield return combatManager.RunTriggerQueue();
            }
            yield break;
        }
    }
}
