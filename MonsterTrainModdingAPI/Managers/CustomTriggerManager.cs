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
        /// Gets a New GUID
        /// </summary>
        /// <returns></returns>
        public static int GetNewGUID()
        {
            NumTriggers++;
            return NumTriggers;
        }
        /// <summary>
        /// Registers a Localization String
        /// </summary>
        /// <typeparam name="T">The Trigger Type used to register for</typeparam>
        /// <returns></returns>
        public static string RegisterLocalizationString<T>(string RegisterName = "") where T:IMTCharacterTrigger
        {
            if(RegisterName == "") RegisterName = "Trigger_" + typeof(T).Name;
            CharacterTriggerData.TriggerToLocalizationExpression[GetTrigger(typeof(T))] = RegisterName;
            return RegisterName;
        }
        /// <summary>
        /// Returns a Trigger by Integer ID
        /// </summary>
        /// <param name="ID">Integer to cast to Trigger</param>
        /// <returns></returns>
        public static CharacterTriggerData.Trigger GetTrigger(int ID)
        {
            return (CharacterTriggerData.Trigger)ID;
        }
        /// <summary>
        /// Returns a Trigger by an IMT reference
        /// </summary>
        /// <param name="data">Data to get Integer ID</param>
        /// <returns></returns>
        public static CharacterTriggerData.Trigger GetTrigger(IMTCharacterTrigger data)
        {
            return GetTrigger(data.ID);
        }
        /// <summary>
        /// Returns a Trigger by Type
        /// </summary>
        /// <param name="type">Type to get the ID from</param>
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
        /// Returns a Localization Key by Type
        /// </summary>
        /// <param name="type">Type to get the Localization Key from</param>
        /// <returns></returns>
        public static string GetLocalizationKey(Type type)
        {
            if (typeof(IMTCharacterTrigger).IsAssignableFrom(type))
            {
                var trigger = (IMTCharacterTrigger)Activator.CreateInstance(type);
                return trigger.LocalizationKey;
            }
            return "";
        }
        /// <summary>
        /// Queues a Trigger
        /// </summary>
        /// <typeparam name="T">Type of Trigger to Queue</typeparam>
        /// <param name="character">Character to Queue the Trigger On</param>
        /// <param name="canAttackOrHeal">Whether the Character being triggered can Attack or be Healed</param>
        /// <param name="canFireTriggers">Whether the trigger can currently be fired</param>
        /// <param name="fireTriggersData">Additional Parameters for controlling how the trigger is fired</param>
        /// <param name="triggerCount">Number of Times to Trigger</param>
        /// <returns>Returns Whether it succeeded at Queuing a trigger internally</returns>
        public static void QueueTrigger<T>(CharacterState character, bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1) where T : IMTCharacterTrigger
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
            }
        }
        /// <summary>
        /// Queues a Trigger
        /// </summary>
        /// <typeparam name="T">Type of Trigger to Queue</typeparam>
        /// <param name="characters">Characters to Queue trigger on</param>
        /// <param name="canAttackOrHeal">Whether the Character being triggered can Attack or be Healed</param>
        /// <param name="canFireTriggers">Whether the trigger can currently be fired</param>
        /// <param name="fireTriggersData">Additional Parameters for controlling how the trigger is fired</param>
        /// <param name="triggerCount">Number of Times to Trigger</param>
        /// <returns></returns>
        public static void QueueTrigger<T>(CharacterState[] characters, bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1) where T : IMTCharacterTrigger
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
            }
        }
        /// <summary>
        /// Queues a Trigger
        /// </summary>
        /// <typeparam name="T">Type of Trigger to Queue</typeparam>
        /// <typeparam name="Manager">Type of Manager to Queue and Run Triggers to</typeparam>
        /// <param name="canAttackOrHeal">Whether the Character being triggered can Attack or be Healed</param>
        /// <param name="canFireTriggers">Whether the trigger can currently be fired</param>
        /// <param name="fireTriggersData">Additional Parameters for controlling how the trigger is fired</param>
        /// <param name="triggerCount">Number of Times to Trigger</param>
        /// <returns></returns>
        public static void QueueTrigger<T, Manager>(bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1) where T : IMTCharacterTrigger where Manager : ICharacterManager, IProvider
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
            }
        }
        /// <summary>
        /// Queues and Runs a Trigger
        /// </summary>
        /// <typeparam name="T">Type of Trigger to Queue and Run</typeparam>
        /// <param name="character">Character to Run the Trigger On</param>
        /// <param name="canAttackOrHeal">Whether the Character being triggered can Attack or be Healed</param>
        /// <param name="canFireTriggers">Whether the trigger can currently be fired</param>
        /// <param name="fireTriggersData">Additional Parameters for controlling how the trigger is fired</param>
        /// <param name="triggerCount">Number of Times to Trigger</param>
        /// <returns></returns>
        public static IEnumerator QueueAndRunTrigger<T>(CharacterState character, bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1) where T : IMTCharacterTrigger
        {
            QueueTrigger<T>(character, canAttackOrHeal, canFireTriggers, fireTriggersData, triggerCount);
            yield return RunTriggerQueueRemote();
        }
        /// <summary>
        /// Queues and Runs a Trigger
        /// </summary>
        /// <typeparam name="T">Type of Trigger to Queue and Run</typeparam>
        /// <param name="characters">Characters to Run the Trigger On</param>
        /// <param name="canAttackOrHeal">Whether the Character being triggered can Attack or be Healed</param>
        /// <param name="canFireTriggers">Whether the trigger can currently be fired</param>
        /// <param name="fireTriggersData">Additional Parameters for controlling how the trigger is fired</param>
        /// <param name="triggerCount">Number of Times to Trigger</param>
        /// <returns></returns>
        public static IEnumerator QueueAndRunTrigger<T>(CharacterState[] characters, bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1) where T : IMTCharacterTrigger
        {
            QueueTrigger<T>(characters, canAttackOrHeal, canFireTriggers, fireTriggersData, triggerCount);
            yield return RunTriggerQueueRemote();
        }
        /// <summary>
        /// Queues and Runs a Trigger
        /// </summary>
        /// <typeparam name="T">Type of Trigger to Queue and Run</typeparam>
        /// <typeparam name="Manager">Type of Manager to Queue and Run Triggers to</typeparam>
        /// <param name="canAttackOrHeal">Whether the Character being triggered can Attack or be Healed</param>
        /// <param name="canFireTriggers">Whether the trigger can currently be fired</param>
        /// <param name="fireTriggersData">Additional Parameters for controlling how the trigger is fired</param>
        /// <param name="triggerCount">Number of Times to Trigger</param>
        /// <returns></returns>
        public static IEnumerator QueueAndRunTrigger<T, Manager>(bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1) where T : IMTCharacterTrigger where Manager : IProvider, ICharacterManager
        {
            QueueTrigger<T, Manager>(canAttackOrHeal, canFireTriggers, fireTriggersData, triggerCount);
            yield return RunTriggerQueueRemote();
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
