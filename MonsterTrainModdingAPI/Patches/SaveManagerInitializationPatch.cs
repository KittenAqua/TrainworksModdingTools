using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Managers;

namespace MonsterTrainModdingAPI.Patches
{
    [HarmonyPatch(typeof(SaveManager), "Initialize")]
    class SaveManagerInitializationPatch
    {
        static void Postfix(SaveManager __instance)
        {
            CustomCardManager.SaveManager = __instance;
            CustomCardManager.FinishCustomCardRegistration();
            CustomCharacterManager.SaveManager = __instance;
            CustomCharacterManager.FinishCustomCharacterRegistration();
        }
    }
}
