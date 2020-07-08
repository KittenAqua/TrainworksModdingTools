using HarmonyLib;
using Spine;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonsterTrainModdingAPI.Patches
{
    [HarmonyPatch(typeof(StatusEffectManager), "GetLocalizedCharacterTooltipTextKey")]
    public class SETooltipLocalizationPatch
    {
        static string Postfix(string ret, string statusId)
        {
            if (!StatusEffectManager.StatusIdToLocalizationExpression.ContainsKey(statusId.ToLowerInvariant()))
            {
                return "StatusEffect_" + statusId + "_CharacterTooltipText";
            }
            return ret;
        }
    }

    [HarmonyPatch(typeof(StatusEffectManager), "GetLocalizedName")]
    public class SENameLocalizationPatch
    {
        static string Postfix(string ret, string statusId, int stackCount, bool inBold, bool isStackable, bool inCardBodyText)
        {
            if (!StatusEffectManager.StatusIdToLocalizationExpression.ContainsKey(statusId.ToLowerInvariant()))
            {
                string format;
                if ((stackCount > 1 || inCardBodyText) && isStackable)
                {
                    format = ("StatusEffect_" + statusId + "_Stack_CardText").Localize();
                    format = string.Format(format, stackCount);
                }
                else
                {
                    format = ("StatusEffect_" + statusId + "_CardText").Localize();
                }
                if (inBold)
                {
                    format = "<b>" + format + "</b>";
                }
                return format;
            }
            return ret;
        }
    }

    [HarmonyPatch(typeof(StatusEffectManager), "GetLocalizedCardTooltipTextKey")]
    public class SECardTooltipLocalizationPatch
    {
        static string Postfix(string ret, string statusId)
        {
            if (!StatusEffectManager.StatusIdToLocalizationExpression.ContainsKey(statusId.ToLowerInvariant()))
            {
                return "StatusEffect_" + statusId + "_CardTooltipText";
            }
            return ret;
        }
    }

    [HarmonyPatch(typeof(StatusEffectManager), "GetLocalizedNotificationText")]
    public class SENoticeLocalizationPatch
    {
        static string Postfix(string ret, string statusId)
        {
            if (!StatusEffectManager.StatusIdToLocalizationExpression.ContainsKey(statusId.ToLowerInvariant()))
            {
                return "StatusEffect_" + statusId + "_NotificationText";
            }
            return ret;
        }
    }
}
