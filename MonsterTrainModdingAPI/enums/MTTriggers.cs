using System;
using System.Collections.Generic;
using MonsterTrainModdingAPI.Managers;
using System.Text;

namespace MonsterTrainModdingAPI.Enums.MTTriggers
{
    /// <summary>
    /// An Interface Used to Represent a Trigger used by Monster Train
    /// </summary>
    public interface IMTTrigger
    {
        int ID { get; }
        string LocalizationKey { get; }
    }
    /// <summary>
    /// An Interface Used to Represent an In Game Card Trigger
    /// </summary>
    public interface IMTCardTrigger : IMTTrigger { }
    /// <summary>
    /// An Interface Used to Represent an In Game Character Trigger
    /// </summary>
    public interface IMTCharacterTrigger : IMTTrigger { }
    /// <summary>
    /// An Abstract Class used to Automatically Construct a working Character Trigger
    /// </summary>
    /// <typeparam name="T">Type Inheriting from MTCharacterTrigger</typeparam>
    public abstract class MTCharacterTrigger<T> : IMTCharacterTrigger where T : MTCharacterTrigger<T>
    {
        private static int InternalID;
        private static string InternalLocalizationKey;
        /// <summary>
        /// The ID of your Character Trigger 
        /// </summary>
        public int ID { get { return InternalID; } }
        /// <summary>
        /// The Localization Key of Your Trigger
        /// </summary>
        public string LocalizationKey { get { return InternalLocalizationKey; } }

        static MTCharacterTrigger()
        {
            InternalID = CustomTriggerManager.GetNewCharacterGUID();
            InternalLocalizationKey = CustomTriggerManager.RegisterCharacterLocalizationString<T>();
        }
    }

    public abstract class MTCardTrigger<T> : IMTCardTrigger where T: MTCardTrigger<T>
    {
        private static int InternalID;
        private static string InternalLocalizationKey;
        /// <summary>
        /// The ID of your Card Trigger 
        /// </summary>
        public int ID { get { return InternalID; } }
        /// <summary>
        /// The Localization Key of Your Trigger
        /// </summary>
        public string LocalizationKey { get { return InternalLocalizationKey; } }
        static MTCardTrigger()
        {
            InternalID = CustomTriggerManager.GetNewCardGUID();
            InternalLocalizationKey = CustomTriggerManager.RegisterCardLocalizationString<T>();
        }
    }

