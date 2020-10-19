using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Trainworks.Enums.MTTriggers;
using HarmonyLib;
using System.Linq;
using UnityEngine;
using Unity;

namespace Trainworks.Managers
{
    public class CustomTriggerManager
    {
        /// <summary>
        /// Dictionary used for conversion of CharacterTriggers to CardTriggers
        /// </summary>
        private static Dictionary<CharacterTriggerData.Trigger, CardTriggerType> CharToCardTriggerDict = new Dictionary<CharacterTriggerData.Trigger, CardTriggerType>();

        /// <summary>
        /// Dictionary used for conversion of CardTriggers to CharacterTriggers
        /// </summary>
        private static Dictionary<CardTriggerType, CharacterTriggerData.Trigger> CardToCharTriggerDict = new Dictionary<CardTriggerType, CharacterTriggerData.Trigger>();
        
        /// <summary>
        /// Used to queue a Character Trigger to be automatically executed during key phases of combat. If you need to ensure that your trigger is executed at other times, please use QueueAndRunTrigger with a coroutine.
        /// </summary>
        /// <param name="charTrigger">CharacterTrigger to be queued</param>
        /// <param name="character">Character to Queue the Trigger On</param>
        /// <param name="canAttackOrHeal">Whether the Character being triggered can Attack or be Healed</param>
        /// <param name="canFireTriggers">Whether the trigger can currently be fired</param>
        /// <param name="fireTriggersData">Additional Parameters for controlling how the trigger is fired</param>
        /// <param name="triggerCount">Number of Times to Trigger</param>
        /// <returns>Returns Whether it succeeded at Queuing a trigger internally</returns>
        public static void QueueTrigger(CharacterTrigger charTrigger, CharacterState character, bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1)
        {
            if (ProviderManager.TryGetProvider<CombatManager>(out CombatManager combatManager))
            {
                combatManager.QueueTrigger(
                    character,
                    charTrigger.GetEnum(),
                    canAttackOrHeal,
                    canFireTriggers,
                    fireTriggersData,
                    triggerCount
                    );
            }
        }

        /// <summary>
        /// Used to queue a Character Trigger to be automatically executed during key phases of combat. If you need to ensure that your trigger is executed at other times, please use QueueAndRunTrigger with a coroutine.
        /// </summary>
        /// <param name="charTrigger">CharacterTrigger to be queued</param>
        /// <param name="characters">Characters to Queue trigger on</param>
        /// <param name="canAttackOrHeal">Whether the Character being triggered can Attack or be Healed</param>
        /// <param name="canFireTriggers">Whether the trigger can currently be fired</param>
        /// <param name="fireTriggersData">Additional Parameters for controlling how the trigger is fired</param>
        /// <param name="triggerCount">Number of Times to Trigger</param>
        /// <returns></returns>
        public static void QueueTrigger(CharacterTrigger charTrigger, CharacterState[] characters, bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1)
        {
            if (ProviderManager.TryGetProvider<CombatManager>(out CombatManager combatManager))
            {
                foreach (CharacterState character in characters)
                {
                    combatManager.QueueTrigger(
                        character,
                        charTrigger.GetEnum(),
                        canAttackOrHeal,
                        canFireTriggers,
                        fireTriggersData,
                        triggerCount
                        );
                }
            }
        }

        /// <summary>
        /// Used to queue a Character Trigger to be automatically executed during key phases of combat. If you need to ensure that your trigger is executed at other times, please use QueueAndRunTrigger with a coroutine.
        /// </summary>
        /// <typeparam name="Manager">Type of Manager to Queue and Run Triggers to</typeparam>
        /// <param name="charTrigger">CharacterTrigger to be queued</param>
        /// <param name="canAttackOrHeal">Whether the Character being triggered can Attack or be Healed</param>
        /// <param name="canFireTriggers">Whether the trigger can currently be fired</param>
        /// <param name="fireTriggersData">Additional Parameters for controlling how the trigger is fired</param>
        /// <param name="triggerCount">Number of Times to Trigger</param>
        /// <returns></returns>
        public static void QueueTrigger<Manager>(CharacterTrigger charTrigger, bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1) where Manager : ICharacterManager, IProvider
        {
            if (ProviderManager.TryGetProvider<CombatManager>(out CombatManager combatManager) && ProviderManager.TryGetProvider<Manager>(out Manager characterManager))
            {
                for (int i = 0; i < characterManager.GetNumCharacters(); i++)
                {
                    combatManager.QueueTrigger(
                        characterManager.GetCharacter(i),
                        charTrigger.GetEnum(),
                        canAttackOrHeal,
                        canFireTriggers,
                        fireTriggersData,
                        triggerCount
                        );
                }
            }
        }

