using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI.Patches
{
    class ClanSetupPatches
    {
        [HarmonyPatch(typeof(GameStateManager), "StartRun")]
        public class ClanCardSetupPatch
        {
            static void Postfix(ref GameStateManager __instance, RunType runType,
                                string sharecode,
                                ClassData mainClass,
                                ClassData subClass,
                                int ascensionLevel)
            {
                if (CustomClassManager.CustomClassData.ContainsKey(mainClass.GetID())) 
                {
                    foreach (CardData cardData in mainClass.CreateMainClassStartingDeck())
                        SaveManagerInitializationPatch.SaveManager.AddCardToDeck(cardData);
                    if (ascensionLevel >= 6) { SaveManagerInitializationPatch.SaveManager.AddCardToDeck(mainClass.CreateMainClassStartingDeck()[0]); }
                    if (ascensionLevel >= 13) { SaveManagerInitializationPatch.SaveManager.AddCardToDeck(mainClass.CreateMainClassStartingDeck()[0]); }
                    SaveManagerInitializationPatch.SaveManager.AddCardToDeck(mainClass.GetStartingChampionCard());
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
    }
}