    public class MTCharacterTrigger_OnDeath : IMTCharacterTrigger { public int ID => 0; public string LocalizationKey => "Trigger_OnDeath"; }
    public class MTCharacterTrigger_PostCombat : IMTCharacterTrigger { public int ID => 1; public string LocalizationKey => "Trigger_PostCombat"; }
    public class MTCharacterTrigger_OnSpawn : IMTCharacterTrigger { public int ID => 2; public string LocalizationKey => "Trigger_OnSpawn"; }
    public class MTCharacterTrigger_OnAttacking : IMTCharacterTrigger { public int ID => 3; public string LocalizationKey => "Trigger_OnAttacking"; }
    public class MTCharacterTrigger_OnKill : IMTCharacterTrigger { public int ID => 4; public string LocalizationKey => "Trigger_OnKill_CharacterTriggerData"; }
    public class MTCharacterTrigger_OnAnyHeroDeathOnFloor : IMTCharacterTrigger { public int ID => 5; public string LocalizationKey => "Trigger_OnAnyHeroDeathOnFloor"; }
    public class MTCharacterTrigger_OnAnyMonsterDeathOnFloor : IMTCharacterTrigger { public int ID => 6; public string LocalizationKey => "Trigger_OnAnyMonsterDeathOnFloor"; }
    public class MTCharacterTrigger_OnHeal : IMTCharacterTrigger { public int ID => 7; public string LocalizationKey => "Trigger_OnHeal"; }
    public class MTCharacterTrigger_OnTeamTurnBegin : IMTCharacterTrigger { public int ID => 8; public string LocalizationKey => "Trigger_OnTeamTurnBegin"; }
    public class MTCharacterTrigger_OnRevenge : IMTCharacterTrigger { public int ID => 11; public string LocalizationKey => "Trigger_OnRevenge"; }
    public class MTCharacterTrigger_PreCombat : IMTCharacterTrigger { public int ID => 12; public string LocalizationKey => "Trigger_PreCombat"; }
    public class MTCharacterTrigger_PostAscension : IMTCharacterTrigger { public int ID => 13; public string LocalizationKey => "Trigger_PostAscension"; }
    public class MTCharacterTrigger_PostCombatHealing : IMTCharacterTrigger { public int ID => 14; public string LocalizationKey => "Trigger_PostCombatCharacterAbility"; }
    public class MTCharacterTrigger_OnHit : IMTCharacterTrigger { public int ID => 15; public string LocalizationKey => "Trigger_OnHit"; }
    public class MTCharacterTrigger_AfterSpawnEnchant : IMTCharacterTrigger { public int ID => 16; public string LocalizationKey => "Trigger_AfterSpawnEnchant"; }
    public class MTCharacterTrigger_PostDescension : IMTCharacterTrigger { public int ID => 17; public string LocalizationKey => "Trigger_PostDescension"; }
    public class MTCharacterTrigger_OnAnyUnitDeathOnFloor : IMTCharacterTrigger { public int ID => 18; public string LocalizationKey => "Trigger_OnAnyUnitDeathOnFloor"; }
    public class MTCharacterTrigger_CardSpellPlayed : IMTCharacterTrigger { public int ID => 19; public string LocalizationKey => "Trigger_CardSpellPlayed"; }
    public class MTCharacterTrigger_CardMonsterPlayed : IMTCharacterTrigger { public int ID => 20; public string LocalizationKey => "Trigger_CardMonsterPlayed"; }
    public class MTCharacterTrigger_EndTurnPreHandDiscard : IMTCharacterTrigger { public int ID => 21; public string LocalizationKey => "Trigger_EndTurnPreHandDiscard"; }
    public class MTCharacterTrigger_OnFeed : IMTCharacterTrigger { public int ID => 22; public string LocalizationKey => "Trigger_OnFeed"; }
    public class MTCharacterTrigger_OnEaten : IMTCharacterTrigger { public int ID => 23; public string LocalizationKey => "Trigger_OnEaten"; }
    public class MTCharacterTrigger_OnTurnBegin : IMTCharacterTrigger { public int ID => 24; public string LocalizationKey => "Trigger_OnTurnBegin"; }
    public class MTCharacterTrigger_OnBurnout : IMTCharacterTrigger { public int ID => 25; public string LocalizationKey => String.Empty; }
    public class MTCharacterTrigger_OnSpawnNotFromCard : IMTCharacterTrigger { public int ID => 26; public string LocalizationKey => "Trigger_OnSpawn"; }
    public class MTCharacterTrigger_OnUnscaledSpawn : IMTCharacterTrigger { public int ID => 27; public string LocalizationKey => String.Empty; }



    public class MTCardTrigger_OnCast : IMTCardTrigger { public int ID => 0; public string LocalizationKey => "Trigger_OnCast"; }
    public class MTCardTrigger_OnKill : IMTCardTrigger { public int ID => 1; public string LocalizationKey => "Trigger_OnKill_CardTriggerType"; }
    public class MTCardTrigger_OnDiscard : IMTCardTrigger { public int ID => 2; public string LocalizationKey => "Trigger_OnDiscard"; }
    public class MTCardTrigger_OnMonsterDeath : IMTCardTrigger { public int ID => 3; public string LocalizationKey => "Trigger_OnMonsterDeath"; }
    public class MTCardTrigger_OnAnyMonsterDeathOnFloor : IMTCardTrigger { public int ID => 4; public string LocalizationKey => "Trigger_OnAnyMonsterDeathOnFloor"; }
    public class MTCardTrigger_OnAnyHeroDeathOnFloor : IMTCardTrigger { public int ID => 5; public string LocalizationKey => "Trigger_OnAnyHeroDeathOnFloor"; }
    public class MTCardTrigger_OnHealed : IMTCardTrigger { public int ID => 6; public string LocalizationKey => "Trigger_OnHealed"; }
    public class MTCardTrigger_OnPlayerDamageTaken : IMTCardTrigger { public int ID => 7; public string LocalizationKey => "Trigger_OnPlayerDamageTaken"; }
    public class MTCardTrigger_OnAnyUnitDeathOnFloor : IMTCardTrigger { public int ID => 8; public string LocalizationKey => "Trigger_OnAnyUnitDeathOnFloor"; }
    public class MTCardTrigger_OnTreasure : IMTCardTrigger { public int ID => 9; public string LocalizationKey => "Trigger_OnTreasure"; }
    public class MTCardTrigger_OnUnplayed : IMTCardTrigger { public int ID => 10; public string LocalizationKey => "Trigger_OnUnplayed"; }
    public class MTCardTrigger_OnFeed : IMTCardTrigger { public int ID => 11; public string LocalizationKey => "Trigger_OnCast"; }
}
