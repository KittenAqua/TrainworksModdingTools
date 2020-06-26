using System;
using System.Collections.Generic;
using MonsterTrainModdingAPI.Managers;
using System.Text;

namespace MonsterTrainModdingAPI.Enums.MTTriggers
{
    /// <summary>
    /// An Interface Used to Represent an In Game Trigger
    /// </summary>
    public interface IMTCharacterTrigger
    {
        int ID { get; }
    }
    /// <summary>
    /// An Abstract Class used to Automatically Construct a working Character Trigger
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class MTCharacterTrigger<T> : IMTCharacterTrigger where T:MTCharacterTrigger<T>
    {
        public static int InternalID { get; private set; } = CustomTriggerManager.GetNewGUID<T>();
        public int ID { get { return InternalID; } }
    }

    public class MTCharacterTrigger_OnDeath : IMTCharacterTrigger { public int ID => 0; }
    public class MTCharacterTrigger_PostCombat : IMTCharacterTrigger { public int ID => 1; }
    public class MTCharacterTrigger_OnSpawn : IMTCharacterTrigger { public int ID => 2; }
    public class MTCharacterTrigger_OnAttacking : IMTCharacterTrigger { public int ID => 3; }
    public class MTCharacterTrigger_OnKill : IMTCharacterTrigger { public int ID => 4; }
    public class MTCharacterTrigger_OnAnyHeroDeathOnFloor : IMTCharacterTrigger { public int ID => 5; }
    public class MTCharacterTrigger_OnAnyMonsterDeathOnFloor : IMTCharacterTrigger { public int ID => 6; }
    public class MTCharacterTrigger_OnHeal : IMTCharacterTrigger { public int ID => 7; }
    public class MTCharacterTrigger_OnTeamTurnBegin : IMTCharacterTrigger { public int ID => 8; }
    public class MTCharacterTrigger_OnRevenge : IMTCharacterTrigger { public int ID => 11; }
    public class MTCharacterTrigger_PreCombat : IMTCharacterTrigger { public int ID => 12; }
    public class MTCharacterTrigger_PostAscension : IMTCharacterTrigger { public int ID => 13; }
    public class MTCharacterTrigger_PostCombatHealing : IMTCharacterTrigger { public int ID => 14; }
    public class MTCharacterTrigger_OnHit : IMTCharacterTrigger { public int ID => 15; }
    public class MTCharacterTrigger_AfterSpawnEnchant : IMTCharacterTrigger { public int ID => 16; }
    public class MTCharacterTrigger_PostDescension : IMTCharacterTrigger { public int ID => 17; }
    public class MTCharacterTrigger_OnAnyUnitDeathOnFloor : IMTCharacterTrigger { public int ID => 18; }
    public class MTCharacterTrigger_CardSpellPlayed : IMTCharacterTrigger { public int ID => 19; }
    public class MTCharacterTrigger_CardMonsterPlayed : IMTCharacterTrigger { public int ID => 20; }
    public class MTCharacterTrigger_EndTurnPreHandDiscard : IMTCharacterTrigger { public int ID => 21; }
    public class MTCharacterTrigger_OnFeed : IMTCharacterTrigger { public int ID => 22; }
    public class MTCharacterTrigger_OnEaten : IMTCharacterTrigger { public int ID => 23; }
    public class MTCharacterTrigger_OnTurnBegin : IMTCharacterTrigger { public int ID => 24; }
    public class MTCharacterTrigger_OnBurnout : IMTCharacterTrigger { public int ID => 25; }
    public class MTCharacterTrigger_OnSpawnNotFromCard : IMTCharacterTrigger { public int ID => 26; }
    public class MTCharacterTrigger_OnUnscaledSpawn : IMTCharacterTrigger { public int ID => 27; }
}
