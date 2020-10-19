using HarmonyLib;
using Trainworks.Builders;
using System.Collections.Generic;
using Trainworks.Managers;

namespace Trainworks.Builders
{
    public class CardTriggerEffectDataBuilder
    {
        /// <summary>
        /// Don't set directly; use Trigger instead.
        /// </summary>
        public CardTriggerType trigger;

        /// <summary>
        /// Implicitly sets DescriptionKey if null.
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
        public CardTriggerType Trigger
        {
            get { return this.trigger; }
            set
            {
                this.trigger = value;
                if (this.DescriptionKey == null)
                {
                    this.DescriptionKey = this.trigger + "_CardTriggerEffectData_DescriptionKey";
                }
            }
        }

        /// <summary>
        /// Custom description for the trigger effect.
        /// Overrides DescriptionKey.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Use an existing base game trigger's description key to copy the format of its description.
        /// </summary>
        public string DescriptionKey { get; set; }

        /// <summary>
        /// Append to this list to add new card trigger effects. The Build() method recursively builds all nested builders.
        /// </summary>
        public List<CardTriggerData> CardTriggerEffects { get; set; }

        /// <summary>
        /// Append to this list to add new card effects. The Build() method recursively builds all nested builders.
        /// </summary>
        public List<CardEffectDataBuilder> CardEffectBuilders { get; set; }
        public List<CardEffectData> CardEffects { get; set; }

        public CardTriggerEffectDataBuilder()
        {
            this.CardTriggerEffects = new List<CardTriggerData>();
            this.CardEffectBuilders = new List<CardEffectDataBuilder>();
            this.CardEffects = new List<CardEffectData>();
        }

        /// <summary>
        /// Builds the CardTriggerEffectData represented by this builder's parameters recursively;
        /// i.e. all CardEffectBuilders in cardEffects will also be built.
        /// </summary>
        /// <returns>The newly created CardTriggerEffectData</returns>
        public CardTriggerEffectData Build()
        {
            foreach (var builder in this.CardEffectBuilders)
            {
                this.CardEffects.Add(builder.Build());
            }

            CardTriggerEffectData cardTriggerEffectData = new CardTriggerEffectData();
            AccessTools.Field(typeof(CardTriggerEffectData), "cardEffects").SetValue(cardTriggerEffectData, this.CardEffects);
            AccessTools.Field(typeof(CardTriggerEffectData), "cardTriggerEffects").SetValue(cardTriggerEffectData, this.CardTriggerEffects);
            BuilderUtils.ImportStandardLocalization(this.DescriptionKey, this.Description);
            AccessTools.Field(typeof(CardTriggerEffectData), "descriptionKey").SetValue(cardTriggerEffectData, this.DescriptionKey);
            AccessTools.Field(typeof(CardTriggerEffectData), "trigger").SetValue(cardTriggerEffectData, this.Trigger);

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

            CardTriggerEffects.Add(trigger);
            return trigger;
        }
    }
}
