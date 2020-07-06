using System;
using System.Collections.Generic;
using MonsterTrainModdingAPI.Managers;
using System.Linq;
using System.Text;

namespace MonsterTrainModdingAPI.Enums.MTTriggers
{
    /// <summary>
    /// Move to Another Class
    /// </summary>
    /// <typeparam name="TExtendedEnum"></typeparam>
    /// <typeparam name="TEnum"></typeparam>
    public abstract class ExtendedEnum<TExtendedEnum, TEnum>
        where TExtendedEnum : ExtendedEnum<TExtendedEnum, TEnum>
        where TEnum : Enum
    {
        protected static Dictionary<int, TExtendedEnum> IntToExtendedEnumMap = new Dictionary<int, TExtendedEnum>();
        protected static Dictionary<string, TExtendedEnum> NameToExtendedEnumMap = new Dictionary<string, TExtendedEnum>();
        protected int ID;
        protected string Name;
        public ExtendedEnum(string Name, int ID)
        {
            this.ID = ID;
            this.Name = Name;
            if (NameToExtendedEnumMap.ContainsKey(this.Name))
            {
                MonsterTrainModdingAPI.API.Log(BepInEx.Logging.LogLevel.Warning, $"Name: {this.Name} Conflict in domain, {typeof(TExtendedEnum).Name}");
            }
            if (IntToExtendedEnumMap.ContainsKey(this.ID))
            {
                MonsterTrainModdingAPI.API.Log(BepInEx.Logging.LogLevel.Warning, $"ID#{this.ID} Conflict between {Name} and {IntToExtendedEnumMap[this.ID].GetName()} in domain, {typeof(TExtendedEnum).Name}");
            }
            NameToExtendedEnumMap[Name] = GetDerivedClass();
            IntToExtendedEnumMap[ID] = GetDerivedClass();
        }
        /// <summary>
        /// A Function that Returns a Reference of the Subclass
        /// </summary>
        /// <returns></returns>
        public abstract TExtendedEnum GetDerivedClass();
        public int GetID() => ID;
        public virtual TEnum GetEnum() => (TEnum)Enum.ToObject(typeof(TEnum), ID);
        public string GetName() => Name;
        public static int[] GetAllIDs() => IntToExtendedEnumMap.Keys.ToArray();
        public static string[] GetAllNames => NameToExtendedEnumMap.Keys.ToArray();
        public static TExtendedEnum GetValueOrDefault(string Key) => NameToExtendedEnumMap.GetValueOrDefault(Key);
        public static TExtendedEnum GetValueOrDefault(int Key) => IntToExtendedEnumMap.GetValueOrDefault(Key);
    }
    public class CardTrigger : ExtendedEnum<CardTrigger, CardTriggerType>
    {
        private static int NumCardTriggers = 576;
        public CardTrigger(string localizationKey, int? ID = null) : base(localizationKey, ID ?? GetNewCardGUID())
        {
            Dictionary<CardTriggerType, string> dict = (Dictionary<CardTriggerType, string>)typeof(CardTriggerTypeMethods).GetField("TriggerToLocalizationExpression", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null);
            dict[this.GetEnum()] = localizationKey;
        }

        public override CardTrigger GetDerivedClass()
        {
            return this;
        }

        public static int GetNewCardGUID()
        {
            NumCardTriggers++;
            return NumCardTriggers;
        }
    }

    public class CharacterTrigger : ExtendedEnum<CharacterTrigger, CharacterTriggerData.Trigger>
    {
        private static int NumCharTriggers = 576;
        public CharacterTrigger(string localizationKey, int? ID = null) : base(localizationKey, ID ?? GetNewCharacterGUID())
        {
            CharacterTriggerData.TriggerToLocalizationExpression[this.GetEnum()] = localizationKey;
        }

        public override CharacterTrigger GetDerivedClass()
        {
            return this;
        }