        /// <summary>
        /// Used to Queue a Character Trigger then runs the trigger queue as though it was a key phase of combat.
        /// </summary>
        /// <param name="charTrigger">CharacterTrigger to be queued</param>
        /// <param name="character">Character to Run the Trigger On</param>
        /// <param name="canAttackOrHeal">Whether the Character being triggered can Attack or be Healed</param>
        /// <param name="canFireTriggers">Whether the trigger can currently be fired</param>
        /// <param name="fireTriggersData">Additional Parameters for controlling how the trigger is fired</param>
        /// <param name="triggerCount">Number of Times to Trigger</param>
        /// <returns></returns>
        public static void QueueAndRunTrigger(CharacterTrigger charTrigger, CharacterState character, bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1)
        {
            QueueTrigger(charTrigger, character, canAttackOrHeal, canFireTriggers, fireTriggersData, triggerCount);
            RunTriggerQueueRemote();
        }

        /// <summary>
        /// Used to Queue a Character Trigger then runs the trigger queue as though it was a key phase of combat.Queues and Runs a Trigger
        /// </summary>
        /// <param name="charTrigger">CharacterTrigger to be queued</param>
        /// <param name="characters">Characters to Run the Trigger On</param>
        /// <param name="canAttackOrHeal">Whether the Character being triggered can Attack or be Healed</param>
        /// <param name="canFireTriggers">Whether the trigger can currently be fired</param>
        /// <param name="fireTriggersData">Additional Parameters for controlling how the trigger is fired</param>
        /// <param name="triggerCount">Number of Times to Trigger</param>
        /// <returns></returns>
        public static void QueueAndRunTrigger(CharacterTrigger charTrigger, CharacterState[] characters, bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1)
        {
            QueueTrigger(charTrigger, characters, canAttackOrHeal, canFireTriggers, fireTriggersData, triggerCount);
            RunTriggerQueueRemote();
        }

        /// <summary>
        /// Used to Queue a Character Trigger then runs the trigger queue as though it was a key phase of combat.Queues and Runs a Trigger
        /// </summary>
        /// <typeparam name="Manager">Type of Manager to Queue and Run Triggers to</typeparam>
        /// <param name="charTrigger">CharacterTrigger to be queued</param>
        /// <param name="canAttackOrHeal">Whether the Character being triggered can Attack or be Healed</param>
        /// <param name="canFireTriggers">Whether the trigger can currently be fired</param>
        /// <param name="fireTriggersData">Additional Parameters for controlling how the trigger is fired</param>
        /// <param name="triggerCount">Number of Times to Trigger</param>
        /// <returns></returns>
        public static void QueueAndRunTrigger<Manager>(CharacterTrigger charTrigger, bool canAttackOrHeal = true, bool canFireTriggers = true, CharacterState.FireTriggersData fireTriggersData = null, int triggerCount = 1) where Manager : IProvider, ICharacterManager
        {
            QueueTrigger<Manager>(charTrigger, canAttackOrHeal, canFireTriggers, fireTriggersData, triggerCount);
            RunTriggerQueueRemote();
        }

        /// <summary>
        /// Remotely Runs the Trigger Queue of the CombatManager
        /// </summary>
        /// <returns></returns>
        public static void RunTriggerQueueRemote()
        {
            if (ProviderManager.TryGetProvider<CombatManager>(out CombatManager combatManager))
            {
                combatManager.StartCoroutine(combatManager.RunTriggerQueue());
            }
        }

