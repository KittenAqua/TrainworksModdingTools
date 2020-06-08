using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using MonsterTrainModdingAPI.Managers;
using MonsterTrainModdingAPI.Interfaces;
using System.Linq;

namespace MonsterTrainModdingAPI.Patches
{
    [HarmonyPatch(typeof(SaveManager), "Initialize")]
    class SaveManagerInitializationPatch
    {
        static void Postfix(SaveManager __instance)
        {
            CustomCardManager.SaveManager = __instance;
            CustomCharacterManager.SaveManager = __instance;
            CustomCharacterManager.FinishCustomCharacterRegistration();
            List<IInitializable> initializables =
                PluginManager.Plugins.Values.ToList()
                    .Where((plugin) => (plugin is IInitializable))
                    .Select((plugin) => (plugin as IInitializable))
                    .ToList();
            initializables.ForEach((initializable) => initializable.Initialize());
        }
    }
}