        public static int GetNewCharacterGUID()
        {
            NumCharTriggers++;
            return NumCharTriggers;
        }
    }
    public class CharacterTriggers
    {
        public static CharacterTrigger MTCharacterTrigger_OnDeath { get; private set; } = new CharacterTrigger("Trigger_OnDeath", 0);
        public static CharacterTrigger MTCharacterTrigger_PostCombat { get; private set; } = new CharacterTrigger("Trigger_PostCombat", 1);
        public static CharacterTrigger MTCharacterTrigger_OnSpawn { get; private set; } = new CharacterTrigger("Trigger_OnSpawn", 2);
        public static CharacterTrigger MTCharacterTrigger_OnAttacking { get; private set; } = new CharacterTrigger("Trigger_OnAttacking", 3);
        public static CharacterTrigger MTCharacterTrigger_OnKill { get; private set; } = new CharacterTrigger("Trigger_OnKill_CharacterTriggerData", 4);
        public static CharacterTrigger MTCharacterTrigger_OnAnyHeroDeathOnFloor { get; private set; } = new CharacterTrigger("Trigger_OnAnyHeroDeathOnFloor", 5);
        public static CharacterTrigger MTCharacterTrigger_OnAnyMonsterDeathOnFloor { get; private set; } = new CharacterTrigger("Trigger_OnAnyMonsterDeathOnFloor", 6);
        public static CharacterTrigger MTCharacterTrigger_OnHeal { get; private set; } = new CharacterTrigger("Trigger_OnHeal", 7);
        public static CharacterTrigger MTCharacterTrigger_OnTeamTurnBegin { get; private set; } = new CharacterTrigger("Trigger_OnTeamTurnBegin", 8);
        public static CharacterTrigger MTCharacterTrigger_OnRevenge { get; private set; } = new CharacterTrigger("Trigger_OnRevenge", 11);
        public static CharacterTrigger MTCharacterTrigger_PreCombat { get; private set; } = new CharacterTrigger("Trigger_PreCombat", 12);
        public static CharacterTrigger MTCharacterTrigger_PostAscension { get; private set; } = new CharacterTrigger("Trigger_PostAscension", 13);
        public static CharacterTrigger MTCharacterTrigger_PostCombatHealing { get; private set; } = new CharacterTrigger("Trigger_PostCombatCharacterAbility", 14);
        public static CharacterTrigger MTCharacterTrigger_OnHit { get; private set; } = new CharacterTrigger("Trigger_OnHit", 15);
        public static CharacterTrigger MTCharacterTrigger_AfterSpawnEnchant { get; private set; } = new CharacterTrigger("Trigger_AfterSpawnEnchant", 16);
        public static CharacterTrigger MTCharacterTrigger_PostDescension { get; private set; } = new CharacterTrigger("Trigger_PostDescension", 17);
        public static CharacterTrigger MTCharacterTrigger_OnAnyUnitDeathOnFloor { get; private set; } = new CharacterTrigger("Trigger_OnAnyUnitDeathOnFloor", 18);
        public static CharacterTrigger MTCharacterTrigger_CardSpellPlayed { get; private set; } = new CharacterTrigger("Trigger_CardSpellPlayed", 19);
        public static CharacterTrigger MTCharacterTrigger_CardMonsterPlayed { get; private set; } = new CharacterTrigger("Trigger_CardMonsterPlayed", 20);
        public static CharacterTrigger MTCharacterTrigger_EndTurnPreHandDiscard { get; private set; } = new CharacterTrigger("Trigger_EndTurnPreHandDiscard", 21);
        public static CharacterTrigger MTCharacterTrigger_OnFeed { get; private set; } = new CharacterTrigger("Trigger_OnFeed", 22);
        public static CharacterTrigger MTCharacterTrigger_OnEaten { get; private set; } = new CharacterTrigger("Trigger_OnEaten", 23);
        public static CharacterTrigger MTCharacterTrigger_OnTurnBegin { get; private set; } = new CharacterTrigger("Trigger_OnTurnBegin", 24);
        public static CharacterTrigger MTCharacterTrigger_OnBurnout { get; private set; } = new CharacterTrigger(String.Empty, 25);
        public static CharacterTrigger MTCharacterTrigger_OnSpawnNotFromCard { get; private set; } = new CharacterTrigger("Trigger_OnSpawn", 26);
        public static CharacterTrigger MTCharacterTrigger_OnUnscaledSpawn { get; private set; } = new CharacterTrigger(String.Empty, 27);
    }

    public class CardTriggers
    {
        public static CardTrigger MTCardTrigger_OnCast { get; private set; } = new CardTrigger("Trigger_OnCast", 0);
        public static CardTrigger MTCardTrigger_OnKill { get; private set; } = new CardTrigger("Trigger_OnKill_CardTriggerType", 1);
        public static CardTrigger MTCardTrigger_OnDiscard { get; private set; } = new CardTrigger("Trigger_OnDiscard", 2);
        public static CardTrigger MTCardTrigger_OnMonsterDeath { get; private set; } = new CardTrigger("Trigger_OnMonsterDeath", 3);
        public static CardTrigger MTCardTrigger_OnAnyMonsterDeathOnFloor { get; private set; } = new CardTrigger("Trigger_OnAnyMonsterDeathOnFloor", 4);
        public static CardTrigger MTCardTrigger_OnAnyHeroDeathOnFloor { get; private set; } = new CardTrigger("Trigger_OnAnyHeroDeathOnFloor", 5);
        public static CardTrigger MTCardTrigger_OnHealed { get; private set; } = new CardTrigger("Trigger_OnHealed", 6);
        public static CardTrigger MTCardTrigger_OnPlayerDamageTaken { get; private set; } = new CardTrigger("Trigger_OnPlayerDamageTaken", 7);
        public static CardTrigger MTCardTrigger_OnAnyUnitDeathOnFloor { get; private set; } = new CardTrigger("Trigger_OnAnyUnitDeathOnFloor", 8);
        public static CardTrigger MTCardTrigger_OnTreasure { get; private set; } = new CardTrigger("Trigger_OnTreasure", 9);
        public static CardTrigger MTCardTrigger_OnUnplayed { get; private set; } = new CardTrigger("Trigger_OnUnplayed", 10);
        public static CardTrigger MTCardTrigger_OnFeed { get; private set; } = new CardTrigger("Trigger_OnFeed", 11);
    }
}

