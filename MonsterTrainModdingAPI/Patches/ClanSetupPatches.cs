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
        /// This patch reimplements the functionality of SaveManager.LoadClassAndSubclass, which is called and then retconned away
        /// It also extends that functionality for covenants.
        /// It only applies to custom classes.
        /// </summary>
        [HarmonyPatch(typeof(GameStateManager), "StartRun")]
        public class ClanCardSetupPatch
        {
            static void Postfix(ref GameStateManager __instance, RunType runType,
                                string sharecode,
                                ClassData mainClass,
                                ClassData subClass,
                                int ascensionLevel)
            {
                if (ascensionLevel == 0) { return; }
                if (CustomClassManager.CustomClassData.ContainsKey(mainClass.GetID()))
                {
                    foreach (CardData cardData in mainClass.CreateMainClassStartingDeck())
                        SaveManagerInitializationPatch.SaveManager.AddCardToDeck(cardData);
                    if (ascensionLevel >= 6) { SaveManagerInitializationPatch.SaveManager.AddCardToDeck(mainClass.CreateMainClassStartingDeck()[0]); }
                    if (ascensionLevel >= 13) { SaveManagerInitializationPatch.SaveManager.AddCardToDeck(mainClass.CreateMainClassStartingDeck()[0]); }
                    //SaveManagerInitializationPatch.SaveManager.AddCardToDeck(mainClass.GetStartingChampionCard());
                }

                if (CustomClassManager.CustomClassData.ContainsKey(subClass.GetID()))
                {
                    foreach (CardData cardData in subClass.CreateSubClassStartingDeck())
                        SaveManagerInitializationPatch.SaveManager.AddCardToDeck(cardData);
                    if (ascensionLevel >= 8) { SaveManagerInitializationPatch.SaveManager.AddCardToDeck(mainClass.CreateMainClassStartingDeck()[0]); }
                    if (ascensionLevel >= 15) { SaveManagerInitializationPatch.SaveManager.AddCardToDeck(mainClass.CreateMainClassStartingDeck()[0]); }
                }
            }
        }

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

        // This patch fixes display on card upgrade trees
        [HarmonyPatch(typeof(ChampionUpgradeRewardData), "GetUpgradeTree")]
        public class CUSCanInit
        {
            static CardUpgradeTreeData Postfix(CardUpgradeTreeData ret, ref ChampionUpgradeRewardData __instance, SaveManager saveManager)
            {
                if (CustomClassManager.CustomClassData.ContainsKey(saveManager.GetMainClass().GetID()))
                {
                    return saveManager.GetMainClass().GetUpgradeTree();
                }

                return ret;
            }
        }
        
    }
}
