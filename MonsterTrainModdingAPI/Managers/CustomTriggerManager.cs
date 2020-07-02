using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MonsterTrainModdingAPI.Enums.MTTriggers;
using HarmonyLib;
using UnityEngine;

namespace MonsterTrainModdingAPI.Managers
{
    public class CustomTriggerManager
    {
        /// <summary>
        /// 576+ reservation makes it extraordinarily unlikely of a conflict with MT
        /// </summary>
        private static int NumCharTriggers = 576;
        private static int NumCardTriggers = 576;
        private static Dictionary<CardTriggerType, string> CardTriggerToNameDict = new Dictionary<CardTriggerType, string>();
        private static Dictionary<CharacterTriggerData.Trigger, CardTriggerType> CharToCardTriggerDict = new Dictionary<CharacterTriggerData.Trigger, CardTriggerType>();
        private static Dictionary<CardTriggerType, CharacterTriggerData.Trigger> CardToCharTriggerDict = new Dictionary<CardTriggerType, CharacterTriggerData.Trigger>();
        /// <summary>
        /// Gets a New Character Trigger GUID
        /// </summary>
        /// <returns></returns>
        public static int GetNewCharacterGUID()
        {
            NumCharTriggers++;
            return NumCharTriggers;
        }
        /// <summary>
        /// Gets a New Card Trigger GUID
        /// </summary>
        /// <returns></returns>
        public static int GetNewCardGUID()
        {
            NumCardTriggers++;
            return NumCardTriggers;
        }
        /// <summary>
        /// Registers a Localization String
        /// </summary>
        /// <typeparam name="T">The Trigger Type used to register for</typeparam>
        /// <param name="RegisterName">the string to register for localization</param>
        /// <returns></returns>
        public static string RegisterCharacterLocalizationString<T>(string RegisterName = "") where T : IMTCharacterTrigger
        {
            if (RegisterName == "") RegisterName = "Trigger_" + typeof(T).Name;
            CharacterTriggerData.TriggerToLocalizationExpression[GetCharacterTrigger(typeof(T))] = RegisterName;
            return RegisterName;
        }
        /// <summary>
        /// Registers a Localization String
        /// </summary>
        /// <typeparam name="T">The Card Trigger Type used to register for</typeparam>
        /// <param name="RegisterName">The string to register for localization</param>
        /// <returns></returns>
        public static string RegisterCardLocalizationString<T>(string RegisterName = "") where T : IMTCardTrigger
        {
            if (RegisterName == "") RegisterName = "Trigger_" + typeof(T).Name;
            Dictionary<CardTriggerType, string> dict = (Dictionary<CardTriggerType, string>)typeof(CardTriggerTypeMethods).GetField("TriggerToLocalizationExpression", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null);
            dict[GetCardTrigger(typeof(T))] = RegisterName;
            CardTriggerToNameDict[GetCardTrigger(typeof(T))] = typeof(T).Name;
            return RegisterName;
        }
        /// <summary>
        /// Returns a Trigger by Integer ID
        /// </summary>
        /// <param name="ID">Integer to cast to Trigger</param>
        /// <returns></returns>
        public static CharacterTriggerData.Trigger GetCharacterTrigger(int ID)
        {
            return (CharacterTriggerData.Trigger)ID;
        }
        /// <summary>
        /// Returns a Trigger by Integer ID
        /// </summary>
        /// <param name="ID">Integer to cast to CardTriggerType</param>
        /// <returns></returns>
        public static CardTriggerType GetCardTrigger(int ID)
        {
            return (CardTriggerType)ID;
        }
        /// <summary>
        /// Returns a Trigger by an IMT reference
        /// </summary>
        /// <param name="data">Data to get Integer ID</param>
        /// <returns></returns>
        public static CharacterTriggerData.Trigger GetCharacterTrigger(IMTCharacterTrigger data)
        {
            return GetCharacterTrigger(data.ID);
        }
        /// <summary>
        /// Returns a CardTriggerType by an IMT reference
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Data to get enum ID</returns>
        public static CardTriggerType GetCardTrigger(IMTCardTrigger data)
        {
            return GetCardTrigger(data.ID);
        }
        /// <summary>
        /// Returns a Trigger by Type
        /// </summary>
        /// <param name="type">Type to get the ID from</param>
        /// <returns></returns>
        public static CharacterTriggerData.Trigger GetCharacterTrigger(Type type)
        {
            if (typeof(IMTCharacterTrigger).IsAssignableFrom(type))
            {
                var trigger = (IMTCharacterTrigger)Activator.CreateInstance(type);
                return GetCharacterTrigger(trigger);
            }
            return CharacterTriggerData.Trigger.OnDeath;
        }
        /// <summary>
        /// Returns a CardTriggerType by Type
        /// </summary>
        /// <param name="type">Type to get the ID from</param>
        /// <returns></returns>
        public static CardTriggerType GetCardTrigger(Type type)
        {
            if (typeof(IMTCharacterTrigger).IsAssignableFrom(type))
            {
                var trigger = (IMTCardTrigger)Activator.CreateInstance(type);
                return GetCardTrigger(trigger);
            }
            return CardTriggerType.OnCast;
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
                    GetCharacterTrigger(typeof(T)),
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
                        GetCharacterTrigger(typeof(T)),
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
                        GetCharacterTrigger(typeof(T)),
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
        /// <summary>
        /// A general function for applying a custom card trigger.
        /// </summary>
        /// <typeparam name="T">Type of Trigger to Apply</typeparam>
        /// <param name="playedCard">Card to apply triggers to</param>
        /// <param name="fireAllMonsterTriggersInRoom">Whether Apply Card Triggers should fire on monster's Instead, requires Trigger to have an associated character trigger</param>
        /// <param name="roomIndex">Room to fire triggers in, -1 defaults to selected room</param>
        /// <param name="ignoreDeadInTargeting">Whether effects applied by the trigger should ignore dead in targetting</param>
        /// <param name="triggeredCharacter">Character used to determine how many times Card Trigger should be applied</param>
        /// <param name="cardTriggerFiredCallback">Action to take after applying trigger</param>
        /// <returns></returns>
        public static IEnumerator ApplyCardTriggers<T>(CardState playedCard, bool fireAllMonsterTriggersInRoom = false, int roomIndex = -1, bool ignoreDeadInTargeting = true, CharacterState triggeredCharacter = null, Action cardTriggerFiredCallback = null) where T : IMTCardTrigger
        {
            if (ProviderManager.TryGetProvider<CombatManager>(out CombatManager combatManager))
            {
                yield return combatManager.ApplyCardTriggers(
                    GetCardTrigger(typeof(T)),
                    playedCard,
                    fireAllMonsterTriggersInRoom,
                    roomIndex,
                    ignoreDeadInTargeting,
                    triggeredCharacter,
                    cardTriggerFiredCallback
                    );
            }
            yield break;
        }
        /// <summary>
        /// Fires a Card Trigger
        /// </summary>
        /// <typeparam name="T">Type of Trigger to Fire</typeparam>
        /// <param name="playedCard">Card to Fire Trigger on</param>
        /// <param name="roomIndex">Room to fire trigger in, -1 is current room</param>
        /// <param name="ignoreDeadInTargeting">Whether effect should ignore dead in targeting</param>
        /// <param name="triggeredCharacter">Character used for applying effects</param>
        /// <param name="fireCount">how many times the trigger fires</param>
        /// <param name="cardTriggerFiredCallback">Action to call after function is called</param>
        /// <returns></returns>
        public static IEnumerator FireCardTriggers<T>(CardState playedCard, int roomIndex = -1, bool ignoreDeadInTargeting = true, CharacterState triggeredCharacter = null, int fireCount = 1, Action cardTriggerFiredCallback = null) where T : IMTCardTrigger
        {
            if (ProviderManager.TryGetProvider<CombatManager>(out CombatManager combatManager))
            {
                yield return AccessTools.Method(typeof(CombatManager), "FireCardTriggers").Invoke(combatManager, new object[7]
                {
                    GetCardTrigger(typeof(T)),
                    playedCard,
                    roomIndex,
                    ignoreDeadInTargeting,
                    triggeredCharacter,
                    fireCount,
                    cardTriggerFiredCallback
                });
            }
            yield break;
        }
        /// <summary>
        /// Associates two triggers with eachother allowing MT to cast from one trigger to another
        /// </summary>
        /// <typeparam name="Card">Card Trigger to Associate</typeparam>
        /// <typeparam name="Char">Character Trigger to Associate</typeparam>
        public static void AssociateTriggers<Card, Char>() where Card:IMTCardTrigger where Char:IMTCharacterTrigger
        {
            CharToCardTriggerDict[GetCharacterTrigger(typeof(Char))] = GetCardTrigger(typeof(Card));
            CardToCharTriggerDict[GetCardTrigger(typeof(Card))] = GetCharacterTrigger(typeof(Char));
        }
        /// <summary>
        /// Gets the Associated Trigger
        /// </summary>
        /// <param name="trigger">Trigger to get Associate for</param>
        /// <returns></returns>
        public static CardTriggerType? GetAssociatedCardTrigger(CharacterTriggerData.Trigger trigger)
        {
            return CharToCardTriggerDict?[trigger];
        }
        /// <summary>
        /// Gets the Associated Trigger
        /// </summary>
        /// <param name="trigger">Trigger to get Associate for</param>
        /// <returns></returns>
        public static CharacterTriggerData.Trigger? GetAssociatedCharacterTrigger(CardTriggerType trigger)
        {
            return CardToCharTriggerDict?[trigger];
        }
        /// <summary>
        /// Converts Trigger to Read-able Name
        /// </summary>
        /// <param name="triggerType">Trigger to Convert</param>
        /// <returns></returns>
        public static string GetTypeName(CardTriggerType triggerType)
        {
            return CardTriggerToNameDict[triggerType];
        }
    }
}
