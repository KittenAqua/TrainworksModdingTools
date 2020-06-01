using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI.Traits
{
    public class CardTraitCustomDescription : CardTraitState
    {
        public override string GetCardText()
        {
            var relicManager = (RelicManager)AccessTools.Field(typeof(CardState), "relicManager").GetValue(base.GetCard());
            string baseDesc = base.GetCardTraitData().GetParamStr();
            I2.Loc.LocalizationManager.ApplyLocalizationParams(ref baseDesc, null, new CardEffectLocalizationContext(base.GetCard(), relicManager, CustomCardManager.SaveManager, null));
            return baseDesc;
        }
    }
}
