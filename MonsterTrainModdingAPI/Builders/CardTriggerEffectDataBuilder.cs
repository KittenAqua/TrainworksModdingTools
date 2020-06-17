using HarmonyLib;
using MonsterTrainModdingAPI.Builders;
using System.Collections.Generic;

namespace MonsterTrainModdingAPI.Builders
{
    public class CardTriggerEffectDataBuilder
    {
        /// <summary>
        /// Any of the below CardTriggerType can be used to proc the listed effects.
        /// <list type="bullet">
        /// <item><description>OnCast</description></item>
        /// <item><description>OnKill</description></item>
        /// <item><description>OnDiscard</description></item>
        /// <item><description>OnMonsterDeath</description></item>
        /// <item><description>OnAnyMonsterDeathOnFloor</description></item>
        /// <item><description>OnAnyHeroDeathOnFloor</description></item>
        /// <item><description>OnHealed</description></item>
        /// <item><description>OnPlayerDamageTaken</description></item>
        /// <item><description>OnAnyUnitDeathOnFloor</description></item>
        /// <item><description>OnTreasure</description></item>
        /// <item><description>OnUnplayed</description></item>
        /// <item><description>OnFeed</description></item>
        /// </list>
        /// </summary>
        public CardTriggerType trigger { get; set; }

        /// <summary>
        /// Use an existing base game trigger's description key to copy the format of its description.
        /// </summary>
        public string descriptionKey { get; set; }

        /// <summary>
        /// Append to this list to add new card trigger effects. The Build() method recursively builds all nested builders.
        /// </summary>
        public List<CardTriggerData> cardTriggers { get; set; }

        /// <summary>
        /// Append to this list to add new card effects. The Build() method recursively builds all nested builders.
        /// </summary>
        public List<CardEffectDataBuilder> EffectBuilders { get; set; }
        public List<CardEffectData> Effects { get; set; }

        public CardTriggerEffectDataBuilder()
        {
            this.cardTriggers = new List<CardTriggerData>();
            this.EffectBuilders = new List<CardEffectDataBuilder>();
            this.Effects = new List<CardEffectData>();
        }

        /// <summary>
        /// Builds the CardTriggerEffectData represented by this builder's parameters recursively;
        /// i.e. all CardEffectBuilders in cardEffects will also be built.
        /// </summary>
        /// <returns>The newly created CardTriggerEffectData</returns>
        public CardTriggerEffectData Build()
        {
            foreach (var builder in this.EffectBuilders)
            {
                this.Effects.Add(builder.Build());
            }

            CardTriggerEffectData cardTriggerEffectData = new CardTriggerEffectData();
            AccessTools.Field(typeof(CardTriggerEffectData), "trigger").SetValue(cardTriggerEffectData, this.trigger);
            AccessTools.Field(typeof(CardTriggerEffectData), "descriptionKey").SetValue(cardTriggerEffectData, this.descriptionKey);
            AccessTools.Field(typeof(CardTriggerEffectData), "cardTriggerEffects").SetValue(cardTriggerEffectData, this.cardTriggers);
            AccessTools.Field(typeof(CardTriggerEffectData), "cardEffects").SetValue(cardTriggerEffectData, this.Effects);

            return cardTriggerEffectData;
        }

        /// <summary>
        /// Adds a new CardTrigger to the list.
        /// </summary>
        /// <param name="persistenceMode">SingleRun, or SingleBattle</param>
        /// <param name="cardTriggerEffect"></param>
        /// <param name="buffEffectType"></param>
        /// <param name="paramInt"></param>
        /// <returns></returns>
        public CardTriggerData AddCardTrigger(PersistenceMode persistenceMode, string cardTriggerEffect, string buffEffectType, int paramInt)
        {
            CardTriggerData trigger = new CardTriggerData();

            trigger.persistenceMode = persistenceMode;
            trigger.cardTriggerEffect = cardTriggerEffect;
            trigger.buffEffectType = buffEffectType;
            trigger.paramInt = paramInt;

            cardTriggers.Add(trigger);
            return trigger;
        }
    }
}
