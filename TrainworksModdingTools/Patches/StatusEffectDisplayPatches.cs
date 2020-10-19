using HarmonyLib;
using Spine;
using System;
using System.Collections.Generic;
using System.Text;

namespace Trainworks.Patches
{
    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
    [HarmonyPatch(typeof(StatusEffectManager), "GetLocalizedName")]
    public class SENameLocalizationPatch
    {
        static string Postfix(string ret, string statusId, int stackCount, bool inBold, bool showStacks, bool inCardBodyText)
        {
            if (!StatusEffectManager.StatusIdToLocalizationExpression.ContainsKey(statusId.ToLowerInvariant()))
            {
                string format;
                if ((stackCount > 1 || inCardBodyText) && showStacks)
                {
                    format = ("StatusEffect_" + statusId + "_Stack_CardText").Localize();
                    format = string.Format(format, stackCount);
                }
                else
                {
                    format = ("StatusEffect_" + statusId + "_CardText").Localize();
                }

                return format;
            }
            return ret;
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
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

    /// <summary>
    /// 
    /// </summary>
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
