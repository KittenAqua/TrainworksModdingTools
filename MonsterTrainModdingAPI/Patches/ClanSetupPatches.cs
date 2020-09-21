using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace MonsterTrainModdingAPI.Patches
{
    class ClanSetupPatches
    {
        /// <summary>
        /// Identifies the necessary card frame for the class and installs it through ridiculous means.
        /// </summary>
        [HarmonyPatch(typeof(CardFrameUI), "SetUpFrame")]
        public class ClanCardFramePatch
        {
            static void Postfix(ref CardFrameUI __instance, CardState cardState, List<AbstractSpriteSelector> ___spriteSelectors)
            {
                try
                {
                    if (cardState.GetLinkedClassID() == null) { return; }
                    List<Sprite> cardFrame;
                    if (CustomClassManager.CustomClassFrame.TryGetValue(cardState.GetLinkedClassID(), out cardFrame))
                    {
                        foreach (AbstractSpriteSelector spriteSelector in ___spriteSelectors)
                        {
                            switch (spriteSelector)
                            {
                                case ClassSpriteSelector classSpriteSelector:

                                    foreach (var image in classSpriteSelector.gameObject.GetComponents<Image>())
                                    {
                                        image.sprite = cardState.GetCardType() == CardType.Monster ? cardFrame[0] : cardFrame[1];
                                    }
                                    continue;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    API.Log(BepInEx.Logging.LogLevel.Error, "TryGetValue is a dumb function.");
                }
            }
        }

        /// <summary>
        /// This patch adds in the custom icon for a clan. We could theoretically add these to VictoryUI's ClassIconMapping list, which seems better in theory. 
        /// In practice, we have no way to guarantee the existence of VictoryUI in the scene at the time of Class instantiation, and no way to serialize the class mapping in advance of 
        /// </summary>
        [HarmonyPatch(typeof(RewardItemUI), "TryOverrideDraftIcon")]
        public class CustomClanRewardDraftIcon
        {
            static void Prefix(RewardItemUI __instance, ref Sprite mainClassIcon, ref Sprite subClassIcon)
            {
                DraftRewardData draftRewardData;
                if ((object)(draftRewardData = (__instance.rewardState?.RewardData as DraftRewardData)) != null)
                {
                    if (draftRewardData.ClassType == RunState.ClassType.MainClass)
                    {
                        string mainClass = CustomClassManager.SaveManager.GetMainClass().GetID();
                        if (CustomClassManager.CustomClassDraftIcons.ContainsKey(mainClass))
                            CustomClassManager.CustomClassDraftIcons.TryGetValue(mainClass, out mainClassIcon);
                    }
                    else if (draftRewardData.ClassType == RunState.ClassType.SubClass)
                    {
                        string subClass = CustomClassManager.SaveManager.GetSubClass().GetID();
                        if (CustomClassManager.CustomClassDraftIcons.ContainsKey(subClass))
                            CustomClassManager.CustomClassDraftIcons.TryGetValue(subClass, out subClassIcon);
                    }
                }
            }
        }
    }
}