        /// <summary>
        /// A general function used to Apply a Card Trigger in different ways, dependant on parameters 
        /// </summary>
        /// <param name="cardTrigger">CardTrigger to be Applied</param>
        /// <param name="playedCard">Card to apply triggers to</param>
        /// <param name="fireAllMonsterTriggersInRoom">Whether Apply Card Triggers should fire on monster's Instead, requires Trigger to have an associated character trigger</param>
        /// <param name="roomIndex">Room to fire triggers in, -1 defaults to selected room</param>
        /// <param name="ignoreDeadInTargeting">Whether effects applied by the trigger should ignore dead in targetting</param>
        /// <param name="triggeredCharacter">Character used to determine how many times Card Trigger should be applied</param>
        /// <param name="cardTriggerFiredCallback">Action to take after applying trigger</param>
        /// <returns></returns>
        public static void ApplyCardTriggers(CardTrigger cardTrigger, CardState playedCard, bool fireAllMonsterTriggersInRoom = false, int roomIndex = -1, bool ignoreDeadInTargeting = true, CharacterState triggeredCharacter = null, Action cardTriggerFiredCallback = null)
        {
            Trainworks.Log(BepInEx.Logging.LogLevel.Info, $"Applying {cardTrigger.Name}");
            if (ProviderManager.TryGetProvider<CombatManager>(out CombatManager combatManager))
            {
                combatManager.StartCoroutine(
                    combatManager.ApplyCardTriggers(
                    cardTrigger.GetEnum(),
                    playedCard,
                    fireAllMonsterTriggersInRoom,
                    roomIndex,
                    ignoreDeadInTargeting,
                    triggeredCharacter,
                    cardTriggerFiredCallback
                    )
                );

            }
        }

        /// <summary>
        /// A function used to fire a card trigger, causing the effects of the corresponding trigger on the playedCard to be fired.
        /// </summary>
        /// <param name="cardTrigger">CardTrigger to be Fired</param>
        /// <param name="playedCard">Card to Fire Trigger on</param>
        /// <param name="roomIndex">Room to fire trigger in, -1 is current room</param>
        /// <param name="ignoreDeadInTargeting">Whether effect should ignore dead in targeting</param>
        /// <param name="triggeredCharacter">Character used for applying effects</param>
        /// <param name="fireCount">how many times the trigger fires</param>
        /// <param name="cardTriggerFiredCallback">Action to call after function is called</param>
        /// <returns></returns>
        public static void FireCardTriggers(CardTrigger cardTrigger, CardState playedCard, int roomIndex = -1, bool ignoreDeadInTargeting = true, CharacterState triggeredCharacter = null, int fireCount = 1, Action cardTriggerFiredCallback = null)
        {
            if (ProviderManager.TryGetProvider<CombatManager>(out CombatManager combatManager))
            {
                combatManager.StartCoroutine(
                    (IEnumerator)AccessTools.Method(
                            typeof(CombatManager),
                            "FireCardTriggers"
                        )
                        .Invoke(combatManager,
                            new object[7]
                            {
                                cardTrigger.GetEnum(),
                                playedCard,
                                roomIndex,
                                ignoreDeadInTargeting,
                                triggeredCharacter,
                                fireCount,
                                cardTriggerFiredCallback
                            }
                        )
                    );
            }
        }

        /// <summary>
        /// Associates two triggers with eachother allowing MT to cast from one trigger to another
        /// </summary>
        /// <param name="cardTrigger">CardTrigger to be Associated</param>
        /// <param name="characterTrigger">CharacterTrigger to be Associated</param>
        public static void AssociateTriggers(CardTrigger cardTrigger, CharacterTrigger characterTrigger)
        {
            CharToCardTriggerDict[characterTrigger.GetEnum()] = cardTrigger.GetEnum();
            CardToCharTriggerDict[cardTrigger.GetEnum()] = characterTrigger.GetEnum();
        }

        /// <summary>
        /// Gets the Associated Card Trigger from a Character Trigger or null
        /// </summary>
        /// <param name="trigger">Trigger to get Associate for</param>
        /// <returns></returns>
        public static CardTriggerType? GetAssociatedCardTrigger(CharacterTriggerData.Trigger trigger)
        {
            if (CharToCardTriggerDict.ContainsKey(trigger))
            {
                return CharToCardTriggerDict[trigger];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the Associated Character Trigger from a Card Trigger or null
        /// </summary>
        /// <param name="trigger">Trigger to get Associate for</param>
        /// <returns></returns>
        public static CharacterTriggerData.Trigger? GetAssociatedCharacterTrigger(CardTriggerType trigger)
        {
            if (CardToCharTriggerDict.ContainsKey(trigger))
            {
                return CardToCharTriggerDict?[trigger];
            }
            else
            {
                return null;
            }
        }
    }
}
