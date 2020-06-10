using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI.Traits
{
    /// <summary>
    /// Has no actual gameplay effect, but adds any custom text to the card's description.
    /// </summary>
    public class CardTraitCustomDescription : CardTraitState
    {
        /// <summary>
        /// Fills in the description format string with the appropriate parameters and returns it.
        /// </summary>
        /// <returns>The final description</returns>
        public override string GetCardText()
        {
            var relicManager = (RelicManager)AccessTools.Field(typeof(CardState), "relicManager").GetValue(base.GetCard());
            string baseDesc = base.GetCardTraitData().GetParamStr();
            I2.Loc.LocalizationManager.ApplyLocalizationParams(ref baseDesc, null, new CardEffectLocalizationContext(base.GetCard(), relicManager, CustomCardManager.SaveManager, null));
            return baseDesc;
        }
    }
}
